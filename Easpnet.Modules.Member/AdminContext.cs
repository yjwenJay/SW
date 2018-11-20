using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Easpnet.Modules.Member.Models;

namespace Easpnet.Modules.Member
{
    /// <summary>
    /// 管理员上下文数据
    /// </summary>
    public class AdminContext
    {
        /// <summary>
        /// 获取或者设置当前登录的会员信息
        /// </summary>
        public static AdminUser LoginUser
        {
            get
            {
                //
                if (HttpContext.Current.Session["CurrentLoginAdminUser"] != null)
                {
                    return HttpContext.Current.Session["CurrentLoginAdminUser"] as AdminUser;
                }
                else
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["Environment"] == "Debug")
                    {
                        AdminUser u = new AdminUser();
                        Query q = Query.NewQuery();
                        q.Where("UserType", Symbol.EqualTo, Ralation.End, UserType.SystemAdmin);
                        if (u.GetModel(q))
                        {
                            return u;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            set
            {
                HttpContext.Current.Session["CurrentLoginAdminUser"] = value;
                if (value != null)
                {
                    HttpContext.Current.Session["CurrentLoginUserName"] = value.UserName;
                    HttpContext.Current.Session["CurrentLoginUserId"] = value.UserId;
                }
                else
                {
                    HttpContext.Current.Session["CurrentLoginUserName"] = null;
                    HttpContext.Current.Session["CurrentLoginUserId"] = null;
                }
            }
        }

        /// <summary>
        /// 验证是否已经登录
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                //调试环境
                if (System.Configuration.ConfigurationManager.AppSettings["Environment"] == "Debug")
                {
                    return true;
                }

                if (HttpContext.Current.Session["CurrentLoginAdminUser"] != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取登录用户的用户Id
        /// </summary>
        public static long LoginUserId
        {
            get
            {
                AdminUser user = LoginUser;
                if (user == null)
                {
                    return 0;
                }
                else
                {
                    return user.UserId;
                }
            }
        }


        /// <summary>
        /// 获取登录用户的用户Id
        /// </summary>
        public static string LoginUserName
        {
            get
            {
                AdminUser user = LoginUser;
                if (user == null)
                {
                    return "";
                }
                else
                {
                    return user.UserName;
                }
            }
        }


        /// <summary>
        /// 退出登录，将清空所有的会员信息
        /// </summary>
        public static void Logout()
        {
            HttpContext.Current.Session["CurrentLoginAdminUser"] = null;
        }
    }
}