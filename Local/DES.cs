using System;
using System.Collections.Generic;

using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Local
{
    public class DES
    {
        private static readonly String sKey = Local.LocalConfig.DesKey;

        private static readonly string flag = Local.LocalConfig.DesFlag;


        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        #region DES加密
        /// <summary>
        ///  DEC   加密过程 
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string pToEncrypt)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(sKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt + DES.flag);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                string result = Convert.ToBase64String(mStream.ToArray());
                result = result.Replace("+", "|");
                result = result.Replace("=", "~");
                return result;
            }
            catch
            {
                return pToEncrypt;
            }
        }
        #endregion

        #region DES解密
        /// <summary>
        ///  DEC   解密过程
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Decrypt(string pToDecrypt)
        {
            pToDecrypt = pToDecrypt.Replace("|", "+");
            pToDecrypt = pToDecrypt.Replace("~", "=");
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(sKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray()).Replace(DES.flag, string.Empty);
            }
            catch
            {
                return pToDecrypt.Replace(DES.flag, string.Empty);
            }
        }
        #endregion

    }
}
