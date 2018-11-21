namespace SW
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Security;

    public class Text
    {
        public static string AddZero(int i)
        {
            return (((i > 9) ? string.Empty : "0") + i);
        }

        public static bool ComparePassword(string pwd1, string pwd2)
        {
            if ((pwd1 == null) && (pwd2 == null))
            {
                return true;
            }
            if ((pwd1 == null) || (pwd2 == null))
            {
                return false;
            }
            int length = pwd1.Length;
            int num2 = pwd2.Length;
            if ((length == num2) && (length != 0))
            {
                return (0 == string.Compare(pwd1, pwd2, true));
            }
            if ((0x20 == length) && (0x10 == num2))
            {
                return (0 == string.Compare(pwd1.Substring(8, 0x10), pwd2, true));
            }
            return (((0x10 == length) && (0x20 == num2)) && (0 == string.Compare(pwd2.Substring(8, 0x10), pwd1, true)));
        }

        public static string DelLastChar(string str, string strchar)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return str.Substring(0, str.LastIndexOf(strchar));
            }
            return "";
        }

        public static string DelLastComma(string str)
        {
            return DelLastChar(str, ",");
        }

        public static string EmailEncode(string s)
        {
            string str = TextEncode(s).Replace("@", "&#64;").Replace(".", "&#46;");
            return JoinString(new string[] { "<a href='mailto:", str, "'>", str, "</a>" });
        }

        public static string GenerateToken(string s)
        {
            if ((s == null) || (s.Length == 0))
            {
                s = string.Empty;
            }
            //return MD5(s + ConfigHelper.Get("CookieToken"));
            return MD5(s);
        }

        public static string GetArrayStr(List<string> list, string spXLTer)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == (list.Count - 1))
                {
                    builder.Append(list[i]);
                }
                else
                {
                    builder.Append(list[i]);
                    builder.Append(spXLTer);
                }
            }
            return builder.ToString();
        }

        public static string[] GetStrArray(string str)
        {
            return GetStrArray(str, ',');
        }

        public static string[] GetStrArray(string str, char strchar)
        {
            return str.Split(new char[] { strchar });
        }

        public static string HtmlDecode(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            StringBuilder builder = new StringBuilder(s);
            builder.Replace("&amp;", "&");
            builder.Replace("&lt;", "<");
            builder.Replace("&gt;", ">");
            builder.Replace("&quot;", "\"");
            builder.Replace("&#39;", "'");
            builder.Replace("<br/>", "\r\n");
            return builder.ToString();
        }

        public static string HtmlEncode(string s)
        {
            return HtmlEncode(s, true);
        }

        public static string HtmlEncode(string s, bool bln)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            StringBuilder builder = new StringBuilder(s);
            builder.Replace("&", "&amp;");
            builder.Replace("<", "&lt;");
            builder.Replace(">", "&gt;");
            builder.Replace("\"", "&quot;");
            builder.Replace("'", "&#39;");
            builder.Replace("\r\n", "<br/>");
            builder.Replace("\r", "<br/>");
            builder.Replace("\n", "<br/>");
            if (bln)
            {
                return ShitEncode(builder.ToString());
            }
            return builder.ToString();
        }

        public static bool IsInString(string arr, string str)
        {
            if (string.IsNullOrEmpty(arr) || string.IsNullOrEmpty(str))
            {
                return false;
            }
            return (("_" + arr).IndexOf(str) > 0);
        }

        public static bool IsNumberList(string str)
        {
            return IsNumberList(str, ',');
        }

        public static bool IsNumberList(string str, char separator)
        {
            if (str == null)
            {
                return false;
            }
            int length = str.Length;
            if (length == 0)
            {
                return false;
            }
            if (!char.IsNumber(str[0]) || !char.IsNumber(str[length - 1]))
            {
                return false;
            }
            length--;
            for (int i = 1; i < length; i++)
            {
                if (separator == str[i])
                {
                    if (!char.IsNumber(str[i - 1]) || !char.IsNumber(str[i + 1]))
                    {
                        return false;
                    }
                }
                else if (!char.IsNumber(str[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsNumeric(object value)
        {
            if (value == null)
            {
                return false;
            }
            return IsNumeric(value.ToString());
        }

        public static bool IsNumeric(string value)
        {
            if (value == null)
            {
                return false;
            }
            int length = value.Length;
            if (length == 0)
            {
                return false;
            }
            if (('-' != value[0]) && !char.IsNumber(value[0]))
            {
                return false;
            }
            for (int i = 1; i < length; i++)
            {
                if (!char.IsNumber(value[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static string JavaScriptEncode(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return JavaScriptEncode(obj.ToString());
        }

        public static string JavaScriptEncode(string str)
        {
            StringBuilder builder = new StringBuilder(str);
            builder.Replace(@"\", @"\\");
            builder.Replace("\r", @"\0Dx");
            builder.Replace("\n", @"\x0A");
            builder.Replace("\"", @"\x22");
            builder.Replace("'", @"\x27");
            return builder.ToString();
        }

        public static string JoinString(params string[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (value.Length == 0)
            {
                return string.Empty;
            }
            return string.Join(string.Empty, value);
        }

        public static string Left(string s, int need, bool encode)
        {
            return Left(s, need, encode, "...");
        }

        public static string Left(string s, int need, bool encode, string endStr)
        {
            if ((s == null) || (s == ""))
            {
                return string.Empty;
            }
            int length = s.Length;
            if (length < (need / 2))
            {
                if (!encode)
                {
                    return s;
                }
                return TextEncode(s);
            }
            int num4 = 0;
            int num2 = 0;
            while (num2 < length)
            {
                char ch = s[num2];
                num4 += RegExp.IsUnicode(ch.ToString()) ? 2 : 1;
                if (num4 >= need)
                {
                    break;
                }
                num2++;
            }
            string str = s.Substring(0, num2);
            if (length > num2)
            {
                int num3 = 0;
                while (num3 < 5)
                {
                    char ch2 = s[num2 - num3];
                    num4 -= RegExp.IsUnicode(ch2.ToString()) ? 2 : 1;
                    if (num4 <= need)
                    {
                        break;
                    }
                    num3++;
                }
                str = s.Substring(0, num2 - num3) + endStr;
            }
            if (!encode)
            {
                return str;
            }
            return TextEncode(str);
        }

        public static int Len(string s)
        {
            return HttpContext.Current.Request.ContentEncoding.GetByteCount(s);
        }

        public static string LoseHtml(string htmlCode)
        {
            if ((htmlCode != null) && (htmlCode.Length != 0))
            {
                return Regex.Replace(htmlCode, "<[^>]+>", string.Empty, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            }
            return string.Empty;
        }

        public static string MD5(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "";
            if (HttpContext.Current == null)
            {
                if ((s == null) || (s.Length == 0))
                {
                    s = string.Empty;
                }
                return FormsAuthentication.HashPasswordForStoringInConfigFile(s, "MD5").ToLower();
            }
            System.Security.Cryptography.MD5 md = System.Security.Cryptography.MD5.Create();
            byte[] bytes = HttpContext.Current.Request.ContentEncoding.GetBytes(s);
            return BitConverter.ToString(md.ComputeHash(bytes)).Replace("-", "").ToLower();
        }

        public static string MD5(string s, int start, int length)
        {
            return MD5(s).Substring(start, length);
        }

        public static string MD5Confusion(string s)
        {
            string str = MD5(s);
            string str2 = "";
            for (int i = 0; i < str.Length; i++)
            {
                char ch = Convert.ToChar((int)(Convert.ToInt16(str[i]) + ((i > (str.Length - i)) ? 1 : -1)));
                if ((i % 2) == 0)
                {
                    str2 = str2 + ch.ToString();
                }
                else
                {
                    str2 = ch.ToString() + str2;
                }
            }
            return str2;
        }

        public static string MD5ConfusionForMD5(string s)
        {
            string str = s;
            string str2 = "";
            for (int i = 0; i < str.Length; i++)
            {
                char ch = Convert.ToChar((int)(Convert.ToInt16(str[i]) + ((i > (str.Length - i)) ? 1 : -1)));
                if ((i % 2) == 0)
                {
                    str2 = str2 + ch.ToString();
                }
                else
                {
                    str2 = ch.ToString() + str2;
                }
            }
            return str2;
        }

        public static string ShitEncode(string s)
        {
            //string input = ConfigHelper.Get("BadWords");
            string input = null;// ConfigHelper.Get("BadWords");
            if ((input == null) || (input.Length == 0))
            {
                input = "妈的|你妈|他妈|妈b|妈比|fuck|shit|我日|法轮|我操";
            }
            else
            {
                input = Regex.Replace(Regex.Replace(input, @"\|{2,}", "|"), @"(^\|)|(\|$)", string.Empty);
            }
            return Regex.Replace(s, input, "**", RegexOptions.IgnoreCase);
        }

        public static string[] Split(string str, string splitStr)
        {
            return str.Split(new string[] { splitStr }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string SqlEncode(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            string[] strArray = @"'|/\*\*/|and|exec|insert|select|delete|update|master|truncate|declare".Split(new char[] { '|' });
            string[] strArray2 = "’|//|an&#100;|ex&#101;c|ins&#101;rt|sel&#101;ct|del&#101;te|up&#100;ate|mast&#101;r|truncat&#101;|declar&#101;".Split(new char[] { '|' });
            try
            {
                RegexOptions options = RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase;
                for (int i = 0; i < strArray.Length; i++)
                {
                    s = new Regex(string.Format("{0}", strArray[i]), options).Replace(s, strArray2[i]);
                }
            }
            catch (Exception exception)
            {
                s = string.Format("SQL注入检查出错了：", exception.Message);
            }
            return s;
        }

        public static string TextDecode(string s)
        {
            StringBuilder builder = new StringBuilder(s);
            builder.Replace("<br/><br/>", "\r\n");
            builder.Replace("<br/>", "\r");
            builder.Replace("<p></p>", "\r\n\r\n");
            return builder.ToString();
        }

        public static string TextEncode(string s)
        {
            if ((s == null) || (s.Length == 0))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder(s);
            builder.Replace("&", "&amp;");
            builder.Replace("<", "&lt;");
            builder.Replace(">", "&gt;");
            builder.Replace("\"", "&quot;");
            builder.Replace("'", "&#39;");
            return ShitEncode(builder.ToString());
        }

        public static string ToDBC(string input)
        {
            char[] chArray = input.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (chArray[i] == '　')
                {
                    chArray[i] = ' ';
                }
                else if ((chArray[i] > 0xff00) && (chArray[i] < 0xff5f))
                {
                    chArray[i] = (char)(chArray[i] - 0xfee0);
                }
            }
            return new string(chArray);
        }

        public static string ToSBC(string input)
        {
            char[] chArray = input.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (chArray[i] == ' ')
                {
                    chArray[i] = '　';
                }
                else if (chArray[i] < '\x007f')
                {
                    chArray[i] = (char)(chArray[i] + 0xfee0);
                }
            }
            return new string(chArray);
        }
    }
}

