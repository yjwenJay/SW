namespace Easpnet.UrlRewrite.Configuration
{
    using System;
    using System.Configuration;
    using System.Xml;

    public sealed class RewriterConfigurationSectionHandler : IConfigurationSectionHandler
    {
        object IConfigurationSectionHandler.Create(object parent, object configContext, XmlNode section)
        {
            return section;
        }
    }
}
