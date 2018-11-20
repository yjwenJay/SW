namespace Easpnet.UrlRewrite.Actions
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Collections;

    public sealed class RewriteAction : SetLocationAction, IRewriteCondition
    {
        private ArrayList _conditions;
        private RewriteProcessing _processing;

        public RewriteAction(string location, RewriteProcessing processing) : base(location)
        {
            this._conditions = new ArrayList();
            this._processing = processing;
        }

        public override RewriteProcessing Execute(RewriteContext context)
        {
            base.Execute(context);
            return this._processing;
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
