namespace Easpnet.UrlRewrite.Actions
{
    using Easpnet.UrlRewrite;
    using System;
    using System.Text.RegularExpressions;

    public abstract class SetLocationAction : IRewriteAction
    {
        private string _location;

        protected SetLocationAction(string location)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }
            this._location = location;
        }

        public virtual RewriteProcessing Execute(RewriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            //
            string location = context.Location;
            Regex reg = new Regex("\\?.*$");
            Match math = reg.Match(location);
            string par = "";
            if (math.Success)
            {
                par = math.Value;
            }


            //
            context.Location = context.ResolveLocation(context.Expand(this.Location));
            
            //
            if (reg.Match(context.Location).Success)
            {
                context.Location += par.Replace("?", "&");
            }
            else
            {
                context.Location += par;
            }

            //
            return RewriteProcessing.StopProcessing;
        }

        public string Location
        {
            get
            {
                return this._location;
            }
        }
    }
}
