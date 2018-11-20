using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Modules.Member.Models;

namespace Easpnet.Modules.Member.Pages
{
    public class Register : PageBase
    {
        protected User user;
        protected OperateResult res;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = new User();
            res = new OperateResult();

            if (Request.Form["action"] == "send")
            {
                if (string.IsNullOrEmpty(Request.Form["user.username"]) || Request.Form["user.username"].Length < 4 || Request.Form["user.username"].Length > 16)
                {
                    res.AddMessage(T("用户名不能为空，并且长度必须4-16个英文或数字或下划线"));
                }
                else
                {
                    user.UserName = Request.Form["user.username"];
                }

                if (string.IsNullOrEmpty(Request.Form["user.password"]) || Request.Form["user.password"].Length < 6 || Request.Form["user.password"].Length > 16)
                {
                    res.AddMessage(T("密码不能为空，并且长度必须6-16个字符"));
                }
                else
                {
                    if (string.IsNullOrEmpty(Request.Form["passConfirm"]))
                    {
                        res.AddMessage(T("请确认您输入的密码！"));
                    }
                    else if (Request.Form["passConfirm"].Trim() != Request.Form["user.password"].Trim())
                    {
                        res.AddMessage(T("两次密码必须输入一致！"));
                    }
                    else
                    {
                        user.Password = Request.Form["user.password"].Trim();
                    }                    
                }

                if (res.Success)
                {
                    user.RegIp = Request.UserHostAddress;
                    user.RegTime = DateTime.Now;
                    bool success = user.Register();
                    if (success)
                    {
                        Response.Redirect(Html.HrefLink("Member", "RegisterSuccess"));
                    }
                    else
                    {
                        res.AddMessage(T("对不起，注册失败，请稍后再试！"));
                    }
                }
                
            }

            //
            //AddJsFile("Themes/Default/Js/rounded-corners.js");
            //AddJsFile("Themes/Default/Js/form-field-tooltip.js");
            AddJsFile("Themes/Default/Js/formValidator/js/jquery.validationEngine-zh-cn.js");
            AddJsFile("Themes/Default/Js/formValidator/js/jquery.validationEngine.js");
        }
    }
}
