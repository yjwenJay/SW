namespace Easpnet.UrlRewrite.Transforms
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Text;

    public sealed class Base64Transform : IRewriteTransform
    {
        public string ApplyTransform(string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        public string Name
        {
            get
            {
                return "base64decode";
            }
        }
    }
}
