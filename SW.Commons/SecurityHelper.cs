/**********************************************************************
 * * �� �� ���� SecurityHelper
 * * �ļ���ţ� 01
 * * �� �� �ˣ� Jordan
 * * ��    �ڣ� 2006-12-02
 * * �� �� �ˣ� Jordan
 * * �޸����ڣ� 2006-12-02
 * * ��    ���� ��ȫ�����ܵ�ͨ�ò�����
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
	/// MySecurity ��ժҪ˵����
	/// </summary>
    public class SecurityHelper
	{
		/// <summary>
		/// ��ʼ����ȫ��
		/// </summary>
		public SecurityHelper()
		{
			key = "0123456789";
		}
		private string key; //Ĭ����Կ

		private byte[] sKey;
		private byte[] sIV;

		/// <summary>
		/// �����ַ���
		/// </summary>
		/// <param name="inputStr">�����ַ���</param>
		/// <param name="keyStr">���룬����Ϊ����</param>
		/// <returns>������ܺ��ַ���</returns>
		static public string SEncryptString(string inputStr,string keyStr)
		{
            SecurityHelper ws = new SecurityHelper();
			return ws.EncryptString(inputStr,keyStr);
		}
		/// <summary>
		/// �����ַ��� ��ԿΪϵͳĬ��
		/// </summary>
		/// <param name="inputStr">�����ַ���</param>
		/// <returns>������ܺ��ַ���</returns>
		static public string SEncryptString(string inputStr)
		{
            SecurityHelper ws = new SecurityHelper();
			return ws.EncryptString(inputStr,"");
		}
		/// <summary>
		/// �����ַ���
		/// </summary>
		/// <param name="inputStr">�����ַ���</param>
		/// <param name="keyStr">���룬����Ϊ����</param>
		/// <returns>������ܺ��ַ���</returns>
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
		/// �����ļ�
		/// </summary>
		/// <param name="filePath">�����ļ�·��</param>
		/// <param name="savePath">���ܺ�����ļ�·��</param>
		/// <param name="keyStr">���룬����Ϊ����</param>
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
		/// �����ַ���
		/// </summary>
		/// <param name="inputStr">Ҫ���ܵ��ַ���</param>
		/// <param name="keyStr">��Կ</param>
		/// <returns>���ܺ�Ľ��</returns>
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
		/// �����ļ�
		/// </summary>
		/// <param name="filePath">�����ļ�·��</param>
		/// <param name="savePath">���ܺ�����ļ�·��</param>
		/// <param name="keyStr">���룬����Ϊ����</param>
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
		/// �����ַ���
		/// </summary>
		/// <param name="inputStr">Ҫ���ܵ��ַ���</param>
		/// <param name="keyStr">��Կ</param>
		/// <returns>���ܺ�Ľ��</returns>
		static public string SDecryptString(string inputStr,string keyStr)
		{
            SecurityHelper ws = new SecurityHelper();
			return ws.DecryptString(inputStr,keyStr);
		}
		/// <summary>
		///  �����ַ��� ��ԿΪϵͳĬ��
		/// </summary>
		/// <param name="inputStr">Ҫ���ܵ��ַ���</param>
		/// <returns>���ܺ�Ľ��</returns>
		static public string SDecryptString(string inputStr)
		{
            SecurityHelper ws = new SecurityHelper();
			return ws.DecryptString(inputStr,"");
		}

        /// <summary>
        /// �õ�MD5�����ӷ���
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
        /// ��ASP���ݵ�MD5�����㷨
        /// </summary>
        /// <param name="strUnSafe">Ҫ���ܵ��ַ���</param>
        /// <returns>���ܺ���ַ���</returns>
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


        #region 3DES ���ܽ���
        #region TripleDESEncrypt 3DES����24λ�������Ϊ0�ֽ�
        /// <summary>
        /// 3DES����24λ�������Ϊ0�ֽ�
        /// </summary>
        /// <param name="encryptSource">���ܵ�����Դ</param>
        /// <param name="encryptKey">���ܵ��ܳ�</param>
        /// <param name="encryptIV">���ܵ�ʸ��</param>
        /// <returns>���ܴ�</returns>
        public static string TripleDESEncrypt(string encryptSource, string encryptKey, string encryptIV)
        {
            return TripleDESEncrypt(encryptSource, encryptKey, encryptIV, System.Security.Cryptography.PaddingMode.Zeros, 24, "ToBase64");
        }
        #endregion

        #region TripleDESEncrypt 3DES����24λ�Զ��������
        /// <summary>
        /// 3DES����24λ�Զ��������
        /// </summary>
        /// <param name="encryptSource">���ܵ�����Դ</param>
        /// <param name="encryptKey">���ܵ��ܳ�</param>
        /// <param name="encryptIV">���ܵ�ʸ��</param>
        /// <param name="paddingMode">�������</param>
        /// <returns>���ܴ�</returns>
        public static string TripleDESEncrypt(string encryptSource, string encryptKey, string encryptIV, System.Security.Cryptography.PaddingMode paddingMode)
        {
            return TripleDESEncrypt(encryptSource, encryptKey, encryptIV, paddingMode, 24, "ToBase64");
        }
        #endregion

        #region TripleDESEncrypt  3DES�����Զ����ֽ�λ�������
        /// <summary>
        /// 3DES�����Զ����ֽ�λ�������
        /// </summary>
        /// <param name="encryptSource">���ܵ�����Դ</param>
        /// <param name="encryptKey">���ܵ��ܳ�</param>
        /// <param name="encryptIV">���ܵ�ʸ��</param>
        /// <param name="paddingMode">�Զ����������</param>
        /// <param name="byteNum">�Զ����ֽ���</param>
        /// <param name="outType">����ֽ���ʽToHex16,ToBase64</param>
        /// <returns>���ܴ�</returns>
        public static string TripleDESEncrypt(string encryptSource, string encryptKey, string encryptIV, System.Security.Cryptography.PaddingMode paddingMode, int byteNum, string outType)
        {
            //����һ���Գ��㷨
            SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();

            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            if (string.IsNullOrEmpty(outType))
            {
                return "������ò��ܿ�";
            }
            if ((encryptKey.Trim().Length) != byteNum)
            {
                return "�����ֽ�";
            }
            byte[] Key = System.Text.Encoding.Default.GetBytes(encryptKey);
            mCSP.Key = Key;
            //Ĭ��ʸ��
            if (String.IsNullOrEmpty(encryptIV))
            {
                encryptIV = encryptKey.Substring(0, 8);
            }
            mCSP.IV = System.Text.Encoding.Default.GetBytes(encryptIV);

            //ָ�����ܵ�����ģʽ
            mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            //��ȡ�����ü����㷨�����ģʽ
            mCSP.Padding = paddingMode;

            ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);

            byt = System.Text.Encoding.Default.GetBytes(encryptSource.Trim());

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();

            string mToString = string.Empty;
            //���16�����ַ�
            if (outType == "ToHex16")
            {
                mToString = ToHexString(ms.ToArray());
            }
            //���ToBase64�ַ�
            if (outType == "ToBase64")
            {
                mToString = Convert.ToBase64String(ms.ToArray()).ToString().Replace("\0", "");
            }

            return mToString;
        }
        #endregion


        #region TripleDESDecrypt  3DES����24λZeros���
        /// <summary>
        /// 3DES����24λZeros���
        /// </summary>
        /// <param name="decryptSource">���ܵ�����Դ</param>
        /// <param name="decryptKey">���ܵ��ܳ�</param>
        /// <param name="decryptIV">���ܵ�ʸ��</param>
        /// <returns>���ܴ�</returns>
        public static string TripleDESDecrypt(string decryptSource, string decryptKey, string decryptIV)
        {
            return TripleDESDecrypt(decryptSource, decryptKey, decryptIV, System.Security.Cryptography.PaddingMode.Zeros, 24, "ToBase64");
        }
        #endregion

        #region TripleDESDecrypt 3DES����24λ�Զ����������
        /// <summary>
        /// 3DES����24λ�Զ����������
        /// </summary>
        /// <param name="decryptSource">���ܵ�����Դ</param>
        /// <param name="decryptKey">���ܵ��ܳ�</param>
        /// <param name="decryptIV">���ܵ�ʸ��</param>
        /// <param name="paddingMode">�������</param>
        /// <returns>���ܴ�</returns>
        public static string TripleDESDecrypt(string decryptSource, string decryptKey, string decryptIV, System.Security.Cryptography.PaddingMode paddingMode)
        {
            return TripleDESDecrypt(decryptSource, decryptKey, decryptIV, paddingMode, 24, "ToBase64");
        }
        #endregion

        #region TripleDESDecrypt 3DES����
        /// <summary>
        /// 3DES����
        /// </summary>
        /// <param name="decryptSource">���ܵ�����Դ</param>
        /// <param name="decryptKey">���ܵ��ܳ�</param>
        /// <param name="decryptIV">���ܵ�ʸ��</param>
        /// <param name="paddingMode">���ģʽ</param>
        /// <param name="byteNum">�ֽ�</param>
        ///  <param name="outType">�����ֽ���ʽToHex16,ToBase64</param>
        /// <returns>���ܴ�</returns>
        public static string TripleDESDecrypt(string decryptSource, string decryptKey, string decryptIV, System.Security.Cryptography.PaddingMode paddingMode, int byteNum, string outType)
        {
            //����һ���Գ��㷨
            SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();

            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt = new byte[0];

            if (string.IsNullOrEmpty(outType))
            {
                return "�������ò��ܿ�";
            }
            if ((decryptKey.Trim().Length) != byteNum)
            {
                return "�����ֽڴ���";
            }

            byte[] Key = System.Text.Encoding.Default.GetBytes(decryptKey.Trim());

            mCSP.Key = Key;
            //Ĭ��ʸ��
            if (String.IsNullOrEmpty(decryptIV))
            {
                decryptIV = decryptKey.Substring(0, 8);
            }
            mCSP.IV = System.Text.Encoding.Default.GetBytes(decryptIV);

            mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            mCSP.Padding = paddingMode;
            ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);

            //���16�����ַ�
            if (outType == "ToHex16")
            {
                byt = ConvertHexToBytes(decryptSource);
            }
            //���ToBase64�ַ�
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

        #region GetMD5 MD5����
        /// <summary>
        /// MD5����
        /// </summary>
        /// <param name="source">���ܵ�����Դ,����123���Ϊf1c728f403a814c2d09512e835263dfe</param>
        /// <returns>���ܴ�</returns>
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
        /// MD5����
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string source)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5");
        }
        #endregion
        #region ConvertHexToBytes��16�����ַ���ת��Ϊ�ֽ�����
        /// <summary>
        /// ��16�����ַ���ת��Ϊ�ֽ�����
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
        #region ToHexString ���ֽ�����ת��Ϊ�ַ���
        /// <summary>
        /// �ַ�
        /// </summary>
        public static char[] HexDigits = {'0', '1', '2', '3', '4', '5', '6', '7',
										'8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
        /// <summary>
        /// ���ֽ�����ת��Ϊ�ַ���
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
