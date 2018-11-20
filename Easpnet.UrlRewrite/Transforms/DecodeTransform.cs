namespace Easpnet.UrlRewrite.Transforms
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Web;

    public sealed class DecodeTransform : IRewriteTransform
    {
        public string ApplyTransform(string input)
        {
            return HttpUtility.UrlDecode(input);
        }

        public string Name
        {
            get
            {
                return "decode";
            }
        }
    }
}
