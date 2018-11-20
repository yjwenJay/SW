namespace Easpnet.UrlRewrite.Conditions
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Text.RegularExpressions;

    public abstract class MatchCondition : IRewriteCondition
    {
        private Regex _pattern;

        protected MatchCondition(string pattern)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException("pattern");
            }
            this._pattern = new Regex(pattern, RegexOptions.IgnoreCase);
        }

        public abstract bool IsMatch(RewriteContext context);

        public Regex Pattern
        {
            get
            {
                return this._pattern;
            }
        }
    }
}
