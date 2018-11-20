namespace Easpnet.UrlRewrite
{
    using Easpnet.UrlRewrite.Configuration;
    using System;
    using System.Web;
    using Easpnet.UrlRewrite.Utilities;
    using Easpnet.Modules;

    public sealed class RewriterHttpModule : IHttpModule
    {
        private static RewriterEngine _rewriter = new RewriterEngine(new HttpContextFacade(), RewriterConfiguration.Current);

        private void BeginRequest(object sender, EventArgs e)
        {
            //若文件不存在，则进行重写
            if (!System.IO.File.Exists(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath)))
            {
                HttpContext.Current.Response.AddHeader("X-Powered-By", Configuration.XPoweredBy);
                _rewriter = new RewriterEngine(new HttpContextFacade(), RewriterConfiguration.Current);
                _rewriter.Rewrite();
            }
        }

        public static string ResolveLocation(string location)
        {
            return _rewriter.ResolveLocation(location);
        }

        void IHttpModule.Dispose()
        {
        }

        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(this.BeginRequest);
        }

        public static RewriterConfiguration Configuration
        {
            get
            {
                return RewriterConfiguration.Current;
            }
        }

        public static string OriginalQueryString
        {
            get
            {
                return _rewriter.OriginalQueryString;
            }
            set
            {
                _rewriter.OriginalQueryString = value;
            }
        }

        public static string QueryString
        {
            get
            {
                return _rewriter.QueryString;
            }
            set
            {
                _rewriter.QueryString = value;
            }
        }

        public static string RawUrl
        {
            get
            {
                return _rewriter.RawUrl;
            }
            set
            {
                _rewriter.RawUrl = value;
            }
        }
    }
}
