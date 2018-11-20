namespace Easpnet.UrlRewrite.Actions
{
    using System;
    using System.Net;

    public sealed class NotFoundAction : SetStatusAction
    {
        public NotFoundAction() : base(HttpStatusCode.NotFound)
        {
        }
    }
}
