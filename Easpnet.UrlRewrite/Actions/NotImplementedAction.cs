namespace Easpnet.UrlRewrite.Actions
{
    using System;
    using System.Net;

    public sealed class NotImplementedAction : SetStatusAction
    {
        public NotImplementedAction() : base(HttpStatusCode.NotImplemented)
        {
        }
    }
}
