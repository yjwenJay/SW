using System.Web;
using System.Reflection;

namespace Easpnet.Modules
{
    public class AjaxIndexPage : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            IAjaxHandler iajax;
            string moduleName = context.Request.QueryString["_m"];
            string blockName = context.Request.QueryString["_b"];

            Easpnet.Modules.Models.Module md = Easpnet.Modules.Models.Module.GetModuleByName(moduleName);
            if (md != null)
            {
                string dll = md.AssemblyName;

                //默认情况下使用模块默认的Ajax处理类，若接收到_b参数，则调用到模块的Ajax处理类
                string clsname = md.NameSpace + ".AjaxHandler";

                if (!string.IsNullOrEmpty(blockName))
                {
                    clsname = md.NameSpace + ".Blocks." + blockName;
                }

                Assembly ass = Assembly.LoadFrom(context.Server.MapPath("~/bin/" + dll));
                iajax = ass.CreateInstance(clsname) as IAjaxHandler;
                if (iajax != null)
                {
                    iajax.Ajax();
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
