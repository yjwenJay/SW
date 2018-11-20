
using System;
using System.Security.Cryptography;
using System.Text;
namespace Easpnet
{
    public static class ObjectExtendMethod
    {
        /// <summary>
        /// 转化为Int型数据，为空时时返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt32(this object obj)
        {
            if (obj == null)
            {
                return default(int);
            }
            else
            {
                int res;
                if (int.TryParse(obj.ToString(), out res))
                {
                    return res;
                }
                else
                {
                    return default(int);
                }
            }
        }


        /// <summary>
        /// 转化为Int型数据，为空时时返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToInt64(this object obj)
        {
            if (obj == null)
            {
                return default(long);
            }
            else
            {
                long res;
                if (long.TryParse(obj.ToString(), out res))
                {
                    return res;
                }
                else
                {
                    return default(long);
                }
            }
        }


        /// <summary>
        /// 转化为日期型，发生异常时返回默认时间，而不报错
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(obj);
                //1/1/1753 12:00:00 AM 和 12/31/9999 11:59:59 
                if (dt < Convert.ToDateTime("1/1/1753 12:00:00") || dt > Convert.ToDateTime("12/31/9999 11:59:59"))
                {
                    return "1900-1-1".ToDateTime();
                }
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return "1900-1-1".ToDateTime();
            }
        }


        /// <summary>
        /// 是否为默认时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsDefaultTime(this DateTime dt)
        {
            return dt <= "1900-1-1".ToDateTime();
        }


        /// <summary>
        /// 转化为逻辑型，发生异常时返回默认，而不报错
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBoolean(this object obj)
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
        /// 转化为浮点型，发生异常时返回默认，而不报错
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj)
        {
            if (obj == null)
            {
                return default(decimal);
            }
            else
            {
                decimal res;
                if (decimal.TryParse(obj.ToString(), out res))
                {
                    return res;
                }
                else
                {
                    return default(decimal);
                }
            }
        }

        /// <summary>
        /// 转化为实数类型，发生异常时返回默认，而不报错
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDouble(this object obj)
        {
            if (obj == null)
            {
                return default(double);
            }
            else
            {
                double res;
                if (double.TryParse(obj.ToString(), out res))
                {
                    return res;
                }
                else
                {
                    return default(double);
                }
            }
        }


        /// <summary>
        /// 反序列化
        ///  先将数据库中取出的对象反序强制转化为byte数组，再反序列化为对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object Deserialize(this object obj)
        {
            try
            {
                return SerializeHelper.Deserialize((byte[])obj);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 序列话，将一个对象序列化为byte数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Serialize(this object obj)
        {
            return SerializeHelper.Serialize(obj);
        }

        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5(this string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }

}
