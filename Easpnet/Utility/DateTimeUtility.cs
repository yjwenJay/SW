using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet
{
    public static class DateTimeUtility
    {
        /// <summary>
        /// 显示完整的日志
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string FullString(DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// 是否为默认时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsDefaultTime(DateTime dt)
        {
            return dt <= TypeConvert.ToDateTime("1900-1-1");
        }

        /// <summary>
        /// 显示时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DisplayTime(DateTime dt)
        {
            if (TypeConvert.IsDefaultTime(dt))
            {
                return "--";
            }
            else
            {
                return dt.ToString("yyyy-MM-dd HH:mm:ss");

                //if (dt.Date == DateTime.Now.Date)
                //{
                //    return dt.ToString("今日 HH:mm:ss");
                //}
                //else if (dt.Date == DateTime.Now.Date.AddDays(-1))
                //{
                //    return dt.ToString("昨日 HH:mm:ss");
                //}
                //else if (dt.Date.Year == DateTime.Now.Year)
                //{
                //    return dt.ToString("MM-dd HH:mm:ss");
                //}
                //else
                //{
                //    return dt.ToString("yyyy-MM-dd HH:mm:ss");
                //}
            }
        }
    }
}
