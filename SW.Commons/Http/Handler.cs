

using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using SW.Commons.Enums;

namespace SW.Commons.Http
{

    public class Handler
    {
        private static string[] _WebSearchList = new string[] {
            "google", "isaac", "surveybot", "baiduspider", "yahoo", "yisou", "3721", "qihoo", "daqi", "ia_archiver", "p.arthur", "fast-webcrawler", "java", "microsoft-atl-native", "turnitinbot", "webgather",
            "sleipnir", "msn"
         };

        public static HttpServerUtility Server
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Server;
                return null;
            }
        }

        /// <summary>
        /// 根据类型检查数据合法性
        /// </summary>
        /// <param name="words"></param>
        /// <param name="chkType"></param>
        /// <returns></returns>
        public static string CheckWords(string words, CheckGetEnum chkType, string defaultValue = "")
        {
            string str = words;
            bool flag = false;
            if (!string.IsNullOrEmpty(str))
            {
                str = words.Trim();
                switch (chkType)
                {
                    case CheckGetEnum.Int:
                        try
                        {
                            if (str.Contains(","))
                                str = str.Trim(',').Split(',')[0];
                            int.Parse(str);
                            //flag = RegExp.IsNumeric(str, false);
                        }
                        catch
                        {
                            str = defaultValue;
                        }
                        goto Label_00CF;

                    case CheckGetEnum.Safety:
                        str = DangerousTagsCheck.SafeHtml(str);
                        flag = RegExp.IsSafety(str);
                        goto Label_00CF;

                    case CheckGetEnum.UnSafety:
                        flag = true;
                        goto Label_00CF;

                    case CheckGetEnum.Long:
                        try
                        {
                            if (str.Contains(","))
                                str = str.Trim(',').Split(',')[0];
                            long.Parse(str);
                            //flag = RegExp.IsNumeric(str);
                        }
                        catch
                        {
                            str = defaultValue;
                        }
                        goto Label_00CF;

                    case CheckGetEnum.ScripteSafe:
                        str = DangerousTagsCheck.SafeHtml(str);
                        flag = true;
                        goto Label_00CF;
                }
                flag = false;
            }
            else
            {
                switch (chkType)
                {
                    case CheckGetEnum.Int:
                    case CheckGetEnum.Long:
                        str = defaultValue;
                        break;

                    case CheckGetEnum.Safety:
                        str = defaultValue;
                        break;

                    default:
                        str = defaultValue;
                        break;
                }
                flag = true;
            }
            Label_00CF:
            if (!flag)
            {
                return Text.SqlEncode(str);
            }
            return str;
        }


        public static string GetCode
        {
            get
            {
                return "<span id=\"getcodeSpan\" style=\"cursor:pointer;\" onclick=\"GetCode(this);\">点击获取</span><script type=\"text/javascript\">function GetCode(obj){obj.innerHTML = \"<img src='/GetCode.aspx?time=\" + Math.random() + \"' style='cursor:pointer;' id='getcodeimg'  alt='验证码，看不清楚？点击更换。' />\";}</script>";
            }
        }

        public static bool GetCodeCheck
        {
            get
            {
                string str = Request("getCode", GetTypeEnum.QueryAndPost, CheckGetEnum.Safety);
                object andRemove = SessionState.GetAndRemove("getcode");
                string str2 = (andRemove == null) ? string.Empty : andRemove.ToString();
                return ((str != string.Empty) && (str == str2));
            }
        }

        public static string GetCodeX(string getCodeUrl, string tip)
        {
            return GetCodeX(getCodeUrl, tip, true);
        }

        public static string GetCodeX(string getCodeUrl, string tip, bool isNeedClick)
        {
            if (isNeedClick)
            {
                return ("<span id=\"getcodeSpan\" style=\"cursor:pointer;\" onclick=\"GetCode(this);\">点击获取</span><script type=\"text/javascript\">function GetCode(obj){obj.innerHTML = \"<img src='" + (string.IsNullOrEmpty(getCodeUrl) ? "/GetCode.aspx" : getCodeUrl) + "?time=\" + Math.random() + \"' style='cursor:pointer;' id='getcodeimg'  alt='" + (string.IsNullOrEmpty(tip) ? "验证码，看不清楚？点击更换。" : tip) + "' />\";}</script>");
            }
            return ("<a href=\"#\" id=\"getcodeSpan\" style=\"cursor:pointer;\" onclick=\"GetCode(this);\"><img src='" + (string.IsNullOrEmpty(getCodeUrl) ? "/GetCode.aspx" : getCodeUrl) + "?time=\" + Math.random() + \"' style='cursor:pointer;' id='getcodeimg' border='0'  alt='" + (string.IsNullOrEmpty(tip) ? "验证码，看不清楚？点击更换。" : tip) + "' /></a><script type=\"text/javascript\">function GetCode(obj){obj.innerHTML = \"<img src='" + (string.IsNullOrEmpty(getCodeUrl) ? "/GetCode.aspx" : getCodeUrl) + "?time=\" + Math.random() + \"' style='cursor:pointer;' id='getcodeimg' border='0'  alt='" + (string.IsNullOrEmpty(tip) ? "验证码，看不清楚？点击更换。" : tip) + "' />\";}</script>");
        }

        public static string HtmlDecode(string words)
        {
            return HttpContext.Current.Server.HtmlDecode(words);
        }

        public static string HtmlEncode(string words)
        {
            return HttpContext.Current.Server.HtmlEncode(words);
        }

        public static bool IsWebSearch()
        {
            string userAgent = HttpContext.Current.Request.UserAgent;
            if ((userAgent == null) || (string.Empty == userAgent))
            {
                return true;
            }
            userAgent = userAgent.ToLower();
            for (int i = 0; i < _WebSearchList.Length; i++)
            {
                if (-1 != userAgent.IndexOf(_WebSearchList[i]))
                {
                    return true;
                }
            }
            return UserBrowser.Equals("Unknown");
        }

        public static string MapPath(string path)
        {
            if (RegExp.IsPhysicalPath(path))
            {
                return path;
            }
            if (HttpContext.Current != null)
            {
                try
                {
                    return HttpContext.Current.Server.MapPath(path);
                }
                catch
                {
                    ShowMsg("出错了：读取路径出错。", MessageEnum.Error);
                    goto Label_003D;
                }
            }
            ShowMsg("HttpContext.Current 为空引用", MessageEnum.Error);
            Label_003D:
            return string.Empty;
        }

        public static string MapPath(string path, string parentPath)
        {
            if (RegExp.IsPhysicalPath(path))
            {
                return path;
            }
            if (HttpContext.Current != null)
            {
                try
                {
                    return HttpContext.Current.Server.MapPath(path);
                }
                catch
                {
                    ShowMsg("出错了：读取路径出错。", MessageEnum.Error);
                    goto Label_003D;
                }
            }
            ShowMsg("HttpContext.Current 为空引用", MessageEnum.Error);
            Label_003D:
            return string.Empty;
        }

        public static string MD5(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            bytes = new MD5CryptoServiceProvider().ComputeHash(bytes);
            string str2 = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str2 = str2 + bytes[i].ToString("x").PadLeft(2, '0');
            }
            return str2;
        }

        public static string Request(string name, GetTypeEnum gType, CheckGetEnum chkType)
        {
            return Request(name, gType, chkType, "");
        }

        public static string Request(string name, GetTypeEnum gType, CheckGetEnum chkType, string defaultValue)
        {
            name = name.Trim();
            string str;
            switch (gType)
            {
                case GetTypeEnum.QueryString:
                    str = HttpContext.Current.Request.QueryString[name];
                    if (str != null)
                    {
                        str = CheckWords(str, chkType, defaultValue);
                    }
                    break;

                case GetTypeEnum.Post:
                    str = HttpContext.Current.Request.Form[name];
                    if (str != null)
                    {
                        str = CheckWords(str, chkType, defaultValue);
                    }
                    break;

                case GetTypeEnum.QueryAndPost:
                    str = HttpContext.Current.Request.QueryString[name];
                    if (str == null)
                    {
                        str = HttpContext.Current.Request.Form[name];
                        if (str != null)
                        {
                            str = CheckWords(str, chkType, defaultValue);
                        }
                        break;
                    }
                    str = CheckWords(str, chkType, defaultValue);
                    break;

                case GetTypeEnum.PostAndQuery:
                    str = HttpContext.Current.Request.Form[name];
                    if (str == null)
                    {
                        str = HttpContext.Current.Request.QueryString[name];
                        if (str != null)
                        {
                            str = CheckWords(str, chkType, defaultValue);
                        }
                        break;
                    }
                    str = CheckWords(str, chkType, defaultValue);
                    break;

                default:
                    str = HttpContext.Current.Request.QueryString[name];
                    if (str != null)
                    {
                        str = CheckWords(str, chkType, defaultValue);
                    }
                    break;
            }
            if ((chkType == CheckGetEnum.Int) || (chkType == CheckGetEnum.Long))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    return str.Trim();
                }
                if (!string.IsNullOrEmpty(defaultValue))
                {
                    return defaultValue;
                }
                return "0";
            }
            if (str != null)
            {
                return str.Trim();
            }
            return defaultValue;
        }

        public static string RequestPost(string name)
        {
            return Request(name, GetTypeEnum.Post, CheckGetEnum.Safety);
        }

        public static DateTime RequestPost(string name, DateTime defaultValue)
        {
            DateTime result = defaultValue;
            DateTime.TryParse(Request(name, GetTypeEnum.Post, CheckGetEnum.Safety, defaultValue.ToString()), out result);
            return result;
        }

        public static decimal RequestPost(string name, decimal defaultValue)
        {
            return decimal.Parse(Request(name, GetTypeEnum.Post, CheckGetEnum.Safety, defaultValue.ToString()));
        }

        public static int RequestPost(string name, int defaultValue)
        {
            return int.Parse(Request(name, GetTypeEnum.Post, CheckGetEnum.Int, defaultValue.ToString()));
        }

        public static int RequestPostInt(string name)
        {
            return RequestPost(name, 0);
        }

        public static string RequestPostString(string name)
        {
            return RequestPost(name);
        }

        public static string RequestQueryAndPost(string name)
        {
            return Request(name, GetTypeEnum.QueryAndPost, CheckGetEnum.Safety);
        }


        public static string RequestPostAndQuery(string name)
        {
            return Request(name, GetTypeEnum.PostAndQuery, CheckGetEnum.Safety);
        }

        public static int RequestPostAndQuery(string name, int defaultValue)
        {
            return int.Parse(Request(name, GetTypeEnum.PostAndQuery, CheckGetEnum.Int, defaultValue.ToString()));
        }

        public static DateTime RequestQueryAndPost(string name, DateTime defaultValue)
        {
            DateTime result = defaultValue;
            DateTime.TryParse(Request(name, GetTypeEnum.QueryAndPost, CheckGetEnum.Safety, defaultValue.ToString()), out result);
            return result;
        }

        public static decimal RequestQueryAndPost(string name, decimal defaultValue)
        {
            return decimal.Parse(Request(name, GetTypeEnum.QueryAndPost, CheckGetEnum.Safety, defaultValue.ToString()));
        }

        public static int RequestQueryAndPost(string name, int defaultValue)
        {
            return int.Parse(Request(name, GetTypeEnum.QueryAndPost, CheckGetEnum.Int, defaultValue.ToString()));
        }

        public static long RequestQueryAndPostLong(string name, long defaultValue)
        {
            return long.Parse(Request(name, GetTypeEnum.QueryAndPost, CheckGetEnum.Long, defaultValue.ToString()));
        }

        public static int RequestQueryInt(string name)
        {
            return int.Parse(Request(name, GetTypeEnum.QueryString, CheckGetEnum.Int, "0"));
        }

        public static string RequestQueryString(string name)
        {
            return Request(name, GetTypeEnum.QueryString, CheckGetEnum.Safety);
        }

        private static void Response(string words)
        {
            HttpContext.Current.Response.Write(words);
        }

        private static void ResponseEnd()
        {
            HttpContext.Current.Response.End();
        }

        public static int SecondToMinute(int Second)
        {
            decimal d = Second / 60M;
            return Convert.ToInt32(Math.Ceiling(d));
        }

        public static void ShowMsg(string words, MessageEnum msgType)
        {
            switch (msgType)
            {
                case MessageEnum.Error:
                    HttpContext.Current.Response.Clear();
                    Response(words);
                    ResponseEnd();
                    return;

                case MessageEnum.Message:
                    Response(words);
                    return;
            }
            Response(words);
        }

        public static string UrlDecode(string words)
        {
            return HttpContext.Current.Server.UrlDecode(words);
        }

        public static string UrlEncode(string words)
        {
            return HttpContext.Current.Server.UrlEncode(words);
        }

        public static string ValidateAspxCode(string content, string[] allownIncludes, bool allownAspx, int allownLength, int allownNum)
        {
            RegexOptions options = RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase;
            string pattern = "<%(.*?)%>";
            string str2 = "<!--#include.*?=\"(.*?)\".*?-->";
            string str3 = "script.*runat";
            Regex regex = new Regex(pattern, options);
            Regex regex2 = new Regex(str2, options);
            Regex regex3 = new Regex(str3, options);
            if (!allownAspx || regex3.IsMatch(content))
            {
                content = Regex.Replace(content, pattern, "<!-- delete code,[不允许aspx或runat server] /-->");
            }
            else
            {
                string[] strArray = new string[] { "System.IO", "Directory.", "File.", "StreamReader", "StreamWriter", "Execute", "select", "delete", "insert", "update" };
                MatchCollection matchs = regex.Matches(content);
                int num = 0;
                foreach (Match match in matchs)
                {
                    string str4 = match.Groups[1].Value.Trim().ToLower();
                    bool flag = false;
                    foreach (string str5 in strArray)
                    {
                        if (str4.Contains(str5.ToLower()))
                        {
                            content = content.Replace(match.Value, "<!-- delete code 2,[禁止字符] /-->");
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        if (str4.Length > allownLength)
                        {
                            content = content.Replace(match.Value, "<!-- delete code 3,[长度超长] /-->");
                            break;
                        }
                        if (num >= allownNum)
                        {
                            content = content.Replace(match.Value, "<!-- delete code 4,[总数超过] /-->");
                            break;
                        }
                        num++;
                    }
                }
            }
            if ((allownIncludes == null) || (allownIncludes.Length < 1))
            {
                content = Regex.Replace(content, str2, "<!-- delete include file /-->");
                return content;
            }
            foreach (Match match2 in regex2.Matches(content))
            {
                string input = match2.Groups[1].Value.Trim().ToLower();
                Match match3 = new Regex("/(.*?)/.*", options).Match(input);
                if (!match3.Success)
                {
                    content = content.Replace(match2.Value, "<!-- delete include file 2 /-->");
                    continue;
                }
                bool flag2 = false;
                foreach (string str7 in allownIncludes)
                {
                    if (str7.ToLower().Equals(match3.Groups[1].Value.ToLower()))
                    {
                        flag2 = true;
                        break;
                    }
                }
                if (!flag2)
                {
                    content = content.Replace(match2.Value, "<!-- delete include file 2 /-->");
                }
            }
            return content;
        }

        public static string CurrentPageName
        {
            get
            {
                string[] strArray = HttpContext.Current.Request.Url.AbsolutePath.Split(new char[] { '/' });
                return strArray[strArray.Length - 1].ToLower();
            }
        }

        public static string CurrentPath
        {
            get
            {
                string path = HttpContext.Current.Request.Path;
                path = path.Substring(0, path.LastIndexOf("/"));
                if (path == "/")
                {
                    return string.Empty;
                }
                return path;
            }
        }

        public static string CurrentUrl
        {
            get
            {
                return HttpContext.Current.Request.Url.ToString();
            }
        }

        public static bool IsRobots
        {
            get
            {
                return IsWebSearch();
            }
        }

        public static string Referrer
        {
            get
            {
                Uri urlReferrer = HttpContext.Current.Request.UrlReferrer;
                //HttpContext.Current.Request.RawUrl
                if (urlReferrer == null)
                {
                    return string.Empty;
                }
                return Convert.ToString(urlReferrer);
            }
        }

        public static string rootPath
        {
            get
            {
                return MapPath(".");
            }
        }


        public static string CookieDomain
        {
            get
            {
                string s = HttpContext.Current.Request.Url.ToString().ToLower();
                string _domain = RegExp.GetUrlDomain(s);
                if (string.IsNullOrEmpty(_domain))
                    return ServerDomain;
                else
                    return _domain;
            }
        }

        public static string ServerDomain
        {
            get
            {
                string s = HttpContext.Current.Request.Url.Host.ToLower();
                string str2 = (HttpContext.Current.Request.Url.Port == 80) ? "" : (":" + HttpContext.Current.Request.Url.Port.ToString());
                if ((s.Split('.').Length >= 3) && !RegExp.IsIp(s))
                {
                    string str3 = s.Remove(0, s.IndexOf(".") + 1);
                    if ((!str3.StartsWith("com.") && !str3.StartsWith("net.")) && (!str3.StartsWith("org.") && !str3.StartsWith("gov.")))
                    {
                        return (str3 + str2);
                    }
                }
                return (s + str2);
            }
        }

        /// <summary>
        /// 浏览器UserAgent信息
        /// </summary>
        public static string UserAgent
        {
            get
            {
                string userAgent = HttpContext.Current.Request.UserAgent;
                if (string.IsNullOrEmpty(userAgent))
                    userAgent = "";
                userAgent = userAgent.ToLower();
                return userAgent;
            }
        }

        public static string UserBrowser
        {
            get
            {
                string userAgent = HttpContext.Current.Request.UserAgent;
                if (string.IsNullOrEmpty(userAgent))
                    return "Unknown";
                userAgent = userAgent.ToLower();
                HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
                return (browser.Browser + browser.Version);
                //if ((((userAgent.IndexOf("firefox") < 0) && (userAgent.IndexOf("firebird") < 0)) && ((userAgent.IndexOf("myie") < 0) && (userAgent.IndexOf("opera") < 0))) && ((userAgent.IndexOf("netscape") < 0) && (userAgent.IndexOf("msie") < 0)))
                //{
                //    return "Unknown";Chrome,default,mozilla,webkit,chrome
                //}
            }
        }

        public static string UserIp
        {
            get
            {
                try
                {
                    string s = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (!string.IsNullOrEmpty(s))
                    {
                        //有代理,可能有多个 未考虑IPV6
                        if (s.IndexOf(".") == -1)
                            s = "";
                        else
                        {
                            if (s.IndexOf(",") != -1)
                            {
                                //有, 估计多个代理。取第一个不是内网的IP。 
                                s = s.Replace(" ", "").Replace("'", "").Replace(";", "");
                                string[] temparyip = s.Split(',');
                                foreach (string ip in temparyip)
                                {
                                    if (RegExp.IsIp(ip)
                                        && ip.Substring(0, 3) != "10."
                                        && ip.Substring(0, 7) != "192.168"
                                        && ip.Substring(0, 7) != "172.16.")
                                    {
                                        return ip;
                                    }
                                }
                            }
                            else if (!RegExp.IsIp(s))
                                s = "";
                        }
                    }

                    if (string.IsNullOrEmpty(s))
                        s = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    if (string.IsNullOrEmpty(s))
                        s = HttpContext.Current.Request.UserHostAddress;
                    if (string.IsNullOrEmpty(s))
                        return "Unknown";

                    if (RegExp.IsIp(s))
                        return s;

                    try
                    {
                        //foreach (IPAddress ip in Dns.GetHostAddresses(s))
                        foreach (IPAddress ip in Dns.GetHostEntry(s).AddressList)
                        {
                            if (ip.AddressFamily.ToString() == "InterNetwork")
                            {
                                return ip.ToString();
                            }
                        }
                    }
                    catch
                    {
                        return s;
                    }
                    return s;
                }
                catch {
                    return "";
                }
            }
        }

        public static string webCurrentUrl
        {
            get
            {
                return HttpContext.Current.Request.RawUrl.ToString();
            }
        }
    }
}

