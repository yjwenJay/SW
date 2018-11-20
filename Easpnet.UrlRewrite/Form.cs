namespace Easpnet.UrlRewrite
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    [ToolboxData("<{0}:Form runat=server></{0}:RewrittenForm>"), ComVisible(false)]
    public class Form : HtmlForm
    {
        private string GetAction()
        {
            return RewriterHttpModule.RawUrl;
        }

        private string GetID()
        {
            return this.ClientID;
        }

        private string GetMethod()
        {
            return base.Method;
        }

        private string GetName()
        {
            return this.Name;
        }

        protected override void RenderAttributes(HtmlTextWriter writer)
        {
            writer.WriteAttribute("name", this.GetName());
            base.Attributes.Remove("name");
            writer.WriteAttribute("method", this.GetMethod());
            base.Attributes.Remove("method");
            writer.WriteAttribute("action", this.GetAction(), true);
            base.Attributes.Remove("action");
            base.Attributes.Render(writer);
            if (this.ID != null)
            {
                writer.WriteAttribute("id", this.GetID());
            }
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.RenderChildren(writer);
            writer.RenderEndTag();
        }
    }
}
