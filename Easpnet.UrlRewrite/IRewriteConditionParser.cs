namespace Easpnet.UrlRewrite
{
    using System.Xml;

    public interface IRewriteConditionParser
    {
        IRewriteCondition Parse(XmlNode node);
    }
}
