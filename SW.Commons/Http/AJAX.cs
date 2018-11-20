using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Threading;

namespace SW.Commons.Http
{
    public class AJAX
    {
        #region AJAX主要方法
        /// <summary>
        /// [完全]访问指定URL,返回内容(HttpWebRequest)
        /// </summary>
        /// <param name="URL">要访问的网址</param>
        /// <param name="param">post参数</param>
        /// <param name="enCode">网页编码,可填Auto(自动)</param>
        /// <param name="isGet">get访问</param>
        /// <param name="cookieStr">cookie字符串</param>
        /// <param name="Referer">来源</param>
        /// <param name="userAgent">客户端标识</param>
        /// <param name="timeout">访问超时时间</param>
        /// <param name="proxyip">代理的IP地址</param>
        /// <param name="proxyport">代理端口</param>
        /// <returns></returns>
        public static string WebRequestFun(string URL, string param, string enCode, bool isGet, string cookieStr, string Referer, string userAgent, int timeout = 20000, string proxyip = "", string proxyport = "")
        {
            Encoding Enc = null;
            return WebRequestFun(URL, param, enCode, isGet, cookieStr, Referer, userAgent, out Enc, timeout, proxyip, proxyport);
        }

        ///[完全]访问指定URL,返回内容(HttpWebRequest)
        public static string WebRequestFun(string URL, string param, string enCode, bool isGet, string cookieStr, string Referer, string userAgent, string proxyip, string proxyport)
        {
            Encoding Enc = null;
            return WebRequestFun(URL, param, enCode, isGet, cookieStr, Referer, userAgent, out Enc, 20000, proxyip, proxyport);
        }

        /// <summary>
        /// [完全]访问指定URL,返回内容(HttpWebRequest)
        /// </summary>
        /// <param name="URL">要访问的网址</param>
        /// <param name="param">post参数</param>
        /// <param name="enCode">网页编码,可填Auto(自动)</param>
        /// <param name="isGet">get访问</param>
        /// <param name="cookieStr">cookie字符串</param>
        /// <param name="Referer">来源</param>
        /// <param name="userAgent">客户端标识</param>
        /// <param name="Enc">使用的网页编码</param>
        /// <param name="timeout">访问超时时间</param>
        /// <param name="proxyip">代理的IP地址</param>
        /// <param name="proxyport">代理端口</param>
        /// <returns></returns>
        public static string WebRequestFun(string URL, string param, string enCode, bool isGet, string cookieStr, string Referer, string userAgent, out Encoding Enc, int timeout = 20000, string proxyip = "", string proxyport = "")
        {
            //http://www.cnblogs.com/kissdodog/archive/2013/04/06/3002779.html
            string RequestStr = "";
            HttpWebRequest webrequest = null;
            //HttpWebResponse webreponse = null;
            enCode = enCode.ToLower();
            string enCodiingStr = "utf-8";
            if (!string.IsNullOrEmpty(enCode) && enCode != "auto")
                enCodiingStr = enCode;
            Enc = Encoding.GetEncoding(enCodiingStr);
            if (string.IsNullOrEmpty(URL))
                return "Error";
            try
            {
                string[] webTypes = new string[] { ".cn", ".com", ".net", ".gov", ".hk", ".org", ".edu" };
                string host = "";
                foreach (string it in webTypes)
                {
                    if (URL.Contains(it))
                        host = URL.Substring(0, URL.IndexOf(it) + it.Length);
                }

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                webrequest = (HttpWebRequest)HttpWebRequest.Create(URL);
                webrequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                webrequest.Headers.Add("Accept-Language:zh-CN");
                webrequest.Headers.Add("Cache-Control:no-cache");
                SetHeaderValue(webrequest.Headers, "Connection", "keep-alive");
                //webrequest.Connection = "keep-alive";
                //webrequest.KeepAlive = true;
                //webrequest.Headers.Add("Accept-Encoding:gzip,deflate");
                if (!string.IsNullOrEmpty(cookieStr))
                    webrequest.Headers.Add("Cookie:" + cookieStr);
                if (!string.IsNullOrEmpty(host))
                {
                    //webrequest.Host = host.Replace("http://", "");
                    SetHeaderValue(webrequest.Headers, "Host", host.Replace("http://", ""));
                }
                webrequest.Headers.Add("Pragma:no-cache");
                //webrequest.Referer = "http://www.bidchance.com/search.do?searchtype=zb&queryword=%C3%E6";
                if (!string.IsNullOrEmpty(host))
                {
                    webrequest.Referer = host;
                }
                if (!string.IsNullOrEmpty(Referer))
                    webrequest.Referer = Referer;
                if (!string.IsNullOrEmpty(userAgent))
                    webrequest.UserAgent = userAgent;
                else
                    webrequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2146.0 Safari/537.36";
                //webrequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Maxthon/4.0 Chrome/30.0.1599.101 Safari/537.36";
                webrequest.Timeout = timeout;// 20 * 1000;
                webrequest.AllowAutoRedirect = true;
                webrequest.Method = "POST";
                webrequest.ReadWriteTimeout = timeout;
                if (!string.IsNullOrEmpty(proxyip) && !string.IsNullOrEmpty(proxyport) && RegExp.IsNumeric(proxyport))
                {
                    //WebProxy proxy = new WebProxy();
                    //proxy.Address = new Uri("http://58.22.62.163:888/");
                    //proxy.Credentials = new NetworkCredential("juese", "1029");
                    WebProxy proxy = new WebProxy(proxyip, int.Parse(proxyport));
                    webrequest.Proxy = proxy;
                }
                else
                    webrequest.Proxy = null;

                //webrequest.ServicePoint.ConnectionLimit = 300;
                //CookieContainer cc = new CookieContainer();
                //webrequest.CookieContainer = cc;
                //webrequest.Accept = "image/gif, image/jpeg, image/pjpeg, image/pjpeg, application/x-shockwave-flash, application/xaml+xml, application/x-ms-xbap, application/x-ms-application, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";

                if (isGet)
                    webrequest.Method = "Get";
                else if (!string.IsNullOrEmpty(param))
                {
                    //byte[] postBytes = Encoding.ASCII.GetBytes(param);
                    byte[] postBytes = Enc.GetBytes(param);
                    webrequest.Method = "POST";
                    webrequest.ContentType = "application/x-www-form-urlencoded;charset=" + enCodiingStr;
                    webrequest.ContentLength = postBytes.Length;
                    using (Stream reqStream = webrequest.GetRequestStream())
                    {
                        reqStream.Write(postBytes, 0, postBytes.Length);
                    }
                }
                else
                    webrequest.ContentLength = 0;
                using (WebResponse wr = webrequest.GetResponse())
                {
                    if (enCode == "auto")
                    {
                        Encoding enc = null;
                        bool isNewEncode = false;
                        RequestStr = AJAX.ReadHtmlAutoEncode(wr, out enc, out isNewEncode);
                        if (!string.IsNullOrEmpty(RequestStr))
                            Enc = enc;
                    }
                    else
                    {
                        Stream resStream = wr.GetResponseStream();
                        StreamReader Sr = new StreamReader(resStream, Enc);
                        RequestStr = Sr.ReadToEnd();
                        resStream.Close();
                        Sr.Close();
                    }
                }
                webrequest.Abort();
                return RequestStr;
            }
            catch (WebException we)
            {
                Console.WriteLine("访问网址时网络异常：" + we.Message);
                return "Error";
            }
            catch (Exception e)
            {
                Console.WriteLine("访问网址时出错：" + e.ToString());
                return "Error";
            }
            finally
            {
                if (webrequest != null)
                {
                    webrequest.Abort();
                    webrequest = null;
                }
            }
        }

        /// <summary>
        /// 使用线程访问网址 HttpWebRequest （主要目的为强制结束时间）
        /// </summary>
        public static string WebRequestThread(string URL, string param, string enCode, bool isGet, string cookieStr, string Referer, string userAgent, out Encoding Enc, int timeout = 20000, string proxyip = "", string proxyport = "")
        {
            //string html = AJAX.WebRequestThread("http://www.163.com", "", "auto", true, "", "", "");
            Enc = null;
            AJAXThreadParamModel model = new AJAXThreadParamModel();
            model.url = URL;
            model.param = param;
            model.enCode = enCode;
            model.isGet = isGet;
            model.cookieStr = cookieStr;
            model.referer = Referer;
            model.userAgent = userAgent;
            model.timeout = timeout;
            model.proxyip = proxyip;
            model.proxyport = proxyport;

            AJAXThread AT = new AJAXThread(model);
            if (AT.Start())
            {
                model = AT.Model;
                Enc = model.enc;
                return model.html;
            }
            return "Error";
        }

        /// <summary>
        /// 使用线程访问网址 HttpWebRequest （主要目的为强制结束时间）
        /// </summary>
        public static string WebRequestThread(string URL, string param, string enCode, bool isGet, string cookieStr, string Referer, string userAgent, int timeout = 20000, string proxyip = "", string proxyport = "")
        {
            Encoding Enc = null;
            return WebRequestThread(URL, param, enCode, isGet, cookieStr, Referer, userAgent, out Enc, timeout, proxyip, proxyport);
        }

        /// <summary>
        /// 上传指定文件到URL
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="enCodiingStr"></param>
        /// <param name="param"></param>
        /// <param name="fileName">文件全路径</param>
        /// <returns></returns>
        public static string WebRequestSendFile(string URL, string enCodiingStr, Dictionary<string, string> dict, string fileName)
        {
            //http://blog.csdn.net/hailang9027/article/details/52383428
            string RequestStr = "";
            HttpWebRequest webrequest = null;
            //HttpWebResponse webreponse = null;

            if (string.IsNullOrEmpty(URL))
                return "";

            try
            {
                if (string.IsNullOrEmpty(enCodiingStr))
                    enCodiingStr = "gb2312";
                Encoding Enc = Encoding.GetEncoding(enCodiingStr);
                bool hasFile = false;
                if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
                    hasFile = true;

                MemoryStream memStream = new MemoryStream();
                // 边界符
                string boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
                // 边界符
                byte[] beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
                // 最后的结束符
                byte[] endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

                webrequest = (HttpWebRequest)HttpWebRequest.Create(URL);
                webrequest.ContentType = "multipart/form-data; boundary=" + boundary;
                webrequest.Method = "POST";

                // 写入文件                
                if (hasFile)
                {
                    FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    memStream.Write(beginBoundary, 0, beginBoundary.Length);
                    string filePartHeader = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                    var header = string.Format(filePartHeader, "upfile", Path.GetFileName(fileName));
                    var headerbytes = Enc.GetBytes(header);
                    memStream.Write(headerbytes, 0, headerbytes.Length);
                    var buffer = new byte[1024];
                    int bytesRead;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        memStream.Write(buffer, 0, bytesRead);
                    }
                }

                // 写入字符串
                if (dict != null && dict.Count > 0)
                {
                    if (!hasFile)
                    {
                        string data = string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}", "postType", "heli1.0");
                        byte[] postBytes = Enc.GetBytes(data);
                        memStream.Write(postBytes, 0, postBytes.Length);
                    }

                    string stringKeyHeader = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                    foreach (KeyValuePair<string, string> kvp in dict)
                    {
                        string data = string.Format(stringKeyHeader, kvp.Key, kvp.Value);
                        byte[] postBytes = Enc.GetBytes(data);
                        memStream.Write(postBytes, 0, postBytes.Length);
                    }
                    //foreach (byte[] formitembytes in from string key in stringDict.Keys select string.Format(stringKeyHeader, key, stringDict[key]) into formitem select Encoding.UTF8.GetBytes(formitem))
                    //{
                    //    memStream.Write(formitembytes, 0, formitembytes.Length);
                    //}
                }


                // 写入最后的结束边界符
                memStream.Write(endBoundary, 0, endBoundary.Length);
                webrequest.ContentLength = memStream.Length;
                using (Stream reqStream = webrequest.GetRequestStream())
                {
                    memStream.Position = 0;
                    byte[] tempBuffer = new byte[memStream.Length];
                    memStream.Read(tempBuffer, 0, tempBuffer.Length);
                    memStream.Close();

                    reqStream.Write(tempBuffer, 0, tempBuffer.Length);
                }
                using (WebResponse wr = webrequest.GetResponse())
                {
                    Stream resStream = wr.GetResponseStream();
                    StreamReader Sr = new StreamReader(resStream, Enc);
                    RequestStr = Sr.ReadToEnd();
                    resStream.Close();
                    Sr.Close();
                }
                webrequest.Abort();
            }
            catch (Exception e)
            {
                RequestStr = "Error";
                Console.WriteLine(e.ToString());
                if (webrequest != null) webrequest.Abort();
            }
            return RequestStr;
        }


        /// <summary>
        /// 保存网络文件到本地
        /// </summary>
        /// <param name="webImgUrl">含http:的网络文件图片地址</param>
        /// <param name="savePath">保存路径(绝对或相对)末尾加反斜杠</param>
        /// <param name="newFileName">新文件名,为空则为原文件名(不含扩展名)</param>
        /// <param name="minByte">最小大小byte(1M=1048576,1024*1024)</param>
        /// <param name="maxByte">最大大小byte</param>
        /// <param name="nFilePath">out 保存后的文件全路径</param>
        /// <returns>是否成功</returns>
        public static bool SaveFileFromWeb(string webFileUrl, string savePath, string newFileName, long minByte, long? maxByte, out string nFilePath, string cookieStr = "", string Referer = "", string userAgent = "")
        {
            //string headImage = headimgurl;
            //if (AJAX.SaveFileFromWeb(headimgurl, string.Format("/File/UserImg/{0}/", mtt.id), "headImage", 1024, null, out headImage))
            //    userPersonal.headImage = headImage;

            nFilePath = "";
            bool rebool = false;
            if (string.IsNullOrEmpty(savePath) || string.IsNullOrEmpty(webFileUrl))
                return rebool;
            string imgName = "";
            string defaultType = ".jpg";
            string[] imgTypes = new string[] { ".jpg", ".gif", ".png", ".jpeg", ".bmp", ".rar", ".zip", ".pdf", ".xls", ".doc", ".xlsx", ".docx" };
            string imgType = "";
            string oldSavePath = "";
            try
            {
                if (webFileUrl.Contains("%"))
                    webFileUrl = System.Web.HttpUtility.UrlDecode(webFileUrl);
                imgName = Path.GetFileNameWithoutExtension(webFileUrl);
                if (string.IsNullOrEmpty(newFileName))
                {
                    newFileName = imgName;
                }
                imgType = Path.GetExtension(webFileUrl);
                //imgUrl.ToString().Substring(imgUrl.ToString().LastIndexOf("."));
                foreach (string it in imgTypes)
                {
                    if (imgType.ToLower().Equals(it))
                        break;
                    if (it.Equals(".bmp"))
                        imgType = defaultType;
                }
                if (!savePath.EndsWith("/"))
                    savePath += "/";
                oldSavePath = savePath;
                if (savePath.IndexOf(":") == -1)
                {
                    try
                    {
                        savePath = Handler.MapPath(savePath);
                    }
                    catch { }
                }
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);
            }
            catch (Exception ex)
            {
                nFilePath = ex.ToString();
                return false;
            }

            HttpWebRequest request = null;
            WebResponse response = null;
            Stream stream = null;
            try
            {
                string[] webTypes = new string[] { ".cn", ".com", ".net", ".gov", ".hk", ".org", ".edu" };
                string host = "";
                foreach (string it in webTypes)
                {
                    if (webFileUrl.Contains(it))
                        host = webFileUrl.Substring(0, webFileUrl.IndexOf(it) + it.Length);
                }
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                request = (HttpWebRequest)WebRequest.Create(webFileUrl);
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers.Add("Accept-Language:zh-CN");
                SetHeaderValue(request.Headers, "Connection", "keep-alive");
                if (!string.IsNullOrEmpty(cookieStr))
                    request.Headers.Add("Cookie:" + cookieStr);
                if (!string.IsNullOrEmpty(host))
                {
                    SetHeaderValue(request.Headers, "Host", host.Replace("http://", ""));
                }
                if (!string.IsNullOrEmpty(host))
                {
                    request.Referer = host;
                }
                if (!string.IsNullOrEmpty(Referer))
                    request.Referer = Referer;
                if (!string.IsNullOrEmpty(userAgent))
                    request.UserAgent = userAgent;
                else
                    request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Maxthon/4.0 Chrome/30.0.1599.101 Safari/537.36";
                request.Timeout = 10 * 1000;
                request.ReadWriteTimeout = 60 * 1000;
                request.AllowAutoRedirect = true;
                request.Headers.Add("Pragma:no-cache");
                request.Headers.Add("Cache-Control:no-cache");
                request.Proxy = null;
                System.Net.ServicePointManager.DefaultConnectionLimit = 200;
                //request.Method = "POST";

                response = request.GetResponse();
                stream = response.GetResponseStream();

                long imgLong = response.ContentLength;
                if (imgLong != -1)
                {
                    if (imgLong < minByte)
                        return false;
                    if (maxByte.HasValue && imgLong > maxByte.Value)
                        return false;
                }
                else
                {
                    if (stream != null)
                    {
                        //Content-Disposition:filename=10653733_.doc.gz
                        //Content-Type:application/unkown;charset=gbk
                        //Date:Wed, 14 Dec 2016 01:54:13 GMT
                        //Server:Apache-Coyote/1.1
                        //Set-Cookie:JSESSIONID=B5C534DB941625CBE7B53AB1AA556945; Path=/ae_ids/; HttpOnly
                        //Transfer-Encoding:chunked

                        string extend = "gz";
                        if (response.Headers.AllKeys.Contains("Content-Disposition"))
                        {
                            //Content-Disposition:attachment;filename="1479204691.doc"
                            //Content-Disposition:filename=10653733_.doc.gz
                            string disposition = response.Headers["Content-Disposition"].ToString();
                            extend = RegExp.GetValueFirst(disposition, @"filename=[""|']?[^\.]*?\.([^""']*)[""|']?");
                        }
                        try
                        {
                            nFilePath = savePath + newFileName + "." + extend;
                            oldSavePath = oldSavePath + newFileName + "." + extend;
                            //打开文件流
                            FileStream fso = new FileStream(nFilePath, FileMode.Create);
                            byte[] arrayByte = new byte[1024];
                            int n;
                            while ((n = stream.Read(arrayByte, 0, arrayByte.Length)) > 0)
                            {
                                fso.Write(arrayByte, 0, n);
                            }
                            fso.Close();
                            nFilePath = oldSavePath;
                            //    responseStream = new System.IO.Compression.GZipStream(responseStream, System.IO.Compression.CompressionMode.Decompress);
                            //    //System.IO.StreamReader Reader = new System.IO.StreamReader(responseStream, Encoding.Default);
                            //    //String result = Reader.ReadToEnd();
                        }
                        catch { }
                        finally
                        {
                            if (stream != null)
                                stream.Close();
                            if (response != null)
                                response.Close();
                            request = null;
                            stream = null;
                            response = null;
                        }
                        return true;
                    }
                }

                if (response.ContentType.ToLower().StartsWith("image/"))
                {
                    if (string.IsNullOrEmpty(newFileName))
                        newFileName = imgName;

                    nFilePath = savePath + newFileName + imgType;
                    oldSavePath = oldSavePath + newFileName + imgType;
                    FileStream fso = new FileStream(nFilePath, FileMode.Create);
                    long l = 0;
                    byte[] arrayByte = new byte[1024];
                    while (l < imgLong)
                    {
                        int i = stream.Read(arrayByte, 0, 1024);
                        fso.Write(arrayByte, 0, i);
                        l += i;
                    }
                    fso.Close();
                    rebool = true;
                    nFilePath = oldSavePath;
                }
                else
                {
                    //Console.Write(response.ContentType.ToLower() + ",");
                    string ctype = response.ContentType.ToLower();
                    if (ctype.Contains("octet-stream"))
                    {
                        if (response.Headers.AllKeys.Contains("Content-Disposition"))
                        {
                            //Content-Disposition:attachment;filename="1479204691.doc"
                            string disposition = response.Headers["Content-Disposition"].ToString();
                            ctype = RegExp.GetValueFirst(disposition, @"filename=[""|']?[^\.]*?\.([^""']*)[""|']?");
                        }
                    }
                    if (",jpg,png,gif,rar,zip,pdf,xls,doc,xlsx,docx,".Contains("," + ctype + ","))
                    {
                        nFilePath = savePath + newFileName + "." + ctype;
                        oldSavePath = oldSavePath + newFileName + "." + ctype;
                        FileStream fso = new FileStream(nFilePath, FileMode.Create);
                        long l = 0;
                        byte[] arrayByte = new byte[1024];
                        while (l < imgLong)
                        {
                            int i = stream.Read(arrayByte, 0, 1024);
                            fso.Write(arrayByte, 0, i);
                            l += i;
                        }
                        fso.Close();
                        rebool = true;
                        nFilePath = oldSavePath;
                    }
                    else
                    {
                        //LogHandler.WriteLog("downloadError_", "文件类型错误：{0}", ctype);
                        rebool = false;
                    }
                }
                stream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                //LogHandler.WriteLog("downloadError_", "文件下载异常：{0}", ex.ToString());
                rebool = false;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (response != null)
                    response.Close();
                request = null;
                stream = null;
                response = null;
            }
            return rebool;
        }


        /// <summary>
        /// 设置不能直接设置的header参数 webrequest.Headers.Add
        /// </summary>
        /// <param name="header"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }


        /// <summary>
        /// 自动使用正确的编码解析出HTML 
        /// </summary>
        /// <param name="response">AJAX返回的请求对象</param>
        /// <returns></returns>
        public static string ReadHtmlAutoEncode(WebResponse response)
        {
            Encoding enc = null;
            bool isNewEncode = false;
            return ReadHtmlAutoEncode(response, out enc, out isNewEncode);
        }

        /// <summary>
        /// 自动使用正确的编码解析出HTML 
        /// </summary>
        /// <param name="response">WebResponse对象</param>
        /// <param name="enc">output</param>
        /// <param name="isNewEncode">编码有更新</param>
        /// <param name="tryEnc">初始编码</param>
        /// <returns></returns>
        public static string ReadHtmlAutoEncode(WebResponse response, out Encoding enc, out bool isNewEncode, string tryEnc = "utf-8")
        {
            string enCodiingStr = "utf-8";
            if (!string.IsNullOrEmpty(tryEnc))
                enCodiingStr = tryEnc.ToLower();
            enc = Encoding.GetEncoding(enCodiingStr);
            isNewEncode = false;
            if (response == null)
                return "";

            Stream resStream = null;
            string responseStr = "";
            try
            {
                resStream = response.GetResponseStream();
                //StreamReader Sr = null;
                //Sr = new StreamReader(stream, enc);
                //responseStr = Sr.ReadToEnd();
                //Sr.Close();
                byte[] data;
                using (var memoryStream = new MemoryStream())
                {
                    byte[] buffer = new byte[0x100];
                    for (var i = resStream.Read(buffer, 0, buffer.Length); i > 0; i = resStream.Read(buffer, 0, buffer.Length))
                    {
                        memoryStream.Write(buffer, 0, i);
                    }
                    data = memoryStream.ToArray();
                }
                responseStr = enc.GetString(data);
                bool find = false;

                HttpWebResponse httpWebResponse = (HttpWebResponse)response;
                string charSet = httpWebResponse.CharacterSet.ToLower();
                if (!string.IsNullOrEmpty(charSet))
                {

                    if (charSet.ToLower() == "gbk")
                        charSet = "gb18030";
                    if (charSet == tryEnc)
                        find = true;
                    else if (charSet != "iso-8859-1")
                    {
                        isNewEncode = true;
                        enc = Encoding.GetEncoding(charSet);
                        responseStr = enc.GetString(data);
                        find = true;
                    }
                }
                if (!find)
                {
                    string encode = RegExp.GetValueFirst(responseStr, @".?charset=[""']?([-a-zA-Z_0-9]+)[""']?");
                    if (!string.IsNullOrEmpty(encode))
                    {
                        if (encode.ToLower() == "gbk")
                            encode = "gb18030";
                        if (enCodiingStr != encode)
                        {
                            isNewEncode = true;
                            enc = Encoding.GetEncoding(encode);
                            responseStr = enc.GetString(data);
                        }
                    }
                }

            }
            catch { }
            finally
            {
                if (resStream != null)
                    resStream.Close();
            }
            return responseStr;
        }

        /// <summary>
        /// 自动使用正确的编码解析流为字符串
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns></returns>
        public static string ReadHtmlAutoEncode(Stream stream)
        {
            Encoding enc = null;
            bool isNewEncode = false;
            return ReadHtmlAutoEncode(stream, out enc, out isNewEncode);
        }

        /// <summary>
        /// 自动使用正确的编码解析流为字符串
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="enc">使用的编码</param>
        /// <param name="isNewEncode">编码有更新</param>
        /// <param name="tryEnc">初始编码</param>
        /// <returns></returns>
        public static string ReadHtmlAutoEncode(Stream stream, out Encoding enc, out bool isNewEncode, string tryEnc = "utf-8")
        {
            string enCodiingStr = "utf-8";
            if (!string.IsNullOrEmpty(tryEnc))
                enCodiingStr = tryEnc.ToLower();
            enc = Encoding.GetEncoding(enCodiingStr);
            isNewEncode = false;
            if (stream == null)
                return "";

            string responseStr = "";
            try
            {
                //StreamReader Sr = null;
                //Sr = new StreamReader(stream, enc);
                //responseStr = Sr.ReadToEnd();
                //Sr.Close();
                byte[] data;
                using (var memoryStream = new MemoryStream())
                {
                    byte[] buffer = new byte[0x100];
                    for (var i = stream.Read(buffer, 0, buffer.Length); i > 0; i = stream.Read(buffer, 0, buffer.Length))
                    {
                        memoryStream.Write(buffer, 0, i);
                    }
                    data = memoryStream.ToArray();
                }
                responseStr = enc.GetString(data);

                string encode = RegExp.GetValueFirst(responseStr, @".?charset=[""']?([-a-zA-Z_0-9]+)[""']?");
                if (!string.IsNullOrEmpty(encode))
                {
                    if (encode.ToLower() == "gbk")
                        encode = "gb18030";
                    if (enCodiingStr != encode)
                    {
                        isNewEncode = true;
                        enc = Encoding.GetEncoding(encode);
                        responseStr = enc.GetString(data);
                    }
                }

            }
            catch { }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return responseStr;
        }

        #endregion

        #region 扩展方法

        /// <summary>
        /// [简单WebClient]请求网址并返回数据
        /// </summary>
        public static string WebRequestFun(string url, string param, string method = "POST")
        {
            string returnmsg = "";
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                returnmsg = wc.UploadString(url, method, param);//DownloadString
                //wc.DownloadString("");
            }
            return returnmsg;
        }

        /// <summary>
        /// [简单HttpWebRequest]访问指定URL,返回内容
        /// AJAX.WebRequestFun(string.Format("https://qq.com/a.aspx?secret={0}",appID,appSecret), "utf-8", true, "");
        /// JObject json = (JObject)JsonConvert.DeserializeObject(body);
        /// </summary>
        /// <param name="URL">URL</param>
        /// <param name="enCodiingStr">gb2312或UTF8</param>
        /// <param name="isGet">使用Get否则POST</param>
        /// <param name="param">POST时使用的参数</param>
        /// <returns></returns>
        public static string WebRequestFun(string url, string enCodiingStr, bool isGet, string param)
        {
            string RequestStr = "";
            HttpWebRequest webrequest = null;
            //HttpWebResponse webreponse = null;
            try
            {
                if (string.IsNullOrEmpty(enCodiingStr))
                    enCodiingStr = "gb2312";
                Encoding Enc = Encoding.GetEncoding(enCodiingStr);
                webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
                //req.UserAgent = "Opera/9.25 (Windows NT 6.0; U; en)";
                //req.KeepAlive = true;
                //req.Timeout = 6000000;
                if (isGet)
                    webrequest.Method = "GET";
                else
                {
                    webrequest.Method = "POST";

                    //string PageUrl = string.Format("{0}/sms.aspx?", URLHead);
                    //string Param = string.Format("action=send&userid={0}&account={1}&password={2}&mobile={3}&content={4}", smsName, smsID, smsPwd, HttpUtility.UrlEncode(Mobile, Enc), HttpUtility.UrlEncode(Content, Enc));

                    if (!string.IsNullOrEmpty(param))
                    {
                        //byte[] postBytes = Encoding.ASCII.GetBytes(param);
                        byte[] postBytes = Enc.GetBytes(param);
                        webrequest.ContentType = "application/x-www-form-urlencoded;charset=" + enCodiingStr;
                        webrequest.ContentLength = postBytes.Length;
                        using (Stream reqStream = webrequest.GetRequestStream())
                        {
                            reqStream.Write(postBytes, 0, postBytes.Length);
                        }
                    }

                }
                using (WebResponse wr = webrequest.GetResponse())
                {
                    Stream resStream = wr.GetResponseStream();
                    StreamReader Sr = new StreamReader(resStream, Enc);
                    RequestStr = Sr.ReadToEnd();
                    resStream.Close();
                    Sr.Close();
                }
                webrequest.Abort();
                //webreponse.Close();
            }
            catch (Exception e)
            {
                RequestStr = "Error";
                if (webrequest != null) webrequest.Abort();
            }
            return RequestStr;
        }

        #endregion

    }

    /// <summary>
    /// 使用线程访问网址 HttpWebRequest 实体
    /// </summary>
    public class AJAXThreadParamModel
    {
        public string url { get; set; }
        public string param { get; set; }
        public string enCode { get; set; }
        public bool isGet { get; set; }
        public string cookieStr { get; set; }
        public string referer { get; set; }
        public string userAgent { get; set; }
        public int timeout { get; set; }
        public string proxyip { get; set; }
        public string proxyport { get; set; }
        public string html { get; set; }
        public Encoding enc { get; set; }
    }

    public class AJAXThread
    {
        public AJAXThread(AJAXThreadParamModel model)
        {
            Model = model;
            timeOut = Model.timeout;
        }

        ~AJAXThread()
        {
            if (WorkThread != null)
            {
                try
                {
                    WorkThread.Abort();
                }
                catch { }
                WorkThread = null;
            }
        }

        public AJAXThreadParamModel Model;
        private Thread WorkThread = null;
        private bool getSuc = false;
        private int timeOut = 20000;
        private Encoding outEnc = null;

        public bool Start()
        {
            try
            {
                //WorkThread = new Thread(new ParameterizedThreadStart(GetClientDataByThread));
                WorkThread = new Thread(new ThreadStart(AJAX_Thread));
                WorkThread.IsBackground = true;
                WorkThread.Start();

                //监督子线程运行时间
                while (WorkThread.IsAlive && timeOut > 0)
                {
                    Thread.Sleep(100);
                    timeOut -= 100;
                }
                // 超时处理
                if (WorkThread.IsAlive)
                {
                    try
                    {
                        WorkThread.Abort();
                        WorkThread = null;
                    }
                    catch { }
                    if (getSuc) return true;
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void AJAX_Thread()
        {
            Model.html = AJAX.WebRequestFun(Model.url, Model.param, Model.enCode, Model.isGet, Model.cookieStr, Model.referer, Model.userAgent, out outEnc, Model.timeout, Model.proxyip, Model.proxyport);
            Model.enc = outEnc;
            try
            {
                Thread.CurrentThread.Abort();
                WorkThread = null;
            }
            catch { }

        }
    }
}
