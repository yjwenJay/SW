namespace Easpnet.UrlRewrite.Parsers
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Actions;
    using System;
    using System.Xml;

    public sealed class NotImplementedActionParser : RewriteActionParserBase
    {
        public override IRewriteAction Parse(XmlNode node, object config)
        {
            return new NotImplementedAction();
        }

        public override bool AllowsAttributes
        {
            get
            {
                return false;
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
                return "not-implemented";
            }
        }
    }
}
