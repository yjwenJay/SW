namespace Easpnet.UrlRewrite
{
    using System;
    using System.Web;

    public interface IRewriteErrorHandler
    {
        void HandleError(HttpContext context);
    }
}
