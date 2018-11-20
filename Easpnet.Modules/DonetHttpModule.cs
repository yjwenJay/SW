using System;
using System.Web;
using Easpnet.Modules.Models;

namespace Easpnet.Modules
{
    public class EaspnetHttpModule : IHttpModule
    {
        #region IHttpModule 成员

        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.PostAcquireRequestState += new EventHandler(context_PostAcquireRequestState);
        }


        void context_PostAcquireRequestState(object sender, EventArgs e)
        {
            //获取当前处理的页面对象
            IndexPage page = HttpContext.Current.Handler as IndexPage;
            if (page != null)
            {
                page.CurrentModuleName = HttpContext.Current.Request.QueryString["module"];
                if (HttpContext.Current.Request.QueryString["page"] == null)
                {
                    page.CurrentModuleName = "Core";
                    page.CurrentPageName = "Index";
                }
                else
                {
                    page.CurrentPageName = HttpContext.Current.Request.QueryString["page"];
                }


                //设置页面相关属性值
                page.CurrentTheme = Easpnet.Modules.Models.Theme.GetCurrentTheme();
                page.WebRootUrl = Local.LocalConfig.WebRootUrl;
                page.Configs = Config.GetConfigs();

                //获取页面信息
                page.CurrentPage = PageInfo.GetPageInfo(page.CurrentModuleName, page.CurrentPageName);
                if (page.CurrentPage == null)
                {
                    page.CurrentModuleName = "Core";
                    page.CurrentPageName = "PageNotFound";
                    page.CurrentPage = PageInfo.GetPageInfo(page.CurrentModuleName, page.CurrentPageName);
                }

                if (page.CurrentPage != null)
                {
                    page.CurrentPageGroup = PageGroup.GetPageGroup(page.CurrentPageName);
                }
            }
        }


        void context_BeginRequest(object sender, EventArgs e)
        {
            //Html.CurrentPageName = null;
            //string CurrentPageName = HttpContext.Current.Request.QueryString["page"];

            //if (CurrentPageName != null)
            //{
            //    ///// 获取页面信息
            //    Html.CurrentPageName = CurrentPageName;

            //    //设置模板
            //    Html.CurrentThemeName = Easpnet.Modules.Models.Theme.GetCurrentTheme();

            //    //获取配置
            //    Html.Configs = Config.GetConfigs();

            //    //获取当前处理的页面对象
            //    IndexPage page = HttpContext.Current.Handler as IndexPage;

            //}
        }

        #endregion
    }
}
