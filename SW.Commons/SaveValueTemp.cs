using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SW.Commons.Http;

namespace SW.Commons
{
    /// <summary>
    /// 使用session,cookie保存用户临时数据，
    /// </summary>
    public class SaveValueTemp
    {
        public static string prefix = "svt_";
        /// <summary>
        /// 保存用户临时数据
        /// </summary>
        public static void SaveValue(string name, string value, int saveMinu, bool saveCookie, params string[] keyValue)
        {
            //保存session
            SessionState.Set(prefix + name, value);

            HttpCookie cookie = null;
            if (saveCookie)
            {
                //保存cookie
                cookie = CookieHelper.Add("SaveValueTemp", saveMinu);
                cookie[name] = value;
            }

            if (keyValue != null && keyValue.Length > 0 && keyValue.Length % 2 == 0)
            {
                try
                {
                    for (int i = 0; i < keyValue.Length; i += 2)
                    {
                        string key = string.IsNullOrEmpty(keyValue[i]) ? "" : keyValue[i];
                        string val = string.IsNullOrEmpty(keyValue[i + 1]) ? "" : keyValue[i + 1];
                        if (!string.IsNullOrEmpty(key))
                        {
                            if (saveCookie)
                                cookie[prefix + key] = HttpUtility.UrlEncode(val);
                            SessionState.Set(prefix + key, HttpUtility.UrlEncode(val));
                        }
                    }
                }
                catch { }
            }
            if (saveCookie)
                CookieHelper.Save(cookie);
        }

        public static void SaveValue(string name, string value, params string[] keyValue)
        {
            SaveValue(name, value, 26 * 60, true, keyValue);
        }

        /// <summary>
        /// 读取临时数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ReadValue(string name)
        {
            //string value = SessionState.Get(prefix + name) != null ? SessionState.Get(prefix + name).ToString() : "";
            if (SessionState.Get(prefix + name) != null)
                return SessionState.Get(prefix + name).ToString();
            else
            {
                HttpCookie cookie = CookieHelper.Get("SaveValueTemp");
                if (cookie != null)
                    return cookie[prefix + name];
            }
            return "";
        }

        /// <summary>
        /// 删除用户临时数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="keys"></param>
        public static void RemoveValue(string name, params string[] keys)
        {
            SessionState.Remove(prefix + name);
            HttpCookie cookie = CookieHelper.Set("SaveValueTemp");
            cookie[prefix + name] = "";
            cookie.Expires = DateTime.Now.AddDays(1);
            if (keys != null && keys.Length > 0)
            {
                try
                {
                    foreach (string key in keys)
                    {
                        if (!string.IsNullOrEmpty(key))
                        {
                            cookie[prefix + key] = "";
                            SessionState.Remove(prefix + key);
                        }
                    }
                }
                catch { }
            }
            CookieHelper.Save(cookie);
        }

        public static void RemoveAll()
        {
            SessionState.RemoveAll();
            HttpCookie cookie = CookieHelper.Get("SaveValueTemp");
            if (cookie != null)
            {
                cookie.Value = "";
                cookie.Expires = DateTime.Now.AddDays(1);
                CookieHelper.Save(cookie);
            }
        }
    }
}
