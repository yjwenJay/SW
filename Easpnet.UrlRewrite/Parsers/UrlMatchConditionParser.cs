namespace Easpnet.UrlRewrite.Parsers
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Conditions;
    using System;
    using System.Xml;

    public sealed class UrlMatchConditionParser : IRewriteConditionParser
    {
        public IRewriteCondition Parse(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            XmlNode namedItem = node.Attributes.GetNamedItem("url");
            if (namedItem != null)
            {
                return new UrlMatchCondition(namedItem.Value);
            }
            return null;
        }
    }
}
