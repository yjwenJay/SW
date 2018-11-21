using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SW.Security
{
    public class MD5Helper
    {
        /// <summary>
        /// 对字符串进行 MD5 加密
        /// </summary>
        /// <param name="content">需要加密的内容</param>
        /// <param name="encoding">字符集</param>
        /// <returns>MD5 加密结果</returns>
        public static string Encrypt(string content, Encoding encoding)
        {
            var md5 = new MD5CryptoServiceProvider();
            var arr = encoding.GetBytes(content);
            arr = md5.ComputeHash(arr);
            var result = new StringBuilder();

            Array.ForEach(arr, item =>
            {
                result.AppendFormat("{0:X2}", item);
            });

            return result.ToString();
        }
    }
}
