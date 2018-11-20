namespace Easpnet.UrlRewrite.Transforms
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Text;

    public sealed class Base64DecodeTransform : IRewriteTransform
    {
        public string ApplyTransform(string input)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(input));
        }

        public string Name
        {
            get
            {
                return "base64";
            }
        }
    }
}
