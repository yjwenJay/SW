using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Security;

namespace Easpnet.Modules.Member.Pages
{
    public class LoginOut : PageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string logoutUrl = Html.WebRootUrl;
            if (!string.IsNullOrEmpty(Request["return"]))
            {
                logoutUrl = DES.Decrypt(Request["return"]);
            }

            if (Request.QueryString["type"] == "1")
            {
                Response.Redirect(Html.HrefLink("Member", "AdminLogin"));
                AdminContext.Logout();
            }
            else
            {
                MemberContext.Logout();
            }
            
            Response.Redirect(logoutUrl);
        }
    }
}
