namespace Easpnet.UrlRewrite.Conditions
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Text.RegularExpressions;

    public sealed class PropertyMatchCondition : MatchCondition
    {
        private string _propertyName;

        public PropertyMatchCondition(string propertyName, string pattern) : base(pattern)
        {
            this._propertyName = string.Empty;
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }
            if (pattern == null)
            {
                throw new ArgumentNullException("pattern");
            }
            this._propertyName = propertyName;
        }

        public override bool IsMatch(RewriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            string input = context.Properties[this.PropertyName];
            if (input != null)
            {
                Match match = base.Pattern.Match(input);
                if (match.Success)
                {
                    context.LastMatch = match;
                    return true;
                }
            }
            return false;
        }

        public string PropertyName
        {
            get
            {
                return this._propertyName;
            }
        }
    }
}
