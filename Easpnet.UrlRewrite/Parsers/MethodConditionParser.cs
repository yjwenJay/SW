namespace Easpnet.UrlRewrite.Parsers
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Conditions;
    using System;
    using System.Xml;

    public sealed class MethodConditionParser : IRewriteConditionParser
    {
        public IRewriteCondition Parse(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            XmlNode namedItem = node.Attributes.GetNamedItem("method");
            if (namedItem != null)
            {
                return new MethodCondition(namedItem.Value);
            }
            return null;
        }
    }
}
