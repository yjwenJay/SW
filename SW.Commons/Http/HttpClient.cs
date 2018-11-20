using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace SW.Commons.Http
{
    /// <summary>
    /// Http网络访问实用类
    /// </summary>
    public class HttpClient
    {
        /// <summary>
        /// 提交Post请求，默认编码为UTF-8
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string Post(string url, string postData, Encoding encoding)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest; ;
                request.Method = "POST";
                byte[] byteData = encoding.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteData.Length;
                Stream newStream = request.GetRequestStream();
                newStream.Write(byteData, 0, byteData.Length);
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    result = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();
                }
                stream.Close();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        /// <summary>
        /// 提交Post请求，默认编码为UTF-8
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string PostSSL(string url, string certpath, string postData, Encoding encoding, string password)
        {
            string result = string.Empty;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                Log4net.Info(ConfigHelper.AppSettings("WeixinApiclientCert"));
                X509Certificate obj509 = new X509Certificate(certpath, password);//写入正确的证书路径(第四步导出的Cer文件)
                request.ClientCertificates.Add(obj509);
                request.Method = "POST";
                byte[] byteData = encoding.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteData.Length;
                Stream newStream = request.GetRequestStream();
                newStream.Write(byteData, 0, byteData.Length);
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    result = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();
                }
                stream.Close();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                Log4net.Error(ex, "Http请求异常:请求地址:" + url);
            }

            return result;
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //总是返回TRUE 
            return true;
        }

        public static string Post(string url, string postData, string contentType, Encoding encoding)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest request = null;
                //如果是发送HTTPS请求  
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }
                request.Method = "POST";
                byte[] byteData = encoding.GetBytes(postData);
                request.ContentType = contentType;
                request.ContentLength = byteData.Length;
                Stream newStream = request.GetRequestStream();
                newStream.Write(byteData, 0, byteData.Length);
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    result = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();
                }
                stream.Close();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
            }

            return result;
        }
        /// <summary>
        /// 提交Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="referer"></param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static string Post(string url, NameValueCollection postData, string referer, string cookies)
        {
            string strPost = "";
            if (postData != null)
            {
                for (int i = 0; i < postData.Count; i++)
                {
                    if (i == 0)
                    {
                        strPost += postData.GetKey(i) + "=" + postData[i];
                    }
                    else
                    {
                        strPost += "&" + postData.GetKey(i) + "=" + postData[i];
                    }
                }
            }
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; Data Center; SE 2.X; Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1) ; EmbeddedWB 14.52 from: http://www.bsalsa.com/ EmbeddedWB 14.52; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; SE 2.X)";
            httpWebRequest.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/xaml+xml, application/vnd.ms-xpsdocument, application/x-ms-xbap, application/x-ms-application, */*";
            httpWebRequest.Referer = referer;
            httpWebRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            httpWebRequest.Headers.Add("Cookie", cookies);
            if (!string.IsNullOrEmpty(strPost))
            {
                Stream tempStream = httpWebRequest.GetRequestStream();
                StreamWriter streamWriter = new StreamWriter(tempStream, Encoding.Default);
                streamWriter.Write(strPost); //将POST的数据写入request
                streamWriter.Close();
            }
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream(); //得到回写的字节流
            StreamReader readStream = new StreamReader(responseStream, Encoding.Default);
            string result = readStream.ReadToEnd();
            readStream.Close();
            responseStream.Close();
            httpWebResponse.Close();
            httpWebRequest.Abort();
            return result;
        }

        /// <summary>
        /// 提交Get请求，默认编码为UTF-8
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string Get(string url, Encoding encoding)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    result = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();
                }
                stream.Close();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public static string PostXml(string url, string postData, string referer, Encoding encoding, string cookies)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "text/xml";
            httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; Data Center; SE 2.X; Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1) ; EmbeddedWB 14.52 from: http://www.bsalsa.com/ EmbeddedWB 14.52; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; SE 2.X)";
            httpWebRequest.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/xaml+xml, application/vnd.ms-xpsdocument, application/x-ms-xbap, application/x-ms-application, */*";
            httpWebRequest.Referer = referer;
            httpWebRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            httpWebRequest.Headers.Add("Cookie", cookies);
            if (!string.IsNullOrEmpty(postData))
            {
                Stream tempStream = httpWebRequest.GetRequestStream();
                StreamWriter streamWriter = new StreamWriter(tempStream, Encoding.GetEncoding("GB2312"));
                streamWriter.Write(postData); //将POST的数据写入request
                streamWriter.Close();
            }
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream(); //得到回写的字节流
            StreamReader readStream = new StreamReader(responseStream, encoding);
            string result = readStream.ReadToEnd();

            readStream.Close();
            responseStream.Close();
            httpWebResponse.Close();
            httpWebRequest.Abort();
            return result;
        }

    }
}
