using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SW
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringExtendMethod
    {
        /// <summary>
        /// 判断字符串是否为空
        /// 为空时返回true、否则返回false
        /// </summary>
        /// <param name="s"></param>
        /// <returns>为空时返回true、否则返回false</returns>
        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 判断字符串是否有值 等于!IsEmpty()
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool HasValue(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 判断字符串是否为int
        /// 为int 时返回true、否则返回false
        /// </summary>
        /// <param name="s"></param>
        /// <returns>为int 时返回true、否则返回false</returns>
        public static bool IsInt(this string s)
        {
            int i;
            bool b = int.TryParse(s, out i);
            return b;
        }

        /// <summary>
        /// 将字符串数据转换为整形数组
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static decimal[] ToDecimalArray(this string[] content)
        {
            decimal[] c = new decimal[content.Length];
            for (int i = 0; i < content.Length; i++)
            {
                c[i] = content[i].ToDecimal();
            }
            return c;
        }

        /// <summary>
        /// 扩展方法用来判断字符串是不是Email形式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmail(this string s)
        {
            //Regex r = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");     
            Regex r = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
            return r.IsMatch(s);
        }

        /// <summary>
        /// 以字符串为分隔符的Split方法
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>数组</returns>
        public static string[] SplitString(string str, string separator)
        {
            string tmp = str;
            Hashtable ht = new Hashtable();
            int i = 0;
            int pos = tmp.IndexOf(separator);
            while (pos != -1)
            {
                ht.Add(i, tmp.Substring(0, pos));
                tmp = tmp.Substring(pos + separator.Length);
                pos = tmp.IndexOf(separator);
                i++;
            }
            ht.Add(i, tmp);
            string[] array = new string[ht.Count];
            for (int j = 0; j < ht.Count; j++)
                array[j] = ht[j].ToString();

            return array;
        }

        ///// <summary>  
        ///// 字符串转换成16进制字符串  
        ///// </summary>  
        ///// <param name="bytes"></param>  
        ///// <returns></returns>  
        //public static byte[] StringToHexByte(this string hexString)
        //{
        //    hexString = hexString.Replace(" ", "");
        //    if ((hexString.Length % 2) != 0)
        //        hexString += " ";
        //    byte[] returnBytes = new byte[hexString.Length / 2];
        //    for (int i = 0; i < returnBytes.Length; i++)
        //        returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
        //    return returnBytes;
        //}


        /// <summary>  
        /// 字符串转换成16进制字符串  
        /// </summary>  
        /// <param name="bytes"></param>  
        /// <returns></returns>  
        //public static string StringToHexByte(this string hexString)
        //{

        //}

        public static string ByteArrayToHexString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder(Bytes.Length * 2);
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }



        public static string HexStringToString(string hs, Encoding encode)
        {
            //以%分割字符串，并去掉空字符
            string[] chars = hs.Split(new char[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] b = new byte[chars.Length];
            //逐个字符变为16进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                b[i] = Convert.ToByte(chars[i], 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }


        /// <summary>  
        /// 字节数组转16进制字符串  
        /// </summary>  
        /// <param name="bytes"></param>  
        /// <returns></returns>  
        public static string ByteToHexString(this byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }



        /// <summary>  
        /// 字符串转换成16进制字符串  
        /// </summary>  
        /// <param name="bytes"></param>  
        /// <returns></returns>  
        public static byte[] StringToHexByte(byte[] b)
        {
            //hexString = hexString.Replace(" ", "");
            //if ((hexString.Length % 2) != 0)
            //    hexString += " ";
            byte[] returnBytes = new byte[b.Length];
            for (int i = 0; i < b.Length; i++)
                returnBytes[i] = Convert.ToByte(b[i].ToString(), 16);
            return returnBytes;
        }




        #region 随机数
        /// <summary>
        /// 随机数
        /// </summary>
        /// <param name="RandNumLength">多少个</param>
        /// <returns></returns>
        public static string GetRandom(int RandNumLength)
        {
            Random randNum = new Random(unchecked((int)DateTime.Now.Ticks));
            StringBuilder sb = new StringBuilder(RandNumLength);
            for (int i = 0; i < RandNumLength; i++)
            {
                sb.Append(randNum.Next(0, 9));
            }
            return sb.ToString();
        }
        #endregion
    }
}
