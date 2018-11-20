namespace Easpnet.UrlRewrite.Actions
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Collections;
    using System.Net;

    public sealed class RedirectAction : SetLocationAction, IRewriteCondition
    {
        private ArrayList _conditions;
        private bool _permanent;

        public RedirectAction(string location, bool permanent) : base(location)
        {
            this._conditions = new ArrayList();
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }
            this._permanent = permanent;
        }

        public override RewriteProcessing Execute(RewriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            base.Execute(context);
            if (this._permanent)
            {
                context.StatusCode = HttpStatusCode.MovedPermanently;
            }
            else
            {
                context.StatusCode = HttpStatusCode.Found;
            }
            return RewriteProcessing.StopProcessing;
        }

        public bool IsMatch(RewriteContext context)
        {
            foreach (IRewriteCondition condition in this.Conditions)
            {
                if (!condition.IsMatch(context))
                {
                    return false;
                }
            }
            return true;
        }

        public IList Conditions
        {
            get
            {
                return this._conditions;
            }
        }
    }
}
