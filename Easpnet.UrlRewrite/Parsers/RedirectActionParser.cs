namespace Easpnet.UrlRewrite.Parsers
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Actions;
    using Easpnet.UrlRewrite.Utilities;
    using System;
    using System.Configuration;
    using System.Xml;

    public sealed class RedirectActionParser : RewriteActionParserBase
    {
        public override IRewriteAction Parse(XmlNode node, object config)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            XmlNode namedItem = node.Attributes.GetNamedItem("to");
            if (namedItem == null)
            {
                throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.AttributeRequired, new object[] { "to" }), node);
            }
            bool permanent = true;
            XmlNode node3 = node.Attributes.GetNamedItem("permanent");
            if (node3 != null)
            {
                permanent = Convert.ToBoolean(node3.Value);
            }
            RedirectAction action = new RedirectAction(namedItem.Value, permanent);
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
                return "redirect";
            }
        }
    }
}
