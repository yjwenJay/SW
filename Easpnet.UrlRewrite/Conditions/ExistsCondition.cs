namespace Easpnet.UrlRewrite.Conditions
{
    using Easpnet.UrlRewrite;
    using System;
    using System.IO;

    public class ExistsCondition : IRewriteCondition
    {
        private string _location;

        public ExistsCondition(string location)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }
            this._location = location;
        }

        public bool IsMatch(RewriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            string path = context.MapPath(context.Expand(this._location));
            if (!File.Exists(path))
            {
                return Directory.Exists(path);
            }
            return true;
        }
    }
}
