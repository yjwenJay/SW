namespace Easpnet.UrlRewrite.Transforms
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Threading;

    public sealed class UpperTransform : IRewriteTransform
    {
        public string ApplyTransform(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            return input.ToUpper(Thread.CurrentThread.CurrentCulture);
        }

        public string Name
        {
            get
            {
                return "upper";
            }
        }
    }
}
