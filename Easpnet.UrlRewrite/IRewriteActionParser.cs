namespace Easpnet.UrlRewrite
{
    using System;
    using System.Xml;

    public interface IRewriteActionParser
    {
        IRewriteAction Parse(XmlNode node, object config);

        bool AllowsAttributes { get; }

        bool AllowsNestedActions { get; }

        string Name { get; }
    }
}
