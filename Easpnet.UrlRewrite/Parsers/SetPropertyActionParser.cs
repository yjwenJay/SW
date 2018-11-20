namespace Easpnet.UrlRewrite.Parsers
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Actions;
    using Easpnet.UrlRewrite.Utilities;
    using System;
    using System.Configuration;
    using System.Xml;

    public sealed class SetPropertyActionParser : RewriteActionParserBase
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
            XmlNode namedItem = node.Attributes.GetNamedItem("property");
            if (namedItem == null)
            {
                return null;
            }
            XmlNode node3 = node.Attributes.GetNamedItem("value");
            if (node3 == null)
            {
                throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.AttributeRequired, new object[] { "value" }), node);
            }
            return new SetPropertyAction(namedItem.Value, node3.Value);
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
                return "set";
            }
        }
    }
}
