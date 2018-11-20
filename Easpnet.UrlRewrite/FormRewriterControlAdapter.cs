namespace Easpnet.UrlRewrite
{
    using System;
    using System.Web.UI;
    using System.Web.UI.Adapters;

    public class FormRewriterControlAdapter : ControlAdapter
    {
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(new RewriteFormHtmlTextWriter(writer));
        }
    }
}
