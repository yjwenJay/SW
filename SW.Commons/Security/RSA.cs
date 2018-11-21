using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SW.Security
{
    public class RSA
    {
        #region
        /// <summary>  
        /// RSA获取公钥私钥  
        /// </summary>  
        /// <param name="str_PrivateKey"></param>  
        /// <param name="str_PublicKey"></param>  
        public static void RSACreateKey(ref string str_PublicKey, ref string str_PrivateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            //str_PublicKey = Convert.ToBase64String(RSA.ExportCspBlob(false));  
            //str_PrivateKey = Convert.ToBase64String(RSA.ExportCspBlob(true));  


            str_PublicKey = rsa.ToXmlString(false);
            str_PrivateKey = rsa.ToXmlString(true);
        }


        /// <summary>  
        /// RSA加密  
        /// </summary>  
        /// <param name="source"></param>  
        /// <param name="str_PrivateKey"></param>  
        /// <param name="str_PublicKey"></param>  
        /// <returns></returns>  
        public static string RSAEncrypt(string source, string str_PublicKey)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();


                rsa.FromXmlString(str_PublicKey);


                //str_PublicKey = Convert.ToBase64String(RSA.ExportCspBlob(false));  
                //str_PrivateKey = Convert.ToBase64String(RSA.ExportCspBlob(true));  
                byte[] DataToEncrypt = Encoding.UTF8.GetBytes(source);


                byte[] bs = rsa.Encrypt(DataToEncrypt, false);
                //str_PublicKey = Convert.ToBase64String(RSA.ExportCspBlob(false));  
                //str_PrivateKey = Convert.ToBase64String(RSA.ExportCspBlob(true));  
                string encrypttxt = Convert.ToBase64String(bs);


                return encrypttxt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        /// <summary>  
        /// RSA解密  
        /// </summary>  
        /// <param name="strRSA"></param>  
        /// <param name="str_PrivateKey"></param>  
        /// <returns></returns>  
        public static string RSADecrypt(string strRSA, string str_PrivateKey)
        {
            try
            {
                byte[] DataToDecrypt = Convert.FromBase64String(strRSA);
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();


                rsa.FromXmlString(str_PrivateKey);
                //byte[] bsPrivatekey = Convert.FromBase64String(str_PrivateKey);  
                //RSA.ImportCspBlob(bsPrivatekey);  


                byte[] bsdecrypt = rsa.Decrypt(DataToDecrypt, false);


                string strRE = Encoding.UTF8.GetString(bsdecrypt);
                return strRE;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        #endregion  
    }
}
