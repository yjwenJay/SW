namespace Easpnet.UrlRewrite.Parsers
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Actions;
    using Easpnet.UrlRewrite.Configuration;
    using Easpnet.UrlRewrite.Utilities;
    using System;
    using System.Collections;
    using System.Configuration;
    using System.Xml;

    public class IfConditionActionParser : RewriteActionParserBase
    {
        public override IRewriteAction Parse(XmlNode node, object config)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            RewriterConfiguration configuration = config as RewriterConfiguration;
            ConditionalAction action = new ConditionalAction();
            bool negative = node.LocalName == "unless";
            base.ParseConditions(node, action.Conditions, negative, config);
            ReadActions(node, action.Actions, configuration);
            return action;
        }

        private static void ReadActions(XmlNode node, IList actions, RewriterConfiguration config)
        {
            for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
            {
                if (node2.NodeType == XmlNodeType.Element)
                {
                    IList parsers = config.ActionParserFactory.GetParsers(node2.LocalName);
                    if (parsers != null)
                    {
                        bool flag = false;
                        foreach (IRewriteActionParser parser in parsers)
                        {
                            IRewriteAction action = parser.Parse(node2, config);
                            if (action != null)
                            {
                                flag = true;
                                actions.Add(action);
                            }
                        }
                        if (!flag)
                        {
                            throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.ElementNotAllowed, new object[] { node.FirstChild.Name }), node);
                        }
                    }
                }
            }
        }

        public override bool AllowsAttributes
        {
            get
            {
                return true;
            }
        }

        public override bool AllowsNestedActions
        {
            get
            {
                return true;
            }
        }

        public override string Name
        {
            get
            {
                return "if";
            }
        }
    }
}
