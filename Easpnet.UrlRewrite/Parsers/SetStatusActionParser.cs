namespace Easpnet.UrlRewrite.Parsers
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Actions;
    using System;
    using System.Net;
    using System.Xml;

    public sealed class SetStatusActionParser : RewriteActionParserBase
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
            XmlNode namedItem = node.Attributes.GetNamedItem("status");
            if (namedItem == null)
            {
                return null;
            }
            return new SetStatusAction((HttpStatusCode) Convert.ToInt32(namedItem.Value));
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
