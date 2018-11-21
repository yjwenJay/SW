using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW
{
    /// <summary>
    /// 字符串操作 二
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// JS的Eval方法
        /// 异常返回 Error
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Eval(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return "";
            try
            {
                expression = expression.Replace("==", "=");
                return new System.Data.DataTable().Compute(expression, "").ToString();

                //另一种已过时方法
                //Microsoft.JScript.Vsa.VsaEngine ve = Microsoft.JScript.Vsa.VsaEngine.CreateEngine();
                //return Microsoft.JScript.Eval.JScriptEvaluate(s, ve);
            }
            catch
            {
                return "Error";
            }
        }

        #region 字符串操作

        /// <summary>
        /// 替换字符串中的任何空白字符为一个空格
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ReplaceHtmlToLine(string content)
        {
            if (string.IsNullOrEmpty(content))
                return "";
            try
            {
                content = RegExp.RegReplace(content, @"(\s{2,})", " ");
                //content = RegExp.RegReplace(content, @"((\r\n){2,})", " ");
            }
            catch { }
            return content;
        }

        /// <summary>
        /// 格式化为JS字符串,用于alert等输出
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConvertToJSStr(object obj)
        {
            string str = obj.ToString();
            if (string.IsNullOrEmpty(str))
                return "";
            str = str.Replace("'", "");
            str = str.Replace("\"", "");
            str = str.Replace("\r", "\\r");
            str = str.Replace("\n", "\\n");
            return str;
        }

        /// <summary>
        /// 删除字符串结尾的逗号
        /// 也可使用方法 .Trim(',')
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns></returns>
        public static string DelLastComma(string str)
        {
            return DelLastString(str, ",");
        }

        /// <summary>
        /// 返回从字符串开始位置0,到字符串中最后一个指定字符开始为结束位置的新字符串
        /// 删除字符串中最后一个指定字符及指定字符以后的数据.
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="strchar">要删除的字符串</param>
        /// <returns></returns>
        public static string DelLastStrStart(string str, string strchar)
        {
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(strchar))
            {
                int tmp = str.LastIndexOf(strchar);
                if (tmp != -1)
                    return str.Substring(0, tmp);
                else
                    return str;
            }
            return "";
        }

        /// <summary>
        /// 删除字符串结尾的字符
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="dstr">要删除的字符串</param>
        /// <returns></returns>
        public static string DelLastString(string str, string dstr)
        {
            string restr = "";
            if (!string.IsNullOrEmpty(str) && str.Length > 0)
            {
                restr = str;
                if (!string.IsNullOrEmpty(dstr))
                {
                    if (str.Substring(str.Length - dstr.Length).Equals(dstr))
                    {
                        return str.Substring(0, str.Length - dstr.Length);
                    }
                }
            }
            return restr;
        }


        /// <summary>
        /// 按字节截断字符串,中文占两位(对象,最终长度,末尾字符)
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="num">长度</param>
        /// <param name="endStr">末尾字符</param>
        /// <returns></returns>
        public static string StrLen(object obj, int num, string endStr = "...")
        {
            try
            {
                if (obj == null)
                    return "";
                string str = obj.ToString();
                if (string.IsNullOrEmpty(str))
                    return "";
                str = str.Trim();
                str = RegExp.ReplaceHtmlTag(str);
                string temp = str.Substring(0, (str.Length < num + 1) ? str.Length : num + 1);
                byte[] encodedBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(temp);
                string outputStr = "";
                int count = 0;

                for (int i = 0; i < temp.Length; i++)
                {
                    if ((int)encodedBytes[i] == 63)
                        count += 2;
                    else
                        count += 1;

                    if (count <= num - endStr.Length)
                        outputStr += temp.Substring(i, 1);
                    else if (count > num)
                        break;
                }
                if (count <= num)
                {
                    outputStr = temp;
                    endStr = "";
                }
                outputStr += endStr;
                return outputStr;
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// 截断字符串(普通)(对象,长度)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string StrLength(object obj, int num, bool isdd = true)
        {
            try
            {
                if (obj == null)
                    return "";
                string str = obj.ToString();
                if (string.IsNullOrEmpty(str) || num <= 0)
                    return "";
                str = RegExp.ReplaceHtmlTag(str);
                if (str.Length > num)
                {
                    if (isdd)
                        return str.Substring(0, num) + "..";
                    else
                        return str.Substring(0, num);
                }
                else
                    return str;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 字符串替换

        /// <summary>
        /// 过滤字符串中指定的特殊字符
        /// </summary>
        /// <param name="str">要进行过滤的字符串</param>
        /// <param name="repStr">特殊字符数组</param>
        /// <returns></returns>
        public static string ReplaceStr(string str, string[] repStr)
        {
            if (string.IsNullOrEmpty(str) || repStr.Length <= 0)
                return "";
            foreach (string s in repStr)
            {
                if (string.IsNullOrEmpty(s))
                    continue;
                str = str.Replace(s, "");
            }
            return str;
        }


        /// <summary>
        /// 过滤字符串中的单引号
        /// </summary>
        /// <param name="str">要进行过滤的字符串</param>
        /// <returns></returns>
        public static string ReplaceStr(string str)
        {
            string[] strArr = new string[] { "'" };
            return ReplaceStr(str, strArr);
        }

        /// <summary>
        /// 将字符串从指定位置开始到固定长度的内容替换为其它字符
        /// 如手机号中部显示为*，身份证中部显示为星号
        /// cardid = StringHelper.ReplaceSubstrToStr(cardid, 6, 10, 4);
        /// cardid = StringHelper.ReplaceSubstrToStr("13086668476", 3, 10, 4);
        /// </summary>
        /// <param name="str">要替换的字符串</param>
        /// <param name="start">开始位置</param>
        /// <param name="length">长度</param>
        /// <param name="lastStr">字符串结束不替换数</param>
        /// <param name="toStr">替换内容</param>
        /// <returns></returns>
        public static string ReplaceSubstrToStr(string str, int start, int length = 4, int lastStr = 0, string toStr = "*")
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (start < 0) start = 0;
            if (start < str.Length)
            {
                if (length + start > str.Length)
                    length = str.Length - start;
                if (lastStr > 0)
                {
                    if (length + start > str.Length - lastStr)
                    {
                        length = str.Length - lastStr - start;
                    }
                    else
                        length = 0;
                }

                if (length <= 0)
                    return str;
                string newValue = "";
                while (newValue.Length < length)
                {
                    newValue += toStr;
                }
                if (newValue.Length > length)
                    newValue = newValue.Substring(0, length);

                string restr = str.Substring(start, length);
                if (restr.Length > 0)
                    return str.Replace(restr, newValue);
            }
            else
                return "";
            return str;
        }

        #endregion

        #region 拼音

        /// <summary>
        /// 读字符串拼音首字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToPY(string str)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in str)
            {
                builder.Append(GetOneIndex(c));
            }
            return builder.ToString();
        }

        private static string GetOneIndex(char _c)
        {
            if ((_c >= '\0') && (_c < 'Ā'))
            {
                return _c.ToString();
            }
            return GetGbkX(_c.ToString());
        }

        private static string GetGbkX(string testTxt)
        {
            if (testTxt.CompareTo("吖") >= 0)
            {
                if (testTxt.CompareTo("八") < 0)
                {
                    return "A";
                }
                if (testTxt.CompareTo("嚓") < 0)
                {
                    return "B";
                }
                if (testTxt.CompareTo("咑") < 0)
                {
                    return "C";
                }
                if (testTxt.CompareTo("妸") < 0)
                {
                    return "D";
                }
                if (testTxt.CompareTo("发") < 0)
                {
                    return "E";
                }
                if (testTxt.CompareTo("旮") < 0)
                {
                    return "F";
                }
                if (testTxt.CompareTo("铪") < 0)
                {
                    return "G";
                }
                if (testTxt.CompareTo("讥") < 0)
                {
                    return "H";
                }
                if (testTxt.CompareTo("咔") < 0)
                {
                    return "J";
                }
                if (testTxt.CompareTo("垃") < 0)
                {
                    return "K";
                }
                if (testTxt.CompareTo("嘸") < 0)
                {
                    return "L";
                }
                if (testTxt.CompareTo("拏") < 0)
                {
                    return "M";
                }
                if (testTxt.CompareTo("噢") < 0)
                {
                    return "N";
                }
                if (testTxt.CompareTo("妑") < 0)
                {
                    return "O";
                }
                if (testTxt.CompareTo("七") < 0)
                {
                    return "P";
                }
                if (testTxt.CompareTo("亽") < 0)
                {
                    return "Q";
                }
                if (testTxt.CompareTo("仨") < 0)
                {
                    return "R";
                }
                if (testTxt.CompareTo("他") < 0)
                {
                    return "S";
                }
                if (testTxt.CompareTo("哇") < 0)
                {
                    return "T";
                }
                if (testTxt.CompareTo("夕") < 0)
                {
                    return "W";
                }
                if (testTxt.CompareTo("丫") < 0)
                {
                    return "X";
                }
                if (testTxt.CompareTo("帀") < 0)
                {
                    return "Y";
                }
                if (testTxt.CompareTo("咗") < 0)
                {
                    return "Z";
                }
            }
            return testTxt;
        }

        #endregion

        #region

        /// <summary>
        /// 对INT数组内的值从小到大排序
        /// </summary>
        /// <param name="iArr"></param>
        /// <returns></returns>
        public static int[] IntArrOrder(int[] iArr)
        {
            int[] NewArr = new int[iArr.Length];
            for (int i = 0; i < iArr.Length; i++)
            {
                for (int j = i + 1; j < iArr.Length; j++)
                {
                    if (iArr[j] < iArr[i])
                    {
                        int tmp = iArr[i];
                        iArr[i] = iArr[j];
                        iArr[j] = tmp;
                    }
                }
            }
            return iArr;
        }



        /// <summary>
        /// 可扩展多个属性的方法
        /// </summary>
        public static string[] StringArr(string strArr, int size)
        {
            string[] value = new string[size];
            string[] arr = new string[0];
            if (!string.IsNullOrEmpty(strArr))
                arr = strArr.Split(',');
            for (int i = 0; i < value.Length; i++)
            {
                if (i < arr.Length)
                    value[i] = arr[i];
                else
                    value[i] = "";
            }
            return value;
            //修改
            //string[] valArr = StringHelper.StringArr(model.statusArr);
            //valArr[0] = "2";
            //model.statusArr = string.Join(",", valArr);
            //bll.Update(model);

            //判断
            //string[] valArr = StringHelper.StringArr(model.statusArr);
            //bool sended = false;//已发送
            //if (valArr.Length > 2)
            //{
            //    if (valArr[2] == "1")
            //        sended = true;
            //}
        }
        #endregion
    }
}
