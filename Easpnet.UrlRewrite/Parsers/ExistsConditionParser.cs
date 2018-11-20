namespace Easpnet.UrlRewrite.Parsers
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Conditions;
    using System;
    using System.Xml;

    public sealed class ExistsConditionParser : IRewriteConditionParser
    {
        public IRewriteCondition Parse(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            XmlNode namedItem = node.Attributes.GetNamedItem("exists");
            if (namedItem != null)
            {
                return new ExistsCondition(namedItem.Value);
            }
            return null;
        }
    }
}
