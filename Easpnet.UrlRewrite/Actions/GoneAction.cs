namespace Easpnet.UrlRewrite.Actions
{
    using System;
    using System.Net;

    public sealed class GoneAction : SetStatusAction
    {
        public GoneAction() : base(HttpStatusCode.Gone)
        {
        }
    }
}
