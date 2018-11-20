using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Modules.Member.Models;
using System.Runtime.InteropServices;
using Easpnet.Security;

namespace Easpnet.Modules.Member.Pages
{
    public class AdminLogin : PageBase
    {
        protected AdminUser user;

        protected void Page_Load(object sender, EventArgs e)
        {
            //退出
            if (Request.QueryString["logout"] == "1")
            {
                AdminContext.Logout();
            }

            //
            user = new AdminUser();
            if (Request.Form["action"] == "send")
            {
                if (string.IsNullOrEmpty(Request.Form["username"]))
                {
                    AddErrorMessage(T("请输入用户名!"));
                }

                if (string.IsNullOrEmpty(Request.Form["password"]))
                {
                    AddErrorMessage(T("请输入密码!"));
                }
                
                
                if (!HasError)
                {
                    if (Session["CheckCodeValue"] == null)
                    {
                        AddErrorMessage("验证码已过期，请重新登陆！");
                    }
                    else
                    {
                        //
                        if (Post("CodeValue") == Session["CheckCodeValue"].ToString())
                        {                            
                            user.UserName = Request.Form["username"];
                            user.Password = Request.Form["password"];               //解密密码
                            user.Password = MD5.MD5passwordToLower(MD5.MD5passwordToLower(user.Password));
                            string res = "";
                            bool success = user.AdminLogin("", out res);
                            if (success)
                            {
                                Response.Redirect(Html.HrefLink("Member", "AdminIndex"));
                            }
                            else
                            {
                                AddErrorMessage(T(res));
                            }
                        }
                        else
                        {
                            AddErrorMessage("验证码输入有误！");
                        }
                    }
                }

            }
        }
    }
}
