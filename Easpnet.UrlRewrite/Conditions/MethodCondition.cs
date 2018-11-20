namespace Easpnet.UrlRewrite.Conditions
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Text.RegularExpressions;

    public sealed class MethodCondition : MatchCondition
    {
        public MethodCondition(string pattern) : base(GetMethodPattern(pattern))
        {
        }

        private static string GetMethodPattern(string method)
        {
            return string.Format("^{0}$", Regex.Replace(method, @"[^a-zA-Z,\*]+", "").Replace(",", "|").Replace("*", ".+"));
        }

        public override bool IsMatch(RewriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            return base.Pattern.IsMatch(context.Method);
        }
    }
}
