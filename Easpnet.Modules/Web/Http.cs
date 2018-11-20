using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Runtime.InteropServices;

namespace Easpnet.Modules.Web
{
    public class Http
    {
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);


        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string ip;

            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) // 服务器， using proxy
            {
                //  得到真实的客户端地址
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // Return real client IP.
            }
            else//如果没有使用代理服务器或者得不到客户端的ip not using proxy or can't get the Client IP
            {

                // 得到服务端的地址
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP.
            }

            return ip;
        }

        /// <summary>
        /// 获取客户端Mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientMac()
        {
            string userip = HttpContext.Current.Request.UserHostAddress;
            string strClientIP = HttpContext.Current.Request.UserHostAddress.ToString().Trim();
            Int32 ldest = inet_addr(strClientIP); //目的地的ip 
            Int32 lhost = inet_addr("");   //本地服务器的ip 
            Int64 macinfo = new Int64();
            Int32 len = 6;
            int res = SendARP(ldest, 0, ref macinfo, ref len);
            string mac_src = macinfo.ToString("X");
            if (mac_src == "0")
            {
                return userip;
            }
            else
            {
                string mac_dest = "";

                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }

                return mac_dest;
            }            
        }
    }
}
