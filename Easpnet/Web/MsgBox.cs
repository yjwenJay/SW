using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Web
{
    public class MsgBox
    {
        #region MsgBox
        /// <summary>
        /// 显示“确定”，点击以后就转到预设网址的提示框
        /// </summary>
        /// <param name="Msg">提示信息</param>
        /// <param name="URL">“确定”以后要转到预设网址，为空表示只提示不跳转</param>
        /// <returns>提示框JS</returns>
        public static void MsgBoxs(string strMsg, string URL)
        {
            string StrScript;
            StrScript = ("<script language=javascript>");
            StrScript += ("alert('" + strMsg + "');");
            if (URL != "")
            {
                StrScript += ("window.location='" + URL + "';");
            }
            StrScript += ("</script>");
            System.Web.HttpContext.Current.Response.Write(StrScript);
        }
        #endregion

    }
}
