namespace SW.Commons
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web;

    public class RegExp
    {
        private static RegexOptions regexOptions = RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled;
        /// <summary>
        /// 正则RegexOptions选项
        /// </summary>
        public static RegexOptions RegexOptions
        {
            get { return regexOptions; }
            set { regexOptions = value; }
        }

        /// <summary>
        /// 读图片地址,RegExp.patternSrc
        /// </summary>
        public static string patternSrc = @" src=[\""|\']?([^> =""']+\.(gif|jpg|jpeg|bmp|png))[\""|\']?\W";
        //@" src=[\""|\']?([^>]+?\.(gif|jpg|jpeg|bmp|png))\W";
        //@" src=[\""|\']?(.+?\.(gif|jpg|jpeg|bmp|png))[\""|\']?"

        /// <summary>
        /// 替换正则表达式特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplactStrToReg(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            string val = "^,(,),*,?,[,],$,+,|,{,}";
            string[] valArr = val.Split(',');
            foreach (string s in valArr)
            {
                str = str.Replace(s, "\\" + s);
            }
            return str;
        }

        /// <summary>
        /// 使用正则读取设定了左右边界的子串内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startStr"></param>
        /// <param name="endStr"></param>
        /// <param name="getStart"></param>
        /// <param name="getEnd"></param>
        /// <returns></returns>
        public static string GetSubStringUseReg(string text, string startStr, string endStr, bool getStart = false, bool getEnd = false)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(startStr) || string.IsNullOrEmpty(endStr) || text == "Error")
                return null;
            if (startStr.ToLower() == "regex")
            {
                if (getEnd && getStart)
                    return RegExp.GetValueFirst(text, endStr, 0);
                else
                    return RegExp.GetValueFirst(text, endStr);
            }
            string start = ReplactStrToReg(startStr);
            string end = ReplactStrToReg(endStr);
            string regStr = string.Format(@"({0})(.(?!({0})))*?({1})", start, end);
            string content = RegExp.GetValueFirst(text, regStr, 0);
            if (string.IsNullOrEmpty(content))
                return "";
            if (!getStart)
                content = content.Replace(startStr, "");
            if (!getEnd)
                content = content.Replace(endStr, "");
            return content;
        }

        #region 判断

        /// <summary>
        /// 是否存在指定的值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <param name="isSIC">不区分大小写,不换行等,内容多时请设置为true</param>
        /// <returns></returns>
        public static bool IsMatch(string s, string pattern, bool isSIC = false)
        {
            if (isSIC)
                return Regex.IsMatch(s, pattern, regexOptions);
            else
                return Regex.IsMatch(s, pattern);
        }

        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool isTel(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            //string pattern = @"^(\+?)(\d{2,3})?(1[34578]\d{9})$";
            string pattern = @"^(\+?)(\d{2,3})?(1[^12]\d{9})$";
            return Regex.IsMatch(s, pattern);
        }

        /// <summary>
        /// 是否电话(坐机)号码
        /// </summary>
        /// <param name="s"></param>
        /// <param name="checkTel">是否可是手机号</param>
        /// <returns></returns>
        public static bool isPhone(string s, bool checkTel = true)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            if (checkTel)
            {
                if (isTel(s))
                    return true;
            }
            string pattern = @"^(\(0\d{2,3}\)|0\d{2,3}-?)?[1-9]\d{6,7}(\-\d{1,4})?$";
            return Regex.IsMatch(s, pattern);
        }

        /// <summary>
        /// 是否手机浏览
        /// </summary>
        /// <param name="phoneType">手机类型：正则pattern</param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static bool isPhoneBrowser(string phoneType = "", string userAgent = "")
        {
            if (string.IsNullOrEmpty(userAgent) && HttpContext.Current != null)
                userAgent = HttpContext.Current.Request.UserAgent;
            if (string.IsNullOrEmpty(userAgent))
                return false;
            string pattern = @"(iphone|ipod|android|ios|ipad|midp|ucweb|windows ce|mobile|blackberry|webos|incognito|webmate|bada|nokia|skyfire|lg)";
            if (!string.IsNullOrEmpty(phoneType))
                pattern = string.Format("({0})", phoneType);
            return Regex.IsMatch(userAgent, pattern, regexOptions);
        }

        /// <summary>
        /// 是否微信浏览
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static bool isWxBrowser(string userAgent = "")
        {
            return isPhoneBrowser("MicroMessenger", userAgent);
        }

        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="s"></param>
        /// <param name="isUns">是无符号</param>
        /// <returns></returns>
        public static bool IsNumeric(string s, bool isUns = true)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            string pattern = @"^[0-9]+$";
            if (!isUns)
                pattern = @"^(\-|\+)?[0-9]+$";
            return Regex.IsMatch(s, pattern);
        }

        /// <summary>
        /// 是否数值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="onlyFloat">必有小数点</param>
        /// <returns></returns>
        public static bool IsFloatNum(string s, bool onlyFloat = false)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            string pattern = @"^[-\+]?\d+(\.\d+)*$";
            if (onlyFloat)
                pattern = @"^[-\+]?\d+\.\d+$";
            return Regex.IsMatch(s, pattern);
        }

        /// <summary>
        /// 是否邮箱
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmail(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            string pattern = @"^[\w-_]+(\.[\w-_]+)*@[\w-]+(\.[\w-]+)+$";
            return Regex.IsMatch(s, pattern);
        }
        /// <summary>
        /// 是否身份证号
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsCardid(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            string pattern = @"^[1-9]([0-9]{16}|[0-9]{13})[xX0-9]$";
            return Regex.IsMatch(s, pattern);
        }
        /// <summary>
        /// 是否IP地址
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsIp(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            string pattern = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";
            return Regex.IsMatch(s, pattern);
        }

        /// <summary>
        /// 是否中文
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool isChinese(string s)
        {
            string pattern = @"^[\u4e00-\u9fa5]{2,}$";
            return Regex.IsMatch(s, pattern);
        }
        public static bool IsPhysicalPath(string s)
        {
            string pattern = @"^\s*[a-zA-Z]:.*$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsRelativePath(string s)
        {
            if ((s == null) || (s == string.Empty))
            {
                return false;
            }
            if (s.StartsWith("/") || s.StartsWith("?"))
            {
                return false;
            }
            if (Regex.IsMatch(s, @"^\s*[a-zA-Z]{1,10}:.*$"))
            {
                return false;
            }
            return true;
        }

        public static bool IsSafety(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            string input = Regex.Replace(s.Replace("%20", " "), @"\s", " ");
            string pattern = "select |insert |delete from |count\\(|drop table|update |truncate |asc\\(|mid\\(|char\\(|xp_cmdshell|exec master|net localgroup administrators|:|net user|\"|\\'| or ";
            return !Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsUnicode(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            string pattern = @"^[\u4E00-\u9FA5\uE815-\uFA29]+$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsUnsFlaot(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            string pattern = "^[0-9]+.?[0-9]+$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsUnsNumeric(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            string pattern = "^[0-9]+$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsUrl(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            string pattern = @"^(http|https|ftp|rtsp|mms):(\/\/|\\\\)[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]*$";
            return Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase);
        }
        #endregion

        #region 特别
        /// <summary>
        /// 读出URL地址的域名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetUrlDomain(string url)
        {
            if (string.IsNullOrEmpty(url))
                return "";

            //string pattern = @"([^\.\\\/]+(\.(com|cn|gov|net|org|tv|mi|biz|cc)){1,2}|localhost|((\d+\.){3,3}\d+))(:\d+)?(?=[\W]|$)";
            string pattern = @"([^\.\\\/]+(\.(com|cn|net|cc|tv|org|edu|gov|mobi|tel|info|int|vip|xin|xyz|ltd|shop|wang|club|top|site|ren|group|link|red|ink|pro|biz|kim|name|mi|mil|museum|aero|coop|travel|asia|jobs|cat)){1,2}|localhost|((\d+\.){3,3}\d+))(:\d+)?(?=[\W]|$)";
            
            if (RegExp.RegIsMatch(url, pattern))
            {
                return RegExp.GetValueFirst(url, pattern, 0);
            }
            return "";
        }

        #endregion

        #region 取值
        /// <summary>
        /// 使用正则读取第一个值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <param name="groupIndex">组别</param>
        /// <param name="isAsync">使用线程控制匹配时间为1秒内</param>
        /// <returns></returns>
        public static string GetValueFirst(string str, string pattern, int groupIndex = 1, bool isAsync = false)
        {
            MatchCollection mat = GetValueMC(str, pattern, isAsync);
            if (mat != null && mat.Count > 0)// 这里有可能造成cpu资源耗尽
            {
                Match m = mat[0];
                if (m.Groups.Count >= groupIndex + 1)
                    return m.Groups[groupIndex].Value;
            }
            return "";
        }

        /// <summary>
        /// 使用正则读取所有值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <param name="isAsync">使用线程控制匹配时间为1秒内</param>
        /// <returns></returns>
        public static MatchCollection GetValueMC(string str, string pattern, bool isAsync = false)
        {
            //AppDomain.CurrentDomain.SetData("REGEX_DEFAULT_MATCH_TIMEOUT", TimeSpan.FromSeconds(1));
            Regex reg = new Regex(pattern, regexOptions);
            MatchCollection mat = null;
            if (!isAsync)
                mat = reg.Matches(str);// 这里有可能造成cpu资源耗尽
            else
            {
                RegExpAsync rea = new RegExpAsync();
                mat = rea.Matches(reg, str);
            }
            return mat;
        }

        #endregion

        #region 替换

        /// <summary>
        /// 替换字符串中所有非汉字为空
        /// </summary>
        /// <param name="str">要替换的字符串</param>
        /// <returns></returns>
        public static string GetGB2312(string str)
        {
            return GetGB2312(str, "");
        }
        /// <summary>
        /// 替换字符串中所有非汉字为指定的字符
        /// </summary>
        /// <param name="str">要替换的字符串</param>
        /// <param name="repstr">替换成的字符</param>
        /// <returns></returns>
        public static string GetGB2312(string str, string repstr)
        {
            return RegReplace(str, @"[^a-zA-Z0-9\u4E00-\u9FA5]+", repstr);
        }

        /// <summary>
        /// 去掉重复的逗号，及头尾逗号
        /// </summary>
        /// <param name="str"></param>
        /// <param name="delStartEnd"></param>
        /// <returns></returns>
        public static string RegReplaceComma(string str, bool delStartEnd = true)
        {
            string s = RegReplace(str, @"(,{2,})", ",");
            if (delStartEnd)
                return RegReplace(s, @"(^(,+)|(,+)$)", "");
            return s;
        }

        /// <summary>
        /// 正则替换
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="nstr">替换成的字符</param>
        /// <returns></returns>
        public static string RegReplace(string str, string pattern, string nstr)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            try
            {
                Regex _regItemKey = new Regex(pattern, RegexOptions);
                return _regItemKey.Replace(str, nstr);
            }
            catch
            {
                return str;
            }
        }


        /// <summary>
        /// 是否找到匹配项
        /// </summary>
        /// <param name="str">要查找的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static bool RegIsMatch(string str, string pattern)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            try
            {
                Regex _regItemKey = new Regex(pattern, RegexOptions);
                return _regItemKey.IsMatch(str);
            }
            catch
            {
                return false;
            }
        }


        #endregion


        #region 过滤
        /// <summary>
        /// 过滤HTML标记 0:单标签全过滤,1:双标签全过滤,2:双标签保留内容,3:去掉属性,4:双标签无内容,11:无属性 双标签全过滤,12:无属性 双标签保留内容(<i>-<img><u>-<ul>)
        /// </summary>
        /// <param name="body">要过滤的HTML代码</param>
        /// <param name="TagName">标记名称</param>
        /// <param name="TagType">标记类型</param>
        /// <returns></returns>
        public static string RegReplaceTag(string body, string TagName, int TagType = 1)
        {
            string newbody = body;
            string pattern = "";
            switch (TagType)
            {
                case 0:
                    pattern = string.Format(@"<{0}([^>])*>", TagName);//普通单个标记 如<br /><img src="" /> <a href="">
                    break;
                case 1:
                    pattern = string.Format(@"<{0}([^>])*>[\s\S]*?</{0}([^>])*>", TagName);//成对标记及内容 如<a href="">...</a>
                    break;
                case 2:
                    pattern = string.Format(@"<{0}([^>])*>|</{0}([^>])*>", TagName); //单个的成对标记
                    break;
                case 3:
                    pattern = string.Format(@"(<{0})[^>]*([/]?>)", TagName);//去掉标签属性 如样式
                    break;
                case 4:
                    pattern = string.Format(@"<{0}([^>])*>[\s]*?</{0}([^>])*>", TagName);//成对标记无内容 如<p></p>
                    break;
                case 11:
                    pattern = string.Format(@"<{0}>[\s\S]*?</{0}>", TagName);//无属性 成对标记及内容 如<i>...</i> 避免替换了img <u>-<ul>
                    break;
                case 12:
                    pattern = string.Format(@"<{0}>|</{0}>", TagName); //无属性 单个的成对标记 如<i>...</i> 避免替换了img <u>-<ul>
                    break;
            }

            Regex reg = new Regex(pattern, regexOptions);
            if (TagType == 3)
            {
                newbody = reg.Replace(body, "$1$2");
            }
            else
            {
                newbody = reg.Replace(body, "");
            }
            return newbody;
        }

        /// <summary>
        /// 替换掉指定HTML标记
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tags">开关20个:Iframe|Object|Script|Style|Div|Table-Tbody|Tr|Td-Th|Span-Strong-b-em-u-i-s|Img|Font|A|Input-Form|P|Br|Style|ul-ol-dl|li|dt|dd</param>
        /// <returns></returns>
        public static string ReplaceHtmlTag(string html, string tags = "1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1")
        {
            if (string.IsNullOrEmpty(html) || string.IsNullOrEmpty(tags))
                return "";
            if (html.IndexOf("<") == -1)
                return html;
            string[] Arr = new string[20];
            string[] arr = new string[0];
            if (!string.IsNullOrEmpty(tags))
                arr = tags.Split('|');
            for (int i = 0; i < Arr.Length; i++)
            {
                if (i < arr.Length)
                    Arr[i] = arr[i];
                else
                    Arr[i] = "0";
            }
            string reHtml = html;
            if (Arr[0] == "1")
                reHtml = RegExp.RegReplaceTag(reHtml, "Iframe", 1);
            if (Arr[1] == "1")
                reHtml = RegExp.RegReplaceTag(reHtml, "Object", 1);
            if (Arr[2] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "Script", 1);
                reHtml = RegExp.RegReplaceTag(reHtml, "NoScript", 1);
            }
            if (Arr[3] == "1")
                reHtml = RegExp.RegReplaceTag(reHtml, "Style", 1);
            if (Arr[4] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "Div", 2);
                reHtml = RegExp.RegReplaceTag(reHtml, "Div", 4);
            }
            if (Arr[5] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "Table", 2);
                reHtml = RegExp.RegReplaceTag(reHtml, "Tbody", 2);
            }
            if (Arr[6] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "Tr", 2);
            }
            if (Arr[7] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "Th", 2);
                reHtml = RegExp.RegReplaceTag(reHtml, "Td", 2);
            }
            if (Arr[8] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "Span", 2);
                reHtml = RegExp.RegReplaceTag(reHtml, "Label", 2);
                reHtml = RegExp.RegReplaceTag(reHtml, "Strong", 2);
                reHtml = RegExp.RegReplaceTag(reHtml, "b", 12);
                reHtml = RegExp.RegReplaceTag(reHtml, "em", 2);
                reHtml = RegExp.RegReplaceTag(reHtml, "u", 12);
                reHtml = RegExp.RegReplaceTag(reHtml, "i", 12);
                reHtml = RegExp.RegReplaceTag(reHtml, "s", 12);
                //reHtml = RegReplace(reHtml, @"<!--(.)*?-->", "");
            }
            if (Arr[9] == "1")
                reHtml = RegExp.RegReplaceTag(reHtml, "Img", 0);
            if (Arr[10] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "Font", 2);
                reHtml = RegExp.RegReplaceTag(reHtml, "Font", 4);
            }
            if (Arr[11] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "A", 2);
                reHtml = RegExp.RegReplaceTag(reHtml, "A", 4);
            }
            if (Arr[12] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "Input", 0);
                reHtml = RegExp.RegReplaceTag(reHtml, "Form", 2);
                reHtml = RegExp.RegReplaceTag(reHtml, "Button", 0);
            }
            if (Arr[13] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "P", 3);
                reHtml = RegReplace(reHtml, @"<p\s*>([\s|&nbsp;| |　]+)", "<p>");
                //reHtml = RegReplace(reHtml, @"<p\s+[^>]*>", "<p>");
                reHtml = RegExp.RegReplaceTag(reHtml, "P", 4);
            }
            if (Arr[14] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "Br", 3);
            }
            if (Arr[15] == "1")
            {
                reHtml = RegReplace(reHtml, @"([ ]?style=[\""|\']?[^\""|\']*[\""|\']?)", "");
            }
            if (Arr[16] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "ul", 2);
                reHtml = RegExp.RegReplaceTag(reHtml, "ol", 2);
                reHtml = RegExp.RegReplaceTag(reHtml, "dl", 2);
            }
            if (Arr[17] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "li", 2);
            }
            if (Arr[18] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "dt", 2);
            }
            if (Arr[19] == "1")
            {
                reHtml = RegExp.RegReplaceTag(reHtml, "dd", 2);
            }
            
        //$reHtml = RegReplaceTag($reHtml, "h1", 2);
        //$reHtml = RegReplaceTag($reHtml, "h2", 2);
        //$reHtml = RegReplaceTag($reHtml, "h3", 2);
        //$reHtml = RegReplaceTag($reHtml, "h4", 2);
            return reHtml;
        }

        #endregion


        public static string[] GetDifDateAndTime(object todate, object fodate)
        {
            string[] strArray = new string[2];
            TimeSpan span = (TimeSpan)(DateTime.Parse(todate.ToString()) - DateTime.Parse(fodate.ToString()));
            double num = span.TotalSeconds / 86400.0;
            num.ToString();
            int length = num.ToString().Length;
            int startIndex = num.ToString().LastIndexOf(".");
            int num4 = (int)Math.Round(num, 10);
            int num5 = (int)(double.Parse("0" + num.ToString().Substring(startIndex, length - startIndex)) * 24.0);
            strArray[0] = num4.ToString();
            strArray[1] = num5.ToString();
            return strArray;
        }

        public static string GetDifDateAndTime(object todate, object fodate, string v1, string v2, string v3, string v4, string v5, string v6)
        {
            TimeSpan span = (TimeSpan)(DateTime.Parse(todate.ToString()) - DateTime.Parse(fodate.ToString()));
            int num = ((int)span.TotalDays) / 0x16d;
            int num2 = (int)(((span.TotalDays / 365.0) - ((int)(span.TotalDays / 365.0))) * 12.0);
            int num3 = (span.Days - (num * 0x16d)) - (num2 * 30);
            int hours = span.Hours;
            int minutes = span.Minutes;
            string str = "";
            if (num != 0)
            {
                str = str + num.ToString() + v1;
            }
            if (num2 != 0)
            {
                str = str + num2.ToString() + v2;
            }
            if (num3 != 0)
            {
                str = str + num3.ToString() + v3;
            }
            if (hours != 0)
            {
                str = str + hours.ToString() + v4;
            }
            if (minutes != 0)
            {
                str = str + minutes.ToString() + v5;
            }
            if ((((num == 0) && (num2 == 0)) && ((num3 == 0) && (hours == 0))) && (minutes == 0))
            {
                return v6;
            }
            return str;
        }

        public static string[] GetPercence(int a, int b)
        {
        Label_0000:
            while (((a % 2) == 0) && ((b % 2) == 0))
            {
                a /= 2;
                b /= 2;
            }
            if (((a % 3) == 0) && ((b % 3) == 0))
            {
                a /= 3;
                b /= 3;
                goto Label_0000;
            }
            if (((a % 5) == 0) && ((b % 5) == 0))
            {
                a /= 5;
                b /= 5;
                goto Label_0000;
            }
            if (((a % 7) == 0) && ((b % 7) == 0))
            {
                a /= 7;
                b /= 7;
                goto Label_0000;
            }
            return new string[] { a.ToString(), b.ToString() };
        }
    }
}

