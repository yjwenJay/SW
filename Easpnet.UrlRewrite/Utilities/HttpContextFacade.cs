namespace Easpnet.UrlRewrite.Utilities
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Collections.Specialized;
    using System.Web;

    internal class HttpContextFacade : IContextFacade
    {
        private Easpnet.UrlRewrite.Utilities.MapPath _mapPath;

        public HttpContextFacade()
        {
            this._mapPath = new Easpnet.UrlRewrite.Utilities.MapPath(this.InternalMapPath);
        }

        public void AppendCookie(HttpCookie cookie)
        {
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public void AppendHeader(string name, string value)
        {
            HttpContext.Current.Response.AppendHeader(name, value);
        }

        public string GetApplicationPath()
        {
            return HttpContext.Current.Request.ApplicationPath;
        }

        public HttpCookieCollection GetCookies()
        {
            return HttpContext.Current.Request.Cookies;
        }

        public NameValueCollection GetHeaders()
        {
            return HttpContext.Current.Request.Headers;
        }

        public string GetHttpMethod()
        {
            return HttpContext.Current.Request.HttpMethod;
        }

        public object GetItem(object item)
        {
            return HttpContext.Current.Items[item];
        }

        public string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        public Uri GetRequestUrl()
        {
            return HttpContext.Current.Request.Url;
        }

        public NameValueCollection GetServerVariables()
        {
            return HttpContext.Current.Request.ServerVariables;
        }

        public void HandleError(IRewriteErrorHandler handler)
        {
            handler.HandleError(HttpContext.Current);
        }

        private string InternalMapPath(string url)
        {
            return HttpContext.Current.Server.MapPath(url);
        }

        public void RewritePath(string url)
        {
            HttpContext.Current.RewritePath(url, false);
        }

        public void SetItem(object item, object value)
        {
            HttpContext.Current.Items[item] = value;
        }

        public void SetRedirectLocation(string url)
        {
            HttpContext.Current.Response.RedirectLocation = url;
        }

        public void SetStatusCode(int code)
        {
            HttpContext.Current.Response.StatusCode = code;
        }

        public Easpnet.UrlRewrite.Utilities.MapPath MapPath
        {
            get
            {
                return this._mapPath;
            }
        }
    }
}
