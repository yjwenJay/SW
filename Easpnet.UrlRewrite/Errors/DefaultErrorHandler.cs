namespace Easpnet.UrlRewrite.Errors
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Web;

    public class DefaultErrorHandler : IRewriteErrorHandler
    {
        private string _url;

        public DefaultErrorHandler(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            this._url = url;
        }

        public void HandleError(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            context.Server.Execute(this._url);
        }
    }
}
