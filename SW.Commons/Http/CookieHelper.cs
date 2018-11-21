using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SW.Http
{
    public class CookieHelper
    {
        private static string cookieDomain = "";
        /// <summary>
        /// 保存时的Domain
        /// </summary>
        public static string CookieDomain
        {
            get { return CookieHelper.cookieDomain; }
            set { CookieHelper.cookieDomain = value; }
        }

        #region 读取
        /// <summary>
        /// 读取HttpCookie 可能为Null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HttpCookie Get(string name)
        {
            return HttpContext.Current.Request.Cookies[name];
        }

        /// <summary>
        /// 读取Cookie值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetValue(string name)
        {
            HttpCookie _cookie = Get(name);
            if (_cookie != null)
            {
                return _cookie.Value;
            }
            return "";
        }
        #endregion

        #region 删除
        public static void Remove(string name)
        {
            Remove(Get(name));
        }

        public static void Remove(HttpCookie cookie)
        {
            if (cookie != null)
            {
                if (!string.IsNullOrEmpty(cookieDomain))
                    cookie.Domain = cookieDomain;
                cookie.Values.Clear();
                cookie.Value = "is deleted";
                cookie.Expires = DateTime.Now.AddMonths(10);
                HttpContext.Current.Response.Cookies.Set(cookie);
                HttpContext.Current.Request.Cookies.Set(cookie);
            }
        }
        #endregion

        #region 新增
        /// <summary>
        /// 保存Cookie
        /// </summary>
        /// <param name="cookie"></param>
        public static void Save(HttpCookie cookie)
        {
            if (Get(cookie.Name) != null)
            {
                Set(cookie);
            }
            else
                Add(cookie);
        }

        /// <summary>
        /// 新增Cookie  一般使用Save
        /// </summary>
        /// <param name="cookie"></param>
        public static void Add(HttpCookie cookie)
        {
            cookie.Path = "/";
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 新增并保存Cookie
        /// </summary>
        /// <param name="cookiename">名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">过期时间</param>
        public static HttpCookie Add(string cookiename, string value, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookiename);
            cookie.Expires = expires;
            cookie.Value = value;
            cookie.Path = "/";
            if (!string.IsNullOrEmpty(cookieDomain))
                cookie.Domain = cookieDomain;
            Save(cookie);
            return cookie;
        }

        /// <summary>
        /// 新增并保存Cookie 默认一天
        /// </summary>
        /// <param name="cookiename">名称</param>
        /// <param name="value">值</param>
        public static HttpCookie Add(string cookiename, string value)
        {
            return Add(cookiename, value, DateTime.Now.AddDays(1.0));
        }

        /// <summary>
        /// 创建无值Cookie 多用于多值Cookie 默认一天
        /// </summary>
        /// <param name="cookiename"></param>
        /// <returns></returns>
        public static HttpCookie Add(string cookiename, int saveMinu = 24 * 60)
        {
            HttpCookie cookie = new HttpCookie(cookiename);
            cookie.Expires = DateTime.Now.AddMinutes(saveMinu);
            cookie.Path = "/";
            if (!string.IsNullOrEmpty(cookieDomain))
                cookie.Domain = cookieDomain;
            return cookie;
        }

        /// <summary>
        /// 新增无值Cookie 多用于多值Cookie 默认一天
        /// </summary>
        /// <param name="cookiename"></param>
        /// <returns></returns>
        public static HttpCookie Set(string cookiename)
        {
            return Add(cookiename);
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改Cookie
        /// </summary>
        /// <param name="cookie"></param>
        public static void Set(HttpCookie cookie)
        {
            if (cookie != null)
                HttpContext.Current.Response.Cookies.Set(cookie);
        }

        /// <summary>
        /// 修改Cookie (无则增加)
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="value"></param>
        public static HttpCookie Set(string cookiename, string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Value = value;
                Set(cookie);
                return cookie;
            }
            else
            {
                return Add(cookiename, value, DateTime.Now.AddDays(1.0));
            }
        }
        #endregion
    }
}
