namespace Easpnet.UrlRewrite
{
    public interface IRewriteAction
    {
        RewriteProcessing Execute(RewriteContext context);
    }
}
