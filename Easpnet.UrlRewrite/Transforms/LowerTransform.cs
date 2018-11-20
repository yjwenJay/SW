namespace Easpnet.UrlRewrite.Transforms
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Threading;

    public sealed class LowerTransform : IRewriteTransform
    {
        public string ApplyTransform(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            return input.ToLower(Thread.CurrentThread.CurrentCulture);
        }

        public string Name
        {
            get
            {
                return "lower";
            }
        }
    }
}
