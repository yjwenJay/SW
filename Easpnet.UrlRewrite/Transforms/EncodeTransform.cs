namespace Easpnet.UrlRewrite.Transforms
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Web;

    public sealed class EncodeTransform : IRewriteTransform
    {
        public string ApplyTransform(string input)
        {
            return HttpUtility.UrlEncode(input);
        }

        public string Name
        {
            get
            {
                return "encode";
            }
        }
    }
}
