namespace Easpnet.UrlRewrite.Configuration
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Errors;
    using Easpnet.UrlRewrite.Logging;
    using Easpnet.UrlRewrite.Transforms;
    using Easpnet.UrlRewrite.Utilities;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Xml;

    public sealed class RewriterConfigurationReader
    {
        private RewriterConfigurationReader()
        {
        }

        public static object Read(XmlNode section)
        {
            if (section == null)
            {
                throw new ArgumentNullException("section");
            }
            RewriterConfiguration config = RewriterConfiguration.Create();
            foreach (XmlNode node in section.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                if (node.LocalName == "error-handler")
                {
                    ReadErrorHandler(node, config);
                    continue;
                }
                if (node.LocalName == "default-documents")
                {
                    ReadDefaultDocuments(node, config);
                    continue;
                }
                if (node.LocalName == "register")
                {
                    if (node.Attributes["parser"] != null)
                    {
                        ReadRegisterParser(node, config);
                    }
                    else if (node.Attributes["transform"] != null)
                    {
                        ReadRegisterTransform(node, config);
                    }
                    else if (node.Attributes["logger"] != null)
                    {
                        ReadRegisterLogger(node, config);
                    }
                    continue;
                }
                if (node.LocalName == "mapping")
                {
                    ReadMapping(node, config);
                }
                else
                {
                    ReadRule(node, config);
                }
            }
            return config;
        }

        private static void ReadDefaultDocuments(XmlNode node, RewriterConfiguration config)
        {
            foreach (XmlNode node2 in node.ChildNodes)
            {
                if ((node2.NodeType == XmlNodeType.Element) && (node2.LocalName == "document"))
                {
                    config.DefaultDocuments.Add(node2.InnerText);
                }
            }
        }

        private static void ReadErrorHandler(XmlNode node, RewriterConfiguration config)
        {
            XmlNode node2 = node.Attributes["code"];
            if (node2 == null)
            {
                throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.AttributeRequired, new object[] { "code" }), node);
            }
            XmlNode node3 = node.Attributes["type"];
            XmlNode node4 = node.Attributes["url"];
            if ((node3 == null) && (node4 == null))
            {
                throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.AttributeRequired, new object[] { "url" }), node);
            }
            IRewriteErrorHandler handler = null;
            if (node3 != null)
            {
                handler = TypeHelper.Activate(node3.Value, null) as IRewriteErrorHandler;
                if (handler == null)
                {
                    throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.InvalidTypeSpecified, new object[0]));
                }
            }
            else
            {
                handler = new DefaultErrorHandler(node4.Value);
            }
            config.ErrorHandlers.Add(Convert.ToInt32(node2.Value), handler);
        }

        private static void ReadMapping(XmlNode node, RewriterConfiguration config)
        {
            XmlNode node2 = node.Attributes["name"];
            StringDictionary map = new StringDictionary();
            foreach (XmlNode node3 in node.ChildNodes)
            {
                if (node3.NodeType == XmlNodeType.Element)
                {
                    if (node3.LocalName != "map")
                    {
                        throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.ElementNotAllowed, new object[] { node3.LocalName }), node);
                    }
                    XmlNode node4 = node3.Attributes["from"];
                    if (node4 == null)
                    {
                        throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.AttributeRequired, new object[] { "from" }), node);
                    }
                    XmlNode node5 = node3.Attributes["to"];
                    if (node5 == null)
                    {
                        throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.AttributeRequired, new object[] { "to" }), node);
                    }
                    map.Add(node4.Value, node5.Value);
                }
            }
            config.TransformFactory.AddTransform(new StaticMappingTransform(node2.Value, map));
        }

        private static void ReadRegisterLogger(XmlNode node, RewriterConfiguration config)
        {
            XmlNode node2 = node.Attributes["logger"];
            if (node2 == null)
            {
                throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.AttributeRequired, new object[] { "logger" }), node);
            }
            if (node.ChildNodes.Count > 0)
            {
                throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.ElementNoElements, new object[] { "register" }), node);
            }
            IRewriteLogger logger = TypeHelper.Activate(node2.Value, null) as IRewriteLogger;
            if (logger != null)
            {
                config.Logger = logger;
            }
        }

        private static void ReadRegisterParser(XmlNode node, RewriterConfiguration config)
        {
            XmlNode node2 = node.Attributes["parser"];
            if (node2 == null)
            {
                throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.AttributeRequired, new object[] { "parser" }), node);
            }
            if (node.ChildNodes.Count > 0)
            {
                throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.ElementNoElements, new object[] { "register" }), node);
            }
            object obj2 = TypeHelper.Activate(node2.Value, null);
            IRewriteActionParser parser = obj2 as IRewriteActionParser;
            if (parser != null)
            {
                config.ActionParserFactory.AddParser(parser);
            }
            IRewriteConditionParser parser2 = obj2 as IRewriteConditionParser;
            if (parser2 != null)
            {
                config.ConditionParserPipeline.AddParser(parser2);
            }
        }

        private static void ReadRegisterTransform(XmlNode node, RewriterConfiguration config)
        {
            XmlNode node2 = node.Attributes["transform"];
            if (node2 == null)
            {
                throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.AttributeRequired, new object[] { "transform" }), node);
            }
            if (node.ChildNodes.Count > 0)
            {
                throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.ElementNoElements, new object[] { "register" }), node);
            }
            IRewriteTransform transform = TypeHelper.Activate(node2.Value, null) as IRewriteTransform;
            if (transform != null)
            {
                config.TransformFactory.AddTransform(transform);
            }
        }

        private static void ReadRule(XmlNode node, RewriterConfiguration config)
        {
            bool flag = false;
            IList parsers = config.ActionParserFactory.GetParsers(node.LocalName);
            if (parsers != null)
            {
                foreach (IRewriteActionParser parser in parsers)
                {
                    if (!parser.AllowsNestedActions && (node.ChildNodes.Count > 0))
                    {
                        throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.ElementNoElements, new object[] { parser.Name }), node);
                    }
                    if (!parser.AllowsAttributes && (node.Attributes.Count > 0))
                    {
                        throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.ElementNoAttributes, new object[] { parser.Name }), node);
                    }
                    IRewriteAction action = parser.Parse(node, config);
                    if (action != null)
                    {
                        config.Rules.Add(action);
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag)
            {
                throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.ElementNotAllowed, new object[] { node.LocalName }), node);
            }
        }
    }
}
