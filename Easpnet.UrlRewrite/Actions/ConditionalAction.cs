namespace Easpnet.UrlRewrite.Actions
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Collections;

    public class ConditionalAction : IRewriteAction, IRewriteCondition
    {
        private ArrayList _actions = new ArrayList();
        private ArrayList _conditions = new ArrayList();

        public virtual RewriteProcessing Execute(RewriteContext context)
        {
            for (int i = 0; i < this.Actions.Count; i++)
            {
                IRewriteCondition condition = this.Actions[i] as IRewriteCondition;
                if ((condition == null) || condition.IsMatch(context))
                {
                    RewriteProcessing processing = (this.Actions[i] as IRewriteAction).Execute(context);
                    if (processing != RewriteProcessing.ContinueProcessing)
                    {
                        return processing;
                    }
                }
            }
            return RewriteProcessing.ContinueProcessing;
        }

        public virtual bool IsMatch(RewriteContext context)
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

        public IList Actions
        {
            get
            {
                return this._actions;
            }
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
