using System;

namespace Easpnet
{
    /// <summary>
    /// 提供数据类型的转化
    /// 防止空值时转化错误
    /// </summary>
    public class TypeConvert
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
        /// 转化为Int型数据，为空时时返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToInt64(object obj)
        {
            try
            {
                return Convert.ToInt64(obj);
            }
            catch
            {
                return default(long);
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
                DateTime dt = Convert.ToDateTime(obj);
                //1/1/1753 12:00:00 AM 和 12/31/9999 11:59:59 
                if (dt < Convert.ToDateTime("1/1/1753 12:00:00") || dt > Convert.ToDateTime("12/31/9999 11:59:59"))
                {
                    return Convert.ToDateTime("1900-1-1");
                }
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return Convert.ToDateTime("1900-1-1");
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


        /// <summary>
        /// 是否为默认时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsDefaultTime(DateTime dt)
        {
            return dt <= TypeConvert.ToDateTime("1900-1-1") || dt == TypeConvert.ToDateTime("1970-01-01 08:00:00");
        }


        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="date">int 时间戳</param>
        /// <returns>DateTime</returns>
        public static System.DateTime ConvertIntToDateTime(int date)
        {
            System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddSeconds(date);
            return time;
        }

        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static int ConvertDateTimeToInt(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (time - startTime).TotalSeconds;
            return TypeConvert.ToInt32(intResult);
        }

    }
}
