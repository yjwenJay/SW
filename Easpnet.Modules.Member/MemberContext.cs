using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Easpnet.Modules.Member.Models;

namespace Easpnet.Modules.Member
{
    /// <summary>
    /// 封装会员登录的相关信息
    /// </summary>
    public class MemberContext
    {
        /// <summary>
        /// 获取或者设置当前登录的会员信息
        /// </summary>
        public static User LoginUser
        {
            get 
            {
                //
                if (HttpContext.Current.Session["CurrentLoginUser"] != null)
                {
                    return HttpContext.Current.Session["CurrentLoginUser"] as User;
                }
                else
                {
                    return null;
                }
            }
            set 
            { 
                HttpContext.Current.Session["CurrentLoginUser"] = value;                
            }
        }

        /// <summary>
        /// 验证是否已经登录
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                if (HttpContext.Current.Session["CurrentLoginUser"] != null)
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
                User user = LoginUser;
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
                User user = LoginUser;
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
            HttpContext.Current.Session["CurrentLoginUser"] = null;
        }
    }
}
