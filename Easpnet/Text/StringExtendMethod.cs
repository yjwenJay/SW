using System;
using System.Collections.Generic;
using System.Text;

namespace Donet
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
    }
}
