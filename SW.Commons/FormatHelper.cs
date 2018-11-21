using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SW.Http;

namespace SW
{
    public class FormatHelper
    {
        #region  格式化金额输出
        /// <summary>
        /// 输出数字(两位小数)
        /// BaseCommon.Utility.Format.FormatDecimal()
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="outY">输出Y符号</param>
        /// <returns></returns>
        public static string FormatDecimal(object obj, bool outY = false)
        {
            /// <%# BaseCommon.Utility.Format.FormatDecimal(Eval("payPrice", "{0}"))%>
            if (obj != null)
            {
                return FormatDecimal(obj.ToString(), outY);
            }
            return "";
        }

        /// <summary>
        /// 输出数字(两位小数)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatDecimal(string obj)
        {
            return FormatDecimal(obj, false);
        }
        /// <summary>
        /// 输出数字(两位小数)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="outY"></param>
        /// <returns></returns>
        public static string FormatDecimal(string decimalStr, bool outY = false)
        {
            if (!string.IsNullOrEmpty(decimalStr))
            {
                double d = 0;
                if (double.TryParse(decimalStr, out d))
                {
                    if (outY)
                        return d.ToString("C");
                    else
                        return d.ToString("F2");
                }
                return decimalStr;
            }
            return "";
        }

        /// <summary>
        /// 合计并输出
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <param name="outY">输出Y符号</param>
        /// <returns></returns>
        public static string FormatDecimal(object obj1, object obj2, bool outY = false)
        {
            double d = 0, d2 = 0;
            string s1 = "", s2 = "";
            bool e1 = false, e2 = false;
            if (obj1 != null)
            {
                s1 = obj1.ToString();
                if (!string.IsNullOrEmpty(s1))
                {
                    e1 = double.TryParse(s1, out d);
                }
            }
            if (obj2 != null)
            {
                s2 = obj2.ToString();
                if (!string.IsNullOrEmpty(s2))
                {
                    e2 = double.TryParse(s2, out d2);
                }
            }
            double sum = d + d2;
            if (e1 && e2)
            {
                if (outY)
                    return sum.ToString("C");
                else
                    return sum.ToString("F2");
            }
            else
                return s1 + s2;
        }


        /// <summary>
        /// 合计并输出
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatDecimal(bool outY, params string[] args)
        {
            string restr = "0";
            if (args.Length > 0)
            {
                double all = 0;
                foreach (string s in args)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        double d = 0;
                        if (double.TryParse(s, out d))
                        {
                            all += d;
                        }
                    }
                }
                if (outY)
                    return all.ToString("C");
                else
                    return all.ToString("0.00");
            }

            return restr;
        }

        /// <summary>
        /// 合计并输出
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatDecimal(params string[] args)
        {
            string restr = "0";
            if (args.Length > 0)
            {
                double all = 0;
                foreach (string s in args)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        double d = 0;
                        if (double.TryParse(s, out d))
                        {
                            all += d;
                        }
                    }
                }
                return all.ToString("0.00");
            }

            return restr;
        }

        /// <summary>
        /// 格式化输出数据
        /// BaseCommon.Utility.Format.FormatDecimal()
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="outY">输出Y符号</param>
        /// <returns></returns>
        public static string FormatDecimal(decimal? val, bool outY = false)
        {
            if (val.HasValue)
            {
                if (outY)
                    return val.Value.ToString("C");
                else
                    return val.Value.ToString("0.00");
            }
            return "";
        }

        #endregion

        #region 手机号部分显示
        /// <summary>
        /// 将手机号码等非完整显示
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public static string FormatTel(string tel)
        {
            if (string.IsNullOrEmpty(tel))
                return "";
            int l = tel.Length;
            if (l > 0)
            {
                string t = tel;
                if (l >= 11)
                {
                    t = tel.Substring(0, 3);
                    t += "****";
                    t += tel.Substring(7);
                }
                else if (l >= 8)
                {
                    t = tel.Substring(0, 3);
                    t += "****";
                    t += tel.Substring(4);
                }
                else
                {
                    t = tel.Substring(0, 1);
                    t += "****";
                    t += tel.Substring(l - 1);
                }
                return t;
            }

            return "";
        }
        #endregion

        #region 格式化字符串为日期输出

        /// <summary>
        /// 格式化字符串为日期输出
        /// </summary>
        /// <param name="dateStr">可能为日期的字符串</param>
        /// <param name="preToDate">需转换为时间</param>
        /// <param name="dateFormat">转换为时间时的输出格式</param>
        /// <returns></returns>
        public static string FormatDate(string dateStr, bool preToDate = false, string dateFormat = "yyyy-MM-dd")
        {
            if (string.IsNullOrEmpty(dateStr))
                return "";
            try
            {
                if (preToDate)
                {
                    DateTime dt;
                    if (DateTime.TryParse(dateStr, out dt))
                        return dt.ToString(dateFormat);
                }
                else
                {
                    int spk = dateStr.IndexOf(' ');
                    if (spk >= 0)
                        return dateStr.Substring(0, spk);
                }
            }
            catch { }
            return dateStr;
        }

        #endregion

        #region 显示图片
        /// <summary>
        /// 将逗号分隔的图片地址,显示到页面
        /// </summary>
        /// <param name="srcs">图片地址串</param>
        /// <param name="showThumb">显示为缩略图</param>
        /// <param name="whType">高宽类型 0只高,1只宽,2不限制,3高宽,4 100%</param>
        /// <param name="imgHeight">高度</param>
        /// <param name="imgWidth">宽度</param>
        /// <param name="split">多图片分隔字符串</param>
        /// <returns></returns>
        public static string FormatImages(string srcs, bool showThumb, int whType = 0, string imgHeight = "100", string imgWidth = "100", string split = "")
        {
            return FormatImages(srcs, whType, imgHeight, imgWidth, split, showThumb);
        }
        /// <summary>
        /// 将逗号分隔的图片地址,显示到页面
        /// </summary>
        /// <param name="srcs">图片地址串</param>
        /// <param name="whType">高宽类型 0只高,1只宽,2不限制,3高宽,4 100%</param>
        /// <param name="imgHeight">高度</param>
        /// <param name="imgWidth">宽度</param>
        /// <param name="split">多图片分隔字符串</param>
        /// <param name="showThumb">显示为缩略图</param>
        /// <returns></returns>
        public static string FormatImages(string srcs, int whType = 0, string imgHeight = "100", string imgWidth = "100", string split = "", bool showThumb = true)
        {
            if (string.IsNullOrEmpty(srcs))
                return "";
            StringBuilder sb = new StringBuilder();
            string[] arr = srcs.Split(',');
            string imgAttr = "";
            switch (whType)
            {
                case 0:
                    imgAttr = " height='{0}'";
                    break;
                case 1:
                    imgAttr = " width='{1}'";
                    break;
                case 3:
                    imgAttr = " height='{0}' width='{1}'";
                    break;
                case 4:
                    imgAttr = " height='100%' width='100%'";
                    break;
            }
            string attr = string.Format(imgAttr, imgHeight, imgWidth);
            foreach (string s in arr)
            {
                if (showThumb)
                    sb.AppendFormat("<a href=\"{0}\" target=\"_blank\" title=\"点击查看大图\"><img src='{1}' {2} /></a>{3}", s, ImgHelper.ImageThumbnaiGet(s), attr, split);
                else
                    sb.AppendFormat("<img src='{0}' {1} />{2}", s, attr, split);
            }
            return sb.ToString();
        }


        public static string FormatMedia(string srcs, string QRhostUrl)
        {
            return FormatMedia(srcs, true, "100", "100", " - ", QRhostUrl);
        }

        /// <summary>
        /// 将逗号分隔的自定义格式媒体地址,显示到页面
        /// </summary>
        /// <param name="srcs"></param>
        /// <param name="isClickPlayer"></param>
        /// <param name="imgHeight"></param>
        /// <param name="imgWidth"></param>
        /// <param name="split"></param>
        /// <param name="QRhostUrl"></param>
        /// <returns></returns>
        public static string FormatMedia(string srcs, bool isClickPlayer = true, string imgHeight = "100", string imgWidth = "100", string split = "&nbsp;", string QRhostUrl = "")
        {
            //video格式 /File/telUpMedia/1726/20180109103720_969.mp4|/File/telUpMedia/1726/20180109103720_978.jpg
            //sound格式 /File/telUpMedia/1726/20180109103640_663.amr|3.4000006,/File/telUpMedia/1726/20180109103651_538.amr|2.8000004

            if (string.IsNullOrEmpty(srcs))
                return "";
            StringBuilder sb = new StringBuilder();
            string[] arr = srcs.Split(',');
            foreach (string src in arr)
            {
                if (string.IsNullOrEmpty(src))
                    continue;
                string msrc = "";
                string ext_src = "";
                if (src.Contains("|"))
                {
                    string[] infos = src.Split('|');
                    if (infos.Length > 0)
                        msrc = infos[0];
                    if (infos.Length > 1)
                        ext_src = infos[1];
                }
                else
                    msrc = src;
                if (string.IsNullOrEmpty(msrc))
                    continue;
                string fileType = GetFileType(msrc);
                //https://developer.mozilla.org/zh-CN/docs/Web/Guide/HTML/Using_HTML5_audio_and_video
                //<span class="curp" onclick="mediaPlayer(this,'VIDEO')" id="video1" data="/File/telUpMedia/1726/20180109103720_969.mp4">
                //<img src="/File/telUpMedia/1726/20180109103720_978.jpg" width="100" height="100" /></span>
                //<span title="长度：2.80"><img src="/userserver/imagehandler.ashx?action=getqr
                //&data=<%= CUnion.WebClass.SysBase.BaseConfig.siteUrl %>/File/telUpMedia/1726/20180109103640_663.amr" width="100" height="100" /></span>

                if (fileType == "VIDEO")
                {//sb.AppendFormat("<span class=\"curp\" onclick=\"mediaPlayer(this,'VIDEO')\" data=\"{0}\"><img src=\"{1}\" width=\"{2}\" height=\"{3}\" /></span>", msrc, ext_src, imgWidth, imgHeight);
                    sb.AppendFormat("<a href=\"{0}\" target=\"_blank\" title=\"点击播放\"><img src='{1}' width=\"{2}\" height=\"{3}\" /></a>", msrc, ext_src, imgWidth, imgHeight, ext_src);
                }
                else if (fileType == "SOUND")
                {
                    sb.AppendFormat("<a href=\"{0}\" target=\"_blank\" title=\"点击播放，时长：{4}\"><img src='/userserver/imagehandler.ashx?action=getqr&data={1}' width=\"{2}\" height=\"{3}\" /></a>", msrc, Handler.Server.UrlEncode(QRhostUrl + msrc), imgWidth, imgHeight, ext_src);
                }
                sb.Append(split);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 读取文件类型 IMAGE，VIDEO，SOUND，MIDI
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileType(string filePath)
        {
            string types = "UNKNOWN";
            string ext = Path.GetExtension(filePath).ToUpper().Replace(".", "");
            switch (ext)
            {
                case "MP3":
                case "M4A":
                case "WAV":
                case "AMR":
                case "AWB":
                case "WMA":
                case "OGG":
                    types = "SOUND";
                    break;
                case "MID":
                case "XMF":
                case "RTTTL":
                case "SMF":
                case "IMY":
                    types = "MIDI";
                    break;

                case "MP4":
                case "M4V":
                case "3GP":
                case "3GPP":
                case "3G2":
                case "3GPP2":
                case "WMV":
                    types = "VIDEO";
                    break;

                case "JPG":
                case "JPEG":
                case "GIF":
                case "PNG":
                case "BMP":
                case "WBMP":
                    types = "IMAGE";
                    break;
            }
            return types;
        }

        #endregion
    }
}
