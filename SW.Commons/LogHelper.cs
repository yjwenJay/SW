using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Web;

namespace SW
{
    /// <summary>
    /// 
    /// </summary>
    public class LogHelper
    {

        #region 新加分文件夹写入日志
        /// <summary>
        /// 微信日志记录
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool LogWX(string formatStr, params object[] args)
        {
            return LogWrite(formatStr, "weixin_", args);
        }

        public static bool LogWXError(string formatStr, params object[] args)
        {
            return LogWrite(formatStr, "weixin_error_", args);
        }

        public static bool LogOther(string formatStr, params object[] args)
        {
            return LogWrite(formatStr, "other_", args);
        }

        public static bool LogPushServer(string formatStr, params object[] args)
        {
            return LogWrite(formatStr, "pushServer_", args);
        }
        /// <summary>
        /// 分文件夹写入日志
        /// </summary>
        /// <param name="formatStr"></param>
        /// <param name="preStr"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool LogWrite(string formatStr, string preStr, params object[] args)
        {
            try
            {
                string Logpath = string.Empty;
                if (HttpContext.Current != null)
                    Logpath = HttpContext.Current.Server.MapPath("/App_Data/Log");
                else
                {
                    Logpath = System.Windows.Forms.Application.StartupPath + "/Log";
                    if (Logpath.ToString().Contains("iis") || Logpath.ToString().Contains("c:"))
                    {
                        //Logpath = "D:/wwwRoot/Log";
                        return false;
                    }
                }
                lock (Logpath)
                {
                    DateTime _fileDate = DateTime.Now;
                    string fileName = "";
                    if (!System.IO.Directory.Exists(Logpath))
                    {
                        System.IO.Directory.CreateDirectory(Logpath);
                    }
                    fileName = string.Format("{0}\\{2}{1}.log", Logpath, _fileDate.ToString("yyyy-MM-dd"), preStr);
                    string content = string.Format("{0}   {1}\r\n", DateTime.Now.ToString("HH:mm:ss.ms"), string.Format(formatStr, args));
                    System.IO.File.AppendAllText(fileName, content);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 当前的日志
        /// </summary>
        /// <param name="text"></param>
        public static void WriteLogs(string text)
        {
            try
            {
                string logPath = ConfigurationManager.AppSettings["logsPath"];
                DateTime dt = DateTime.Now;
                logPath = logPath + "/Info/" + dt.ToString("yyyy-MM") + "/";
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                string fullName = logPath + dt.ToString("yyyyMMdd HH") + ".log";
                StreamWriter sw = null;
                FileStream fs = null;
                if (!File.Exists(fullName))
                {
                    fs = new FileStream(fullName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    //sw = File.CreateText(fullName);
                }
                else
                {
                    fs = new FileStream(fullName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                }
                sw = new StreamWriter(fs, Encoding.Default);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + "     " + text);
                sw.Close();
                fs.Close();
            }
            catch { }
        }

        /// <summary>
        /// 写黑名单IP日志
        /// </summary>
        /// <param name="text"></param>
        /// <param name="isMan"></param>
        public static void WriteBlackIPLogs(string text, bool isMan)
        {
            try
            {
                string logPath = ConfigurationManager.AppSettings["logsPath"];
                DateTime dt = DateTime.Now;
                string fullName = string.Empty;
                if (isMan)
                {
                    fullName = logPath + "/IP/blacklist.txt";

                }
                else
                {
                    logPath = logPath + "/IP/" + dt.ToString("yyyy-MM") + "/";
                    logPath = logPath + dt.ToString("yyyyMMdd") + ".log";
                }
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                StreamWriter sw = null;
                FileStream fs = null;

                if (!File.Exists(fullName))
                {
                    fs = new FileStream(fullName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                }
                else
                {
                    fs = new FileStream(fullName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                }

                sw = new StreamWriter(fs, Encoding.Default);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(text);
                sw.Close();
                fs.Close();
            }
            catch { }
        }


        /// <summary>
        /// 写白名单IP日志
        /// </summary>
        /// <param name="text"></param>
        public static void WriteWhiteIPLogs(string text)
        {
            try
            {
                string logPath = ConfigurationManager.AppSettings["logsPath"];
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                string fullName = logPath + "/IP/whitelist.txt";
                File.Delete(fullName);

                StreamWriter sw = null;
                FileStream fs = null;

                if (!File.Exists(fullName))
                {
                    fs = new FileStream(fullName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                }
                else
                {
                    fs = new FileStream(fullName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                }
                sw = new StreamWriter(fs, Encoding.Default);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(text);
                sw.Close();
                fs.Close();
            }
            catch { }
        }

        /// <summary>
        /// 读取黑名单IP或白名单IP
        /// </summary>
        /// <param name="type">0-黑名单IP，1-白名单IP</param>
        /// <returns></returns>
        public static string ReadIPLogs(int type)
        {
            string result = string.Empty;
            try
            {
                DateTime dt = DateTime.Now;
                string logPath = ConfigurationManager.AppSettings["logsPath"];
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                string fullName = string.Empty;
                if (type.Equals(0))
                    fullName = logPath + "/IP/blacklist.txt";
                else if (type.Equals(1))
                    fullName = logPath + "/IP/whitelist.txt";
                FileStream fs = null;
                if (File.Exists(fullName))
                {
                    fs = new FileStream(fullName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    StreamReader sr = new StreamReader(fs, Encoding.Default);
                    result = sr.ReadToEnd();
                    sr.Close();
                    fs.Close();
                }

            }
            catch (Exception ex)
            {
                WriteErrorLogs(ex.Message + ex.StackTrace);
            }
            return result;
        }


        /// <summary>
        /// 单个错误日志
        /// </summary>
        /// <param name="text"></param>
        public static void WriteErrorLogs(string text)
        {
            try
            {
                string logPath = ConfigurationManager.AppSettings["logsPath"];
                DateTime dt = DateTime.Now;
                logPath = logPath + "/Error/" + dt.ToString("yyyy-MM") + "/";
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                string fullName = logPath + "ERROR-" + dt.ToString("yyyyMMdd") + ".log";
                StreamWriter sw = null;
                FileStream fs = null;
                if (!File.Exists(fullName))
                {
                    //sw = File.CreateText(fullName);
                    fs = new FileStream(fullName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                }
                else
                {
                    fs = new FileStream(fullName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

                }
                sw = new StreamWriter(fs, Encoding.Default);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine("************************************ERROR BEGIN***************************************");
                sw.WriteLine("TIME:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sw.WriteLine(text);
                sw.WriteLine("************************************ERROR END***************************************");
                sw.Close();
                fs.Close();
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="text"></param>
        public static void WriteLogs(string filePath, string text)
        {
            try
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                DateTime dt = DateTime.Now;
                string fileName = dt.ToString("yyyyMMdd HH");
                string fullName = filePath + "/" + fileName + ".log";
                StreamWriter sw = null;
                FileStream fs = null;
                if (!File.Exists(fullName))
                {
                    //sw = File.CreateText(fullName);
                    fs = new FileStream(fullName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                }
                else
                {
                    fs = new FileStream(fullName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                }
                sw = new StreamWriter(fs, Encoding.Default);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(DateTime.Now.ToString() + "     " + text);
                sw.Close();
                fs.Close();
            }
            catch { }
        }


        /// <summary>
        /// 记录下行通知URL
        /// </summary>
        /// <param name="page"></param>
        /// <param name="logid"></param>
        /// <param name="chanelName"></param>
        public static void WirteURLLogs(System.Web.UI.Page page, string logid, string chanelName)
        {
            string method = "GET";
            List<NameValue> lst = new List<NameValue>();
            if (page.Request.Form.Count > 0)
            {
                method = "POST";
                for (int i = 0; i < page.Request.Form.Count; i++)
                {
                    lst.Add(new NameValue(page.Request.Form.GetKey(i), page.Request.Form[i]));
                }
            }
            if (lst.Count <= 0)
            {
                for (int j = 0; j < page.Request.QueryString.Count; j++)
                    lst.Add(new NameValue(page.Request.QueryString.GetKey(j), page.Request.QueryString[j]));
            }
            NameValueCollection nv = new NameValueCollection();
            for (int i = 0; i < lst.Count; i++)
                nv.Add(lst[i].Name, lst[i].Value);
            string text = chanelName + "|method=" + method + "|logid=" + logid + "|url:" + page.Request.Url + "?" + GetSignText(nv);
            WriteLogs(text);
        }




        /// <summary>
        /// 获取参与签名的字符串
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GetSignText(NameValueCollection nv)
        {
            string s = "";
            for (int i = 0; i < nv.Count; i++)
            {
                string name = nv.GetKey(i);
                string value = nv[i];

                if (!string.IsNullOrEmpty(value))
                {
                    if (s == "")
                    {
                        s += name + "=" + value;
                    }
                    else
                    {
                        s += "&" + name + "=" + value;
                    }
                }

            }


            return s;
        }

    }
}
