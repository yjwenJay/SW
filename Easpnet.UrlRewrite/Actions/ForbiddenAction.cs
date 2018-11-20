namespace Easpnet.UrlRewrite.Actions
{
    using System;
    using System.Net;

    public sealed class ForbiddenAction : SetStatusAction
    {
        public ForbiddenAction() : base(HttpStatusCode.Forbidden)
        {
        }
    }
}
