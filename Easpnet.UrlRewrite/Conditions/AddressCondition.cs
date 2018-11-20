namespace Easpnet.UrlRewrite.Conditions
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Utilities;
    using System;
    using System.Net;

    public sealed class AddressCondition : IRewriteCondition
    {
        private IPRange _range;

        public AddressCondition(string pattern)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException("pattern");
            }
            this._range = IPRange.Parse(pattern);
        }

        public bool IsMatch(RewriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            string ipString = context.Properties["REMOTE_ADDR"];
            return ((ipString != null) && this._range.InRange(IPAddress.Parse(ipString)));
        }
    }
}
