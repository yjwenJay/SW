using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Easpnet.Modules.Models;
using Easpnet.Modules.Member.Models;

namespace Easpnet.Modules.Member
{
    public class MemberHttpModule : IHttpModule
    {
        #region IHttpModule 成员

        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.PostAcquireRequestState += new EventHandler(context_PostAcquireRequestState);
        }


        void context_PostAcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {                
                //
                if (Html.IndexPage != null && Html.CurrentPage != null && Html.CurrentPage.IsAdminPage)
                {
                    //进行权限的验证
                    AdminUser loginUser = AdminContext.LoginUser;

                    if (loginUser == null || loginUser.UserType == UserType.Normal)
                    {
                        HttpContext.Current.Response.Write("<script language='javascript'>alert(\"登陆超时，请重新登陆！\");window.top.location='"
                        + Html.HrefLink("Member", "AdminLogin") + "'</script>");
                        HttpContext.Current.Response.End();
                    }
                    //后台首页不进行权限的限制
                    //只对员工用户进行权限验证
                    else if (!string.IsNullOrEmpty(Html.CurrentPageName)
                        && Html.CurrentPageName.ToLower() != "AdminIndex".ToLower()
                        && "AdminMain".ToLower() != Html.CurrentPageName.ToLower()
                        && "AdminDefault".ToLower() != Html.CurrentPageName.ToLower()
                        && loginUser.UserType == UserType.Employee)
                    {
                        if (!loginUser.Permissions.Exists(s => s.PageName == Html.CurrentPageName))
                        {
                            HttpContext.Current.Response.Write("对不起，您没有该权限！");
                            HttpContext.Current.Response.End();
                        }
                    }
                }
            }

            
        }

        #endregion
    }
}
