namespace Easpnet.UrlRewrite.Actions
{
    using Easpnet.UrlRewrite;
    using System;

    public class SetPropertyAction : IRewriteAction
    {
        private string _name;
        private string _value;

        public SetPropertyAction(string name, string value)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this._name = name;
            this._value = value;
        }

        public RewriteProcessing Execute(RewriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            context.Properties.Set(this.Name, context.Expand(this.Value));
            return RewriteProcessing.ContinueProcessing;
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public string Value
        {
            get
            {
                return this._value;
            }
        }
    }
}
