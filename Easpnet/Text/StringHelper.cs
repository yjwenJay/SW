using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet
{
    public static class StringHelper
    {
        /// <summary>
        /// 获取前len个字符，超出部分以des来描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetSubstring(string str, int len, string des)
        {
            if (str.Length <= len)
            {
                return str;
            }
            else
            {
                return str.Substring(0, len) + des;
            }
        }

        /// <summary>
        /// 判别两个字符串是否相等
        /// </summary>
        /// <param name="s1">字符串1</param>
        /// <param name="s2">字符串2</param>
        /// <param name="igloreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static bool IsEqual(string s1, string s2, bool igloreCase)
        {
            if (igloreCase)
            {
                s1 = s1.ToLower();
                s2 = s2.ToLower();
            }

            return s1 == s2;
        }
    }
}
