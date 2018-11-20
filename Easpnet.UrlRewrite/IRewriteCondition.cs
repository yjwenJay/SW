namespace Easpnet.UrlRewrite
{
    using System;

    public interface IRewriteCondition
    {
        bool IsMatch(RewriteContext context);
    }
}
