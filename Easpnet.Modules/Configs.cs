using System.Collections.Specialized;
using Easpnet.Modules.Models;

namespace Easpnet.Modules
{
    public static class Configs
    {
        private static NameValueCollection m_ConfigList;

        public static void ReadConfigs()
        {
            m_ConfigList = Config.GetConfigs();
        }


        /// <summary>
        /// 获取配置项的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string key)
        {
            return Config.GetConfigValue(key);
        }


        /// <summary>
        /// 获取配置项的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetDecimal(string key)
        {
            return TypeConvert.ToDecimal(Get(key));
        }

        /// <summary>
        /// 获取配置项的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            return TypeConvert.ToInt32(Get(key));
        }

        /// <summary>
        /// 获取配置项的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetBoolean(string key)
        {
            return TypeConvert.ToBoolean(Get(key));
        }

    }
}
