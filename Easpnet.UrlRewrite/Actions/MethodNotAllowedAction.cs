namespace Easpnet.UrlRewrite.Actions
{
    using System;
    using System.Net;

    public sealed class MethodNotAllowedAction : SetStatusAction
    {
        public MethodNotAllowedAction() : base(HttpStatusCode.MethodNotAllowed)
        {
        }
    }
}
