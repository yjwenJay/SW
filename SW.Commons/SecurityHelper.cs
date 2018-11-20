/**********************************************************************
 * * 文 件 名： SecurityHelper
 * * 文件编号： 01
 * * 创 建 人： Jordan
 * * 日    期： 2006-12-02
 * * 修 改 人： Jordan
 * * 修改日期： 2006-12-02
 * * 描    述： 安全、加密等通用操作类
 * * Copyright (c) 2005 - 2006
 * ********************************************************************/
using System;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace SW.Commons
{
	/// <summary>
	/// MySecurity 的摘要说明。
	/// </summary>
    public class SecurityHelper
	{
		/// <summary>
		/// 初始化安全类
		/// </summary>
		public SecurityHelper()
		{
			key = "0123456789";
		}
		private string key; //默认密钥

		private byte[] sKey;
		private byte[] sIV;

		/// <summary>
		/// 加密字符串
		/// </summary>
		/// <param name="inputStr">输入字符串</param>
		/// <param name="keyStr">密码，可以为“”</param>
		/// <returns>输出加密后字符串</returns>
		static public string SEncryptString(string inputStr,string keyStr)
		{
            SecurityHelper ws = new SecurityHelper();
			return ws.EncryptString(inputStr,keyStr);
		}
		/// <summary>
		/// 加密字符串 密钥为系统默认
		/// </summary>
		/// <param name="inputStr">输入字符串</param>
		/// <returns>输出加密后字符串</returns>
		static public string SEncryptString(string inputStr)
		{
            SecurityHelper ws = new SecurityHelper();
			return ws.EncryptString(inputStr,"");
		}
		/// <summary>
		/// 加密字符串
		/// </summary>
		/// <param name="inputStr">输入字符串</param>
		/// <param name="keyStr">密码，可以为“”</param>
		/// <returns>输出加密后字符串</returns>
		public string EncryptString(string inputStr,string keyStr)
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider(); 
			if(keyStr=="")
				keyStr=key;
			byte[] inputByteArray = Encoding.Default.GetBytes(inputStr);
			byte[] keyByteArray=Encoding.Default.GetBytes(keyStr);
			SHA1 ha=new SHA1Managed();
			byte[] hb=ha.ComputeHash(keyByteArray);
			sKey=new byte[8];
			sIV=new byte[8];
			for(int i=0;i<8;i++)
				sKey[i]=hb[i];
			for(int i=8;i<16;i++)
				sIV[i-8]=hb[i];
			des.Key=sKey;
			des.IV=sIV;
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms,des.CreateEncryptor(),CryptoStreamMode.Write);  
			cs.Write(inputByteArray, 0, inputByteArray.Length);  
			cs.FlushFinalBlock();  
			StringBuilder ret = new  StringBuilder();  
			foreach(byte b in ms.ToArray())  
			{  
				ret.AppendFormat("{0:X2}", b);  
			}  
			cs.Close();
			ms.Close();
			return  ret.ToString();  
		}

		/// <summary>
		/// 加密文件
		/// </summary>
		/// <param name="filePath">输入文件路径</param>
		/// <param name="savePath">加密后输出文件路径</param>
		/// <param name="keyStr">密码，可以为“”</param>
		/// <returns></returns>  
		public bool EncryptFile(string filePath,string savePath,string keyStr)
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider(); 
			if(keyStr=="")
				keyStr=key;
			FileStream fs=File.OpenRead(filePath);
			byte[] inputByteArray =new byte[fs.Length]; 
			fs.Read(inputByteArray,0,(int)fs.Length);
			fs.Close();
			byte[] keyByteArray=Encoding.Default.GetBytes(keyStr);
			SHA1 ha=new SHA1Managed();
			byte[] hb=ha.ComputeHash(keyByteArray);
			sKey=new byte[8];
			sIV=new byte[8];
			for(int i=0;i<8;i++)
				sKey[i]=hb[i];
			for(int i=8;i<16;i++)
				sIV[i-8]=hb[i];
			des.Key=sKey;
			des.IV=sIV;
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms,des.CreateEncryptor(),CryptoStreamMode.Write);  
			cs.Write(inputByteArray, 0, inputByteArray.Length);  
			cs.FlushFinalBlock();
			fs=File.OpenWrite(savePath);
			foreach(byte b in ms.ToArray())  
			{  
				fs.WriteByte(b);  
			} 
			fs.Close();
			cs.Close();
			ms.Close();
			return true;
		}

		/// <summary>
		/// 解密字符串
		/// </summary>
		/// <param name="inputStr">要解密的字符串</param>
		/// <param name="keyStr">密钥</param>
		/// <returns>解密后的结果</returns>
		public string DecryptString(string inputStr,string keyStr)
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            if (keyStr == string.Empty)
				keyStr = key;
			byte[] inputByteArray = new byte[inputStr.Length / 2];  
			for(int x = 0; x < inputStr.Length / 2; x++)  
			{  
				int i = (Convert.ToInt32(inputStr.Substring(x * 2, 2), 16));  
				inputByteArray[x]  =  (byte)i;  
			}  
			byte[] keyByteArray = Encoding.Default.GetBytes(keyStr);
			SHA1 ha = new SHA1Managed();
			byte[] hb = ha.ComputeHash(keyByteArray);
			sKey = new byte[8];
			sIV = new byte[8];
			for(int i = 0;i < 8;i++)
				sKey[i] = hb[i];
			for(int i = 8;i < 16;i++)
				sIV[i - 8] = hb[i];
			des.Key = sKey;
			des.IV = sIV;
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms,des.CreateDecryptor(),CryptoStreamMode.Write);  
			cs.Write(inputByteArray, 0, inputByteArray.Length);  
			cs.FlushFinalBlock();  
			StringBuilder ret = new StringBuilder();  
			return System.Text.Encoding.Default.GetString(ms.ToArray());  
		}

		/// <summary>
		/// 解密文件
		/// </summary>
		/// <param name="filePath">输入文件路径</param>
		/// <param name="savePath">解密后输出文件路径</param>
		/// <param name="keyStr">密码，可以为“”</param>
		/// <returns></returns>    
		public bool DecryptFile(string filePath,string savePath,string keyStr)
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider(); 
			if(keyStr == string.Empty)
				keyStr = key;
			FileStream fs = File.OpenRead(filePath);
			byte[] inputByteArray = new byte[fs.Length]; 
			fs.Read(inputByteArray,0,(int)fs.Length);
			fs.Close();
			byte[] keyByteArray = Encoding.Default.GetBytes(keyStr);
			SHA1 ha = new SHA1Managed();
			byte[] hb = ha.ComputeHash(keyByteArray);
			sKey = new byte[8];
			sIV = new byte[8];
			for(int i = 0;i < 8;i++)
				sKey[i] = hb[i];
			for(int i = 8;i < 16;i++)
				sIV[i - 8] = hb[i];
			des.Key = sKey;
			des.IV = sIV;
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms,des.CreateDecryptor(),CryptoStreamMode.Write);  
			cs.Write(inputByteArray, 0, inputByteArray.Length);  
			cs.FlushFinalBlock();
			fs=File.OpenWrite(savePath);
			foreach(byte b in ms.ToArray())  
			{  
				fs.WriteByte(b);  
			} 
			fs.Close();
			cs.Close();
			ms.Close();
			return true;
		}

		/// <summary>
		/// 解密字符串
		/// </summary>
		/// <param name="inputStr">要解密的字符串</param>
		/// <param name="keyStr">密钥</param>
		/// <returns>解密后的结果</returns>
		static public string SDecryptString(string inputStr,string keyStr)
		{
            SecurityHelper ws = new SecurityHelper();
			return ws.DecryptString(inputStr,keyStr);
		}
		/// <summary>
		///  解密字符串 密钥为系统默认
		/// </summary>
		/// <param name="inputStr">要解密的字符串</param>
		/// <returns>解密后的结果</returns>
		static public string SDecryptString(string inputStr)
		{
            SecurityHelper ws = new SecurityHelper();
			return ws.DecryptString(inputStr,"");
		}

        /// <summary>
        /// 得到MD5加密子符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool VerifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(input);

            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 与ASP兼容的MD5加密算法
        /// </summary>
        /// <param name="strUnSafe">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string GetSafeStringByMD5(string strUnSafe)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                byte[] bytes = md5.ComputeHash(Encoding.GetEncoding("GB2312").GetBytes(strUnSafe));
                StringBuilder strSafe = new StringBuilder(32);
                for (int i = 0; i < bytes.Length; i++)
                    strSafe.Append(bytes[i].ToString("x").PadLeft(2, '0'));
                return strSafe.ToString();
            }
        }


        #region 3DES 加密解密
        #region TripleDESEncrypt 3DES加密24位填充类型为0字节
        /// <summary>
        /// 3DES加密24位填充类型为0字节
        /// </summary>
        /// <param name="encryptSource">加密的数据源</param>
        /// <param name="encryptKey">加密的密匙</param>
        /// <param name="encryptIV">加密的矢量</param>
        /// <returns>加密串</returns>
        public static string TripleDESEncrypt(string encryptSource, string encryptKey, string encryptIV)
        {
            return TripleDESEncrypt(encryptSource, encryptKey, encryptIV, System.Security.Cryptography.PaddingMode.Zeros, 24, "ToBase64");
        }
        #endregion

        #region TripleDESEncrypt 3DES加密24位自定填充类型
        /// <summary>
        /// 3DES加密24位自定填充类型
        /// </summary>
        /// <param name="encryptSource">加密的数据源</param>
        /// <param name="encryptKey">加密的密匙</param>
        /// <param name="encryptIV">加密的矢量</param>
        /// <param name="paddingMode">填充类型</param>
        /// <returns>加密串</returns>
        public static string TripleDESEncrypt(string encryptSource, string encryptKey, string encryptIV, System.Security.Cryptography.PaddingMode paddingMode)
        {
            return TripleDESEncrypt(encryptSource, encryptKey, encryptIV, paddingMode, 24, "ToBase64");
        }
        #endregion

        #region TripleDESEncrypt  3DES加密自定义字节位填充类型
        /// <summary>
        /// 3DES加密自定义字节位填充类型
        /// </summary>
        /// <param name="encryptSource">加密的数据源</param>
        /// <param name="encryptKey">加密的密匙</param>
        /// <param name="encryptIV">加密的矢量</param>
        /// <param name="paddingMode">自定义填充类型</param>
        /// <param name="byteNum">自定义字节数</param>
        /// <param name="outType">输出字节形式ToHex16,ToBase64</param>
        /// <returns>加密串</returns>
        public static string TripleDESEncrypt(string encryptSource, string encryptKey, string encryptIV, System.Security.Cryptography.PaddingMode paddingMode, int byteNum, string outType)
        {
            //构造一个对称算法
            SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();

            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            if (string.IsNullOrEmpty(outType))
            {
                return "输出配置不能空";
            }
            if ((encryptKey.Trim().Length) != byteNum)
            {
                return "加密字节";
            }
            byte[] Key = System.Text.Encoding.Default.GetBytes(encryptKey);
            mCSP.Key = Key;
            //默认矢量
            if (String.IsNullOrEmpty(encryptIV))
            {
                encryptIV = encryptKey.Substring(0, 8);
            }
            mCSP.IV = System.Text.Encoding.Default.GetBytes(encryptIV);

            //指定加密的运算模式
            mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            //获取或设置加密算法的填充模式
            mCSP.Padding = paddingMode;

            ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);

            byt = System.Text.Encoding.Default.GetBytes(encryptSource.Trim());

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();

            string mToString = string.Empty;
            //输出16进制字符
            if (outType == "ToHex16")
            {
                mToString = ToHexString(ms.ToArray());
            }
            //输出ToBase64字符
            if (outType == "ToBase64")
            {
                mToString = Convert.ToBase64String(ms.ToArray()).ToString().Replace("\0", "");
            }

            return mToString;
        }
        #endregion


        #region TripleDESDecrypt  3DES解密24位Zeros填充
        /// <summary>
        /// 3DES解密24位Zeros填充
        /// </summary>
        /// <param name="decryptSource">解密的数据源</param>
        /// <param name="decryptKey">解密的密匙</param>
        /// <param name="decryptIV">解密的矢量</param>
        /// <returns>解密串</returns>
        public static string TripleDESDecrypt(string decryptSource, string decryptKey, string decryptIV)
        {
            return TripleDESDecrypt(decryptSource, decryptKey, decryptIV, System.Security.Cryptography.PaddingMode.Zeros, 24, "ToBase64");
        }
        #endregion

        #region TripleDESDecrypt 3DES解密24位自定义填充类型
        /// <summary>
        /// 3DES解密24位自定义填充类型
        /// </summary>
        /// <param name="decryptSource">解密的数据源</param>
        /// <param name="decryptKey">解密的密匙</param>
        /// <param name="decryptIV">解密的矢量</param>
        /// <param name="paddingMode">填充类型</param>
        /// <returns>解密串</returns>
        public static string TripleDESDecrypt(string decryptSource, string decryptKey, string decryptIV, System.Security.Cryptography.PaddingMode paddingMode)
        {
            return TripleDESDecrypt(decryptSource, decryptKey, decryptIV, paddingMode, 24, "ToBase64");
        }
        #endregion

        #region TripleDESDecrypt 3DES解密
        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="decryptSource">解密的数据源</param>
        /// <param name="decryptKey">解密的密匙</param>
        /// <param name="decryptIV">解密的矢量</param>
        /// <param name="paddingMode">填充模式</param>
        /// <param name="byteNum">字节</param>
        ///  <param name="outType">解密字节形式ToHex16,ToBase64</param>
        /// <returns>解密串</returns>
        public static string TripleDESDecrypt(string decryptSource, string decryptKey, string decryptIV, System.Security.Cryptography.PaddingMode paddingMode, int byteNum, string outType)
        {
            //构造一个对称算法
            SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();

            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt = new byte[0];

            if (string.IsNullOrEmpty(outType))
            {
                return "解密配置不能空";
            }
            if ((decryptKey.Trim().Length) != byteNum)
            {
                return "解密字节错误";
            }

            byte[] Key = System.Text.Encoding.Default.GetBytes(decryptKey.Trim());

            mCSP.Key = Key;
            //默认矢量
            if (String.IsNullOrEmpty(decryptIV))
            {
                decryptIV = decryptKey.Substring(0, 8);
            }
            mCSP.IV = System.Text.Encoding.Default.GetBytes(decryptIV);

            mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            mCSP.Padding = paddingMode;
            ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);

            //输出16进制字符
            if (outType == "ToHex16")
            {
                byt = ConvertHexToBytes(decryptSource);
            }
            //输出ToBase64字符
            if (outType == "ToBase64")
            {
                byt = Convert.FromBase64String(decryptSource);
            }
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();

            return Encoding.Default.GetString(ms.ToArray()).Replace("\0", "").Trim();
        }
        #endregion

        #region GetMD5 MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">加密的数据源,测试123结果为f1c728f403a814c2d09512e835263dfe</param>
        /// <returns>加密串</returns>
        public static string GetMD5(string source)
        {
            string md5String = string.Empty;
            try
            {
                byte[] byteCode = System.Text.Encoding.Default.GetBytes(source.Trim());
                byteCode = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(byteCode);

                for (int i = 0; i < byteCode.Length; i++)
                {
                    md5String += byteCode[i].ToString("x").PadLeft(2, '0');
                }
            }
            catch (Exception ex)
            {
                md5String = ex.ToString();
                return md5String;
            }
            return md5String;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string source)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5");
        }
        #endregion
        #region ConvertHexToBytes将16进制字符串转化为字节数组
        /// <summary>
        /// 将16进制字符串转化为字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ConvertHexToBytes(string value)
        {
            int len = value.Length / 2;
            byte[] ret = new byte[len];
            for (int i = 0; i < len; i++)
                ret[i] = (byte)(Convert.ToInt32(value.Substring(i * 2, 2), 16));
            return ret;
        }
        #endregion
        #region ToHexString 将字节数组转换为字符串
        /// <summary>
        /// 字符
        /// </summary>
        public static char[] HexDigits = {'0', '1', '2', '3', '4', '5', '6', '7',
										'8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
        /// <summary>
        /// 将字节数组转换为字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i * 2] = HexDigits[b >> 4];
                chars[i * 2 + 1] = HexDigits[b & 0xF];
            }
            return new string(chars);
        }
        #endregion
        #endregion
	}
}
