using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Modules.Member.Models;
using Easpnet.Security;

namespace Easpnet.Modules.Member.Pages
{
    public class Login : PageBase
    {
        protected User user;
        protected OperateResult res;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new User();
            res = new OperateResult();
            if (Request.Form["action"] == "send")
            {
                if (string.IsNullOrEmpty(Request.Form["username"]))
                {
                    AddErrorMessage("请输入用户名！");
                }
                else
                {
                    user.UserName = Request.Form["username"];
                }

                if (string.IsNullOrEmpty(Request.Form["password"]))
                {
                    AddErrorMessage("请输入密码！");
                }
                else
                {
                    user.Password = Request.Form["password"];
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
                            bool success = user.Login();
                            if (success)
                            {
                                if (!string.IsNullOrEmpty(Request["return"]))
                                {
                                    string ret = "";
                                    ret = DES.Decrypt(Request["return"]);
                                    Response.Redirect(ret);
                                }
                                else
                                {
                                    Response.Redirect(Html.WebRootUrl);
                                }
                            }
                            else
                            {
                                AddErrorMessage(T("您提供的帐号或密码有误，不能为您登录！"));
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
