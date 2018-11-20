using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Local
{
    /// <summary>
    /// 本地化配置
    /// </summary>
    public static class LocalConfig
    {
        //数据库类型
        private static string m_DatabaseType = "Sql";

        //连接字符串
        private static string m_ConnectionString;
        
        //平台类型，1表示游戏充值，2表示发卡，3表示二者
        private static int m_PlatformType = 3;

        //DES加密密钥(8位)
        private static string m_DesKey = "ty34ha78";

        //DES加密Flag
        private static string m_DesFlag = "xxa3xu";

        //网站网址
        private static string m_WebRootUrl;

        /// <summary>
        /// 数据库类型
        /// </summary>
        public static string DatabaseType 
        {
            get { return m_DatabaseType; }
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get 
            {                
                if (string.IsNullOrEmpty(m_ConnectionString))
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DB_ReadWrite_ConnString"]))
                    {
                        m_ConnectionString = ConfigurationManager.AppSettings["DB_ReadWrite_ConnString"];
                    }
                    else
                    {
                        m_ConnectionString = "未设置链接字符串";
                    }
                }

                return m_ConnectionString; 
            }
        }
        
        /// <summary>
        /// 置顶平台类型
        /// </summary>
        public static int PlatformType 
        {
            get { return m_PlatformType; } 
        }

        /// <summary>
        /// DES加密密钥
        /// </summary>
        public static string DesKey
        {
            get { return m_DesKey; }
        }

        /// <summary>
        /// DES加密Flag
        /// </summary>
        public static string DesFlag
        {
            get { return m_DesFlag; }
        }

        //网站网址
        public static string WebRootUrl
        {
            get 
            {
                if (string.IsNullOrEmpty(m_WebRootUrl))
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["WebRootUrl"]))
                    {
                        m_WebRootUrl = ConfigurationManager.AppSettings["WebRootUrl"];
                    }
                    else
                    {
                        m_WebRootUrl = "未设置网站根URL";
                    }
                }
                
                return m_WebRootUrl; 
            }
        }


    }
}
