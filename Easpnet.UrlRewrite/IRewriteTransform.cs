namespace Easpnet.UrlRewrite
{
    using System;

    public interface IRewriteTransform
    {
        string ApplyTransform(string input);

        string Name { get; }
    }
}
