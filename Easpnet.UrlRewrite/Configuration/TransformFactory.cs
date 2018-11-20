namespace Easpnet.UrlRewrite.Configuration
{
    using Easpnet.UrlRewrite;
    using Easpnet.UrlRewrite.Utilities;
    using System;
    using System.Collections;

    public class TransformFactory
    {
        private Hashtable _transforms = new Hashtable();

        public void AddTransform(IRewriteTransform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException("transform");
            }
            this._transforms.Add(transform.Name, transform);
        }

        public void AddTransform(string transformType)
        {
            this.AddTransform((IRewriteTransform) TypeHelper.Activate(transformType, null));
        }

        public IRewriteTransform GetTransform(string name)
        {
            return (this._transforms[name] as IRewriteTransform);
        }
    }
}
