using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Security
{
    public class MD5
    {

        #region MD5加密

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string MD5password(string pass)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "MD5");
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string MD5passwordToLower(string pass)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "MD5").ToLower();
        }

        #endregion
    }
}
