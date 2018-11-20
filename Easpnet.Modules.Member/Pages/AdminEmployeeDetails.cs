using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Modules.Member.Models;
using System.Web.Security;
using Easpnet.Security;

namespace Easpnet.Modules.Member.Pages
{
    public class AdminEmployeeDetails : PageBase
    {
        protected AdminUser user;
        protected string back_url = "";
        protected bool is_edit = false;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            back_url = Html.HrefLink("Member", "AdminEmployeeManage");

            //获取用户信息，若没有获取到，则跳转到用户列表页
            user = new AdminUser();
            if (!string.IsNullOrEmpty(Get("user_id")))
            {                
                user.UserId = TypeConvert.ToInt64(Get("user_id"));
                if (user.GetModel() || user.UserType == UserType.Employee)
                {
                    is_edit = true;
                }
            }
            else
            {
                is_edit = false;
            }            

            //处理请求
            Handler();
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        void Handler()
        {
            string option = Post("option");
            if (option == "save" || option == "insert")
            {
                if (option == "insert")
                {
                    //验证
                    if (string.IsNullOrEmpty(Post("UserName")))
                    {
                        AddErrorMessage("请输入用户名！");
                    }
                    else
                    {
                        user.UserName = Post("UserName");
                    }
                }

                if (!is_edit && string.IsNullOrEmpty(Post("Password")))
                {
                    AddErrorMessage("请输入密码！");
                }

                //                
                user.RealName = Post("RealName");
                user.GroupName = Post("GroupName");
                user.Mobile = Post("Mobile");
                user.Email = Post("Email");
                
                if (user.Password != Post("Password"))
                {
                    user.Password = MD5.MD5passwordToLower(MD5.MD5passwordToLower(Request.Form["password"]));
                }

                //二级密码
                if (!string.IsNullOrEmpty(Post("SecondPassword")))
                {                    
                    if (user.SecondPassword != Post("SecondPassword"))
                    {
                        user.SecondPassword = MD5.MD5passwordToLower(MD5.MD5passwordToLower(Post("SecondPassword")));
                    }
                }
                else
                {
                    user.SecondPassword = "";
                }

                //若没有错，则提交
                if (!HasError)
                {
                    if (option == "save")
                    {
                        if (user.Update())
                        {
                            Response.Redirect(back_url);
                        }
                        else
                        {
                            AddErrorMessage(T("保存失败，请重试！"));
                        }
                    }
                    else
                    {
                        user.UserType = UserType.Employee;  //用户类型
                        if (user.ExistsUser(user.UserName))
                        {
                            AddErrorMessage(T("用户名已经存在了！"));
                        }
                        else
                        {
                            if (user.Register())
                            {
                                Response.Redirect(back_url);
                            }
                            else
                            {
                                AddErrorMessage(T("添加用户失败，请重试！"));
                            }
                        }

                    }
                }                
            }
        }

    }
}
