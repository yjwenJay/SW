using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.IO;
using SW.Commons;

namespace SW
{
    public static class ObjectExtendMethod
    {
        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string Md5Password(this string pwd)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToLower();
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="input">加密原串</param>
        /// <param name="encryptKey">加密密钥</param>
        /// <returns>加密结果</returns>
        public static string DESEncryption(this string input, string encryptKey)
        {
            try
            {
                byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };      //当模式为ECB时，IV无用
                byte[] a =SW.Commons.Security.DES.Des3EncodeECB(System.Text.ASCIIEncoding.Default.GetBytes(encryptKey), iv, System.Text.ASCIIEncoding.Default.GetBytes(input));
                return SW.Commons.Security.DES.byteToHexStr(a);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="input">解密原串</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <returns>解密结果</returns>
        public static string DESDecryption(this string input, string decryptKey)
        {
            try
            {
                string result = string.Empty;
                byte[] m = SW.Commons.Security.DES.hexStrToStr(input);
                byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };      //当模式为ECB时，IV无用
                byte[] p = SW.Commons.Security.DES.Des3DecodeECB(System.Text.ASCIIEncoding.Default.GetBytes(decryptKey), iv, m);
                result = System.Text.ASCIIEncoding.Default.GetString(p);
                result = result.Replace("\0", "");
                return result;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }


        public static string ConvertEncoding(this string strInput, Encoding fromEncoding, Encoding destEncoding)
        {
            byte[] Cbyte = null;
            byte[] bytInput = destEncoding.GetBytes(strInput);
            Cbyte = Encoding.Convert(destEncoding, fromEncoding, bytInput);
            return (destEncoding.GetString(Cbyte));
        }

        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(this string encryptStr, string key)
        {
            if (string.IsNullOrEmpty(encryptStr))
                return "";
            byte[] byKey = null;
            try
            {
                string encryptKeyall = key;
                if (encryptKeyall.Length < 9)
                {
                    for (;;)
                    {
                        if (encryptKeyall.Length < 9)
                            encryptKeyall += encryptKeyall;
                        else
                            break;
                    }
                }
                byKey = System.Text.Encoding.UTF8.GetBytes(encryptKeyall.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptStr);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="decryptStr"></param>
        /// <param name="key"></param>
        /// <returns></returns> 
        public static string Decrypt(this string decryptStr, string key)
        {
            if (string.IsNullOrEmpty(decryptStr))
                return "";

            byte[] byKey = null;

            byte[] inputByteArray = new Byte[decryptStr.Length];

            try
            {
                string encryptKeyall = key;
                if (encryptKeyall.Length < 9)
                {
                    for (;;)
                    {
                        if (encryptKeyall.Length < 9)
                            encryptKeyall += encryptKeyall;
                        else
                            break;
                    }
                }
                byKey = System.Text.Encoding.UTF8.GetBytes(encryptKeyall.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(decryptStr);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = new System.Text.UTF8Encoding();
                return encoding.GetString(ms.ToArray());
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 转化为short型数据，为空时时返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static short ToInt16(this object obj)
        {
            try
            {
                return Convert.ToInt16(obj);
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
        public static int ToInt32(this object obj)
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
        public static long ToInt64(this object obj)
        {
            try
            {
                long tmp;
                if (long.TryParse(obj.ToString(), out tmp))
                {
                    return tmp;
                }
                else
                {
                    return default(long);
                }
            }
            catch
            {
                return default(long);
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
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return "1900-1-1".ToDateTime();
                //return default(DateTime);
            }
        }

        /// <summary>
        /// 转化为日期型，发生异常时返回默认时间，而不报错
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this double d)
        {
            System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddSeconds(d);
            return time;
        }

        /// <summary>
        /// 转化为日期型，发生异常时返回默认时间，而不报错
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj, string format)
        {
            try
            {
                DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
                dtFormat.ShortDatePattern = format;
                return Convert.ToDateTime(obj.ToString(), dtFormat);
            }
            catch
            {
                return "1900-1-1".ToDateTime();
                //return default(DateTime);
            }
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
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDecimal2(this object obj)
        {
            try
            {
                decimal f = Convert.ToDecimal(obj);
                int i = (int)(f * 100);
                f = (decimal)(i * 1.0) / 100;
                return f;
            }
            catch
            {
                return default(decimal);
            }

        }

        /// <summary>
        /// 转化为实数类型，发生异常时返回默认，而不报错
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDouble(this object obj)
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
        /// 正则匹配，若匹配不到，则返回空字符串
        /// </summary>
        /// <param name="content"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string GetRegexValue(this string content, string pattern)
        {
            try
            {
                Match match = Regex.Match(content, pattern, RegexOptions.Singleline);
                return match.Success ? match.Value : "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string HideMobile(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            if (input.Length == 11)
            {
                string hidemoblile = "";
                hidemoblile = input.Substring(0, 3) + "****" + input.Substring(7, 4);
                return hidemoblile;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 将Json对象序列化为XML字符串
        /// </summary>
        /// <param name="masterXElement">主XElement</param>
        /// <param name="detailsXElement">明细XElement</param>
        /// <param name="json">要序列化的对象</param>
        /// <returns>序列化产生的XML</returns>
        public static string ToXml(string masterXElement, string detailsXElement, string json)
        {
            XElement masterXElements = new XElement(masterXElement);

            if (!string.IsNullOrEmpty(json) && json != "[{}]")
            {
                var list = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
                foreach (var items in list)
                {
                    XElement detailsXElements = new XElement(detailsXElement);
                    detailsXElements.Add(items.Select(item => new XElement(item.Key, string.IsNullOrEmpty(item.Value) ? null : item.Value)));
                    masterXElements.Add(detailsXElements);
                }
            }
            else
            {
                return "";
            }

            return masterXElements.ToString();
        }
    }

}
