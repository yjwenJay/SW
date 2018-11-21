using System;
using System.Collections.Generic;
using System.Text;

namespace SW
{
    /// <summary>
    /// 提供数据类型的转化
    /// 防止空值时转化错误
    /// </summary>
    public class DbConvert
    {        
        /// <summary>
        /// 转化为Int型数据，为空时时返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt32(object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return default(int);
            }
        }


        /// <summary>
        /// 转化为字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(object obj)
        {
            try
            {
                return Convert.ToString(obj);
            }
            catch
            {
                return default(string);
            }
        }


        /// <summary>
        /// 转化为日期型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return default(DateTime);
            }
        }


        /// <summary>
        /// 转化为逻辑性数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBoolean(object obj)
        {
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch
            {
                return default(bool);
            }
        }


        /// <summary>
        /// 转化为实数类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDouble(object obj)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch
            {
                return default(double);
            }
        }

        public static decimal ToDecimal(object obj)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return default(decimal);
            }
        }
    }
}
