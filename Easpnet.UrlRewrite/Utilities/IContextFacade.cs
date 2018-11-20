namespace Easpnet.UrlRewrite.Utilities
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Collections.Specialized;
    using System.Web;

    public interface IContextFacade
    {
        void AppendCookie(HttpCookie cookie);
        void AppendHeader(string name, string value);
        string GetApplicationPath();
        HttpCookieCollection GetCookies();
        NameValueCollection GetHeaders();
        string GetHttpMethod();
        object GetItem(object item);
        string GetRawUrl();
        Uri GetRequestUrl();
        NameValueCollection GetServerVariables();
        void HandleError(IRewriteErrorHandler handler);
        void RewritePath(string url);
        void SetItem(object item, object value);
        void SetRedirectLocation(string url);
        void SetStatusCode(int code);

        Easpnet.UrlRewrite.Utilities.MapPath MapPath { get; }
    }
}
