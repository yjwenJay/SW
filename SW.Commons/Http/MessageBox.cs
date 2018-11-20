using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SW.Commons.Http
{
    public static class MessageBox
    {
        #region JS代码操作
        private static long rnd = 0;
        /// <summary>
        /// Page给指定页面加入JS代码,已含script标记
        /// </summary>
        /// <param name="page">页面 一般为this</param>
        /// <param name="jsContent">JS代码</param>
        /// <param name="htmlStr">Html信息</param>
        public static void PageScriptAdd(System.Web.UI.Page page, string jsContent, string htmlStr = "")
        {
            if (page == null)
                PageScriptAdd(jsContent, htmlStr);
            else
            {
                rnd += 1;
                page.ClientScript.RegisterStartupScript(page.GetType(), rnd.ToString(), string.Format("{1}<script type=\"text/javascript\">{0}</script>", jsContent, htmlStr), false);
            }
        }


        /// <summary>
        /// (直接输出) JS代码,已含script标记
        /// </summary>
        /// <param name="jsContent">JS代码</param>
        /// <param name="htmlStr">Html信息</param>
        public static void PageScriptAdd(string jsContent, string htmlStr = "")
        {
            HttpContext.Current.Response.Write(string.Format("{1}<script type=\"text/javascript\">{0}</script>", jsContent, htmlStr));
        }

        /// <summary>
        /// Page弹出信息并转向,已含script标记
        /// </summary>
        /// <param name="page">this</param>
        /// <param name="str">提示文字</param>
        /// <param name="url">url</param>
        public static void PageAlertRedir(System.Web.UI.Page page, string str, string url, bool isTop = false)
        {
            PageScriptAdd(page, string.Format("alert(\"{0}\");{2}location.href=\"{1}\"", str, url, isTop ? "window.top." : "window."), str);
        }

        /// <summary>
        /// (直接输出)弹出信息并原窗口跳转,已含script(含End)
        /// </summary>
        /// <param name="str">提示文字</param>
        public static void PageAlertRedir(string str, string url, bool isTop = false)
        {
            PageScriptAdd(string.Format("alert(\"{0}\");{2}location.href=\"{1}\"", str, url, isTop ? "window.top." : "window."), str);
            try
            {
                HttpContext.Current.Response.End();
            }
            catch { }
        }

        /// <summary>
        /// (直接输出) 弹出提示文字,已含script标记
        /// </summary>
        /// <param name="str">提示文字</param>
        public static void PageScriptAlert(string str, bool incEnd = false)
        {
            PageScriptAdd(string.Format("alert(\"{0}\")", str), str);
            try
            {
                if (incEnd)
                    HttpContext.Current.Response.End();
            }
            catch { }
        }
        
        #endregion
    }
}
