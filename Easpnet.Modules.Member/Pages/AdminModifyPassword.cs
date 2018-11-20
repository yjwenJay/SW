using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Security;
using Easpnet.Modules.Member.Models;

namespace Easpnet.Modules.Member.Pages
{
    public class AdminModifyPassword : PageBase
    {
        protected AdminUser user;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //处理请求
            Handler();
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        void Handler()
        {
            user = AdminContext.LoginUser;

            string option = Post("option");
            if (option == "save")
            {
                if (string.IsNullOrEmpty(Post("Password")))
                {
                    AddErrorMessage("请输入旧密码！");
                }

                if (string.IsNullOrEmpty(Post("PasswordNew")))
                {
                    AddErrorMessage("请输入新密码！");
                }

                if (string.IsNullOrEmpty(Post("PasswordNew2")))
                {
                    AddErrorMessage("请输入确认新密码！");
                }
                else
                {
                    if (Post("PasswordNew2") != Post("PasswordNew"))
                    {
                        AddErrorMessage("确认新密码输入不一致！");
                    }
                }

                if (MD5.MD5passwordToLower((MD5.MD5passwordToLower(Post("Password")))) != user.Password)
                {
                    AddErrorMessage("老密码输入有误，不能修改登录密码！");
                }

                //若没有错，则提交
                if (!HasError)
                {
                    if (option == "save")
                    {
                        user.Password = MD5.MD5passwordToLower((MD5.MD5passwordToLower(Post("PasswordNew"))));

                        if (user.Update())
                        {
                            AdminContext.LoginUser = user;
                            AddSuccessMessage(T("密码修改成功！"));
                        }
                        else
                        {
                            AddErrorMessage(T("修改失败，请重试！"));
                        }
                    }
                }
            }
        }


    }
}
