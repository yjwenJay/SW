namespace Easpnet.UrlRewrite.Conditions
{
    using Easpnet.UrlRewrite;
    using System;

    public sealed class NegativeCondition : IRewriteCondition
    {
        private IRewriteCondition _chainedCondition;

        public NegativeCondition(IRewriteCondition chainedCondition)
        {
            if (chainedCondition == null)
            {
                throw new ArgumentNullException("chainedCondition");
            }
            this._chainedCondition = chainedCondition;
        }

        public bool IsMatch(RewriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            return !this._chainedCondition.IsMatch(context);
        }
    }
}
