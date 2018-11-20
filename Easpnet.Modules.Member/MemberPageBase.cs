using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Member
{
    /// <summary>
    /// 会员中心页面基类
    /// </summary>
    public class MemberPageBase : PageBase
    {
        protected Member.Models.User u;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!MemberContext.IsLogin)
            {
                Response.Redirect(Html.HrefLink("Core", "Login"));
            }
            else
            {
                u = MemberContext.LoginUser;
            }
        }
    }
}
