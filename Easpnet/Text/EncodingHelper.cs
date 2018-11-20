using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Text
{
    public static class EncodingHelper
    {
        /// <summary>
        /// 将字符串进行编码的转化
        /// </summary>
        /// <param name="content">转化的内容</param>
        /// <param name="from">原始编码</param>
        /// <param name="to">目标编码</param>
        /// <returns></returns>
        public static string ConvertEncoding(string content, System.Text.Encoding from, System.Text.Encoding to)
        {
            byte[] bt = from.GetBytes(content);
            byte[] utf8Bytes = Encoding.Convert(System.Text.Encoding.Default,
                to, bt);
            content = to.GetString(utf8Bytes);
            return content;
        }
    }
}
