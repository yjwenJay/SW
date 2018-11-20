namespace Easpnet.UrlRewrite.Parsers
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Conditions;
    using Easpnet.UrlRewrite.Utilities;
    using System;
    using System.Configuration;
    using System.Xml;

    public sealed class HeaderMatchConditionParser : IRewriteConditionParser
    {
        public IRewriteCondition Parse(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            XmlNode namedItem = node.Attributes.GetNamedItem("header");
            if (namedItem == null)
            {
                return null;
            }
            string propertyName = namedItem.Value;
            XmlNode node3 = node.Attributes.GetNamedItem("match");
            if (node3 == null)
            {
                throw new ConfigurationErrorsException(MessageProvider.FormatString(Message.AttributeRequired, new object[] { "match" }), node);
            }
            return new PropertyMatchCondition(propertyName, node3.Value);
        }
    }
}
