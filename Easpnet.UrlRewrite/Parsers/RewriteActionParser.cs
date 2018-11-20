namespace Easpnet.UrlRewrite.Parsers
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Actions;
    using Easpnet.UrlRewrite.Utilities;
    using System;
    using System.Configuration;
    using System.Xml;

    public sealed class RewriteActionParser : RewriteActionParserBase
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
            XmlNode node2 = node.Attributes["to"];
            if (node2.Value == null)
            {
                throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.AttributeRequired, new object[] { "to" }), node);
            }
            XmlNode node3 = node.Attributes["processing"];
            RewriteProcessing continueProcessing = RewriteProcessing.ContinueProcessing;
            if (node3 != null)
            {
                if (node3.Value == "restart")
                {
                    continueProcessing = RewriteProcessing.RestartProcessing;
                }
                else if (node3.Value == "stop")
                {
                    continueProcessing = RewriteProcessing.StopProcessing;
                }
                else if (node3.Value != "continue")
                {
                    throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.ValueOfProcessingAttribute, new object[] { node3.Value, "continue", "restart", "stop" }), node);
                }
            }
            RewriteAction action = new RewriteAction(node2.Value, continueProcessing);
            base.ParseConditions(node, action.Conditions, false, config);
            return action;
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
                return false;
            }
        }

        public override string Name
        {
            get
            {
                return "rewrite";
            }
        }
    }
}
