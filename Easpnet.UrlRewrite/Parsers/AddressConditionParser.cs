namespace Easpnet.UrlRewrite.Parsers
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Conditions;
    using System;
    using System.Xml;

    public sealed class AddressConditionParser : IRewriteConditionParser
    {
        public IRewriteCondition Parse(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            XmlNode namedItem = node.Attributes.GetNamedItem("address");
            if (namedItem != null)
            {
                return new AddressCondition(namedItem.Value);
            }
            return null;
        }
    }
}
