using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using Easpnet.Modules.Models;
using System.Web;
using System.Reflection;

namespace Easpnet.Modules
{
    /// <summary>
    /// Html输出类，包含诸如文本框、复选框、下拉列表框等表单元素的输出，以及超链接地址输出等方法
    /// </summary>
    public class Html
    {
        //private static string m_UrlRewriteExt = string.IsNullOrEmpty(Configs["url_rewrite_ext"]) ? "html" : Configs["url_rewrite_ext"];
        //private static string m_WebRootUrl = string.IsNullOrEmpty(ConfigurationManager.AppSettings["WebRootUrl"]) ? "/" : ConfigurationManager.AppSettings["WebRootUrl"];

        private static string m_UrlRewriteExt = "";

        /// <summary>
        /// 获取当前的执行的页面的IndexPage网页对象
        /// </summary>
        public static IndexPage IndexPage
        {
            get 
            {
                //获取当前处理的页面对象
                IndexPage page = HttpContext.Current.Handler as IndexPage;
                return page;
            }
        }

        /// <summary>
        /// 获取配置集合对象
        /// </summary>
        public static NameValueCollection Configs
        {
            get
            {
                if (IndexPage != null)
                {
                    return IndexPage.Configs;
                }
                else
                {
                    return Config.GetConfigs();
                }                 
            }
        }

        /// <summary>
        /// 语言内容
        /// </summary>
        public static NameValueCollection Langs = new NameValueCollection();

        /// <summary>
        /// 获取当前的执行的页面对象
        /// </summary>
        public static PageInfo CurrentPage
        {
            get 
            {
                return IndexPage.CurrentPage; 
            }            
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="val"></param>
        public static void SetConfigValue(string key, string val)
        {
            //获取当前处理的页面对象
            IndexPage page = HttpContext.Current.Handler as IndexPage;
            page.Configs[key] = val;
        }

        /// <summary>
        /// 当前的页面信息
        /// </summary>
        public static string CurrentPageName
        {
            get 
            {
                return IndexPage.CurrentPageName;
            }
        }

        /// <summary>
        /// 翻译内容，若要实现多语言界面，请调用此方法
        /// </summary>
        /// <param name="s">翻以前的字符串</param>
        /// <returns></returns>
        public static string T(string s)
        {
            string v = Langs[s];
            return string.IsNullOrEmpty(v) ? s : v;
        }


        /// <summary>
        /// 获取或者设置当前的模板
        /// </summary>
        public static string CurrentThemeName
        {
            get 
            {
                return IndexPage.CurrentTheme; 
            }
            set
            {
                IndexPage.CurrentTheme = value; 
            }
        }

        /// <summary>
        /// 获取或设置静态化的网页扩展名
        /// </summary>
        public static string UrlRewriteExt
        {
            get
            {
                if (!string.IsNullOrEmpty(m_UrlRewriteExt))
                {
                    return m_UrlRewriteExt;
                }
                else
                {
                    return string.IsNullOrEmpty(Configs["url_rewrite_ext"]) ? "html" : Configs["url_rewrite_ext"];
                }                
            }
            set
            {
                m_UrlRewriteExt = value;
            }
        }

        /// <summary>
        /// 获取或设置网站根目录路径：如: http://www.xxx.com/
        /// </summary>
        public static string WebRootUrl
        {
            get
            {
                if (IndexPage != null)
                {
                    return IndexPage.WebRootUrl; 
                }
                else
                {
                    return Local.LocalConfig.WebRootUrl;
                }
            }
            set
            {
                IndexPage.WebRootUrl = value; 
            }
        }

        /// <summary>
        /// 将相对路径转化为绝对的url路径，从根目录开始
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Url(object url)
        {
            if (url != null)
            {
                return WebRootUrl + url.ToString();
            }
            else
            {
                return WebRootUrl;
            }
        }

        /// <summary>
        /// 构造连接
        /// </summary>
        /// <param name="page_name">页面名称</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        //public static string HrefLink(string page_name, string parameters)
        //{
        //    if (string.IsNullOrEmpty(page_name))
        //    {
        //        page_name = "index";
        //    }

        //    if (!string.IsNullOrEmpty(parameters))
        //    {
        //        parameters = "?" + parameters;
        //    }

        //    //return WebRootUrl + "index.html?page=" + page_name + parameters;
        //    return WebRootUrl + page_name + ".html" + parameters;

        //    //
        //    //PageBase page;

        //}


        /// <summary>
        /// 构造连接
        /// </summary>
        /// <param name="module">模块名称</param>
        /// <param name="page_name">页面名称</param>
        /// <param name="parameters">参数</param>
        /// <returns>静态化后的URL链接</returns>
        public static string HrefLink(string module, string page_name, params string[] parameters)
        {
            //Models.Module md = Models.Module.GetModuleByName(module);
            //Assembly ass = Assembly.LoadFile(HttpContext.Current.Server.MapPath("~/bin/" + md.AssemblyName));
            //object obj = ass.CreateInstance("Easpnet.Modules.Huif.GameRecharge.Models.GameRechargeOrder");
            if (string.IsNullOrEmpty(module))
            {
                module = "";
            }

            if (string.IsNullOrEmpty(page_name))
            {
                page_name = "index";
            }

            string str = "";
            if (parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i == 0)
                    {
                        str += "?" + parameters[i];
                    }
                    else
                    {
                        str += "&" + parameters[i];
                    }
                }
            }

            if (!string.IsNullOrEmpty(module))
            {
                return "/" + module.ToLower() + "/" + page_name.ToLower() + "." + UrlRewriteExt + str;
            }
            else
            {
                return "/" + page_name.ToLower() + "." + UrlRewriteExt + str;
            }
        }


        /// <summary>
        /// 构造连接
        /// </summary>
        /// <param name="page_name">页面名称</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static string HrefLink(params string[] parameters)
        {
            return "/" + HttpContext.Current.Request.RawUrl.Substring(1);
        }
        

        /// <summary>
        /// 构造ajax的链接
        /// </summary>
        /// <param name="page_name"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string AjaxLink(BlockInfo block, string action)
        {
            return "/ajax.ashx?_m=" + block.Module + "&_b=" + block.BlockName + "&action=" + action;
        }



        /// <summary>
        /// 构造连接
        /// </summary>
        /// <param name="page_name"></param>
        /// <returns></returns>
        //public static string HrefLink(string page_name)
        //{
        //    return HrefLink(page_name, "");
        //}


        /// <summary>
        /// 构造Select
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string Select(string id, string name, string value, NameValueCollection values, params string[] options)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = name;
            }
            if (string.IsNullOrEmpty(name))
            {
                name = id;
            }

            //
            StringBuilder sb = new StringBuilder();
            sb.Append("<select name=\"" + name + "\" id=\"" + id + "\"");
            foreach (string s in options)
            {
                sb.Append(" " + s);
            }
            sb.Append(">");
            for (int i = 0; i < values.Count; i++)
            {
                if (value == values[i])
                {
                    sb.Append("<option value=\"" + values[i] + "\" selected=\"selected\">" + values.GetKey(i) + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"" + values[i] + "\">" + values.GetKey(i) + "</option>");
                }                
            }
            sb.Append("</select>");
            return sb.ToString();
        }


        /// <summary>
        /// 构造Select
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string Select(string id, string name, List<NameValue> values, string value, params string[] options)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = name;
            }
            if (string.IsNullOrEmpty(name))
            {
                name = id;
            }

            //
            StringBuilder sb = new StringBuilder();
            sb.Append("<select name=\"" + name + "\" id=\"" + id + "\"");
            foreach (string s in options)
            {
                sb.Append(" " + s);
            }
            sb.Append(">");
            for (int i = 0; i < values.Count; i++)
            {
                NameValue nv = values[i];
                if (value == nv.Value)
                {
                    sb.Append("<option value=\"" + nv.Value + "\" selected=\"selected\">" + nv.Name + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"" + nv.Value + "\">" + nv.Name + "</option>");
                }
            }
            sb.Append("</select>");
            return sb.ToString();
        }

        /// <summary>
        /// 对枚举类型输出Select选择框
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="tp"></param>
        /// <param name="firstText"></param>
        /// <param name="selectedValue"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string SelectEnums(string id, string name, Type tp, string firstText, string selectedValue, params string[] options)
        {
            if (!tp.IsEnum)
            {
                throw new Exception("第三个参数tp必须为枚举类型");
            }

            NameValueCollection nv = new NameValueCollection();
            if (!string.IsNullOrEmpty(firstText))
            {
                nv.Add(firstText, "");
            }
            string[] names = Enum.GetNames(tp);
            Array arr = Enum.GetValues(tp);
            for (int i = 0; i < names.Length; i++)
            {
                nv.Add(T(names[i]), TypeConvert.ToInt32(arr.GetValue(i)).ToString());
            }

            return Html.Select(id, name, selectedValue, nv, options);
        }

        /// <summary>
        /// 输出单个单选框
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public static string Radio(string name, string id, string value, bool selected)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = name;
            }
            if (string.IsNullOrEmpty(name))
            {
                name = id;
            }

            if (selected)
            {
                return "<input type=\"radio\" id=\"" + id + "\" name=\"" + name + "\" checked=\"checked\" value=\"" + value + "\"/>";
            }
            else
            {
                return "<input type=\"radio\" id=\"" + id + "\" name=\"" + name + "\" value=\"" + value + "\"/>";
            }
        }



        /// <summary>
        /// 构造Radio
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string RadioList(string name, NameValueCollection values, string value, params string[] options)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < values.Count; i++)
            {
                string id = name + values[i];
                sb.Append("<span class=\"radioitem\">");
                if (value == values[i])
                {
                    sb.Append("<input type=\"radio\" id=\"" + id + "\" name=\"" + name + "\" checked=\"checked\" value=\"" + values[i] + "\"");
                    foreach (string s in options)
                    {
                        sb.Append(" " + s);
                    }
                    sb.Append("/>");
                }
                else
                {
                    sb.Append("<input type=\"radio\" id=\"" + id + "\" name=\"" + name + "\" value=\"" + values[i] + "\"");
                    foreach (string s in options)
                    {
                        sb.Append(" " + s);
                    }
                    sb.Append("/>");
                }

                sb.Append("<label for=\"" + id + "\">");
                sb.Append(values.GetKey(i));
                sb.Append("</label>");
                sb.Append("</span>");
            }

            return sb.ToString();
        }


        /// <summary>
        /// 构造Radio
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string RadioList(string name, List<NameValue> values, string value)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < values.Count; i++)
            {
                NameValue nv = values[i];
                string id = name + nv.Value;
                sb.Append("<span class=\"radioitem\">");
                if (value == nv.Value)
                {
                    sb.Append("<input type=\"radio\" id=\"" + id + "\" name=\"" + name + "\" checked=\"checked\" value=\"" + nv.Value + "\"/>");
                }
                else
                {
                    sb.Append("<input type=\"radio\" id=\"" + id + "\" name=\"" + name + "\" value=\"" + nv.Value + "\"/>");
                }

                sb.Append("<label for=\"" + id + "\">");
                sb.Append(nv.Name);
                sb.Append("</label>");
                sb.Append("</span>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 对枚举类型输出radio选择框
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="tp"></param>
        /// <param name="firstText"></param>
        /// <param name="selectedValue"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string RadioEnums(string name, Type tp, string selectedValue, params string[] options)
        {
            if (!tp.IsEnum)
            {
                throw new Exception("第三个参数tp必须为枚举类型");
            }

            NameValueCollection nv = new NameValueCollection();
            string[] names = Enum.GetNames(tp);
            Array arr = Enum.GetValues(tp);
            for (int i = 0; i < names.Length; i++)
            {
                nv.Add(T(names[i]), TypeConvert.ToInt32(arr.GetValue(i)).ToString());
            }

            return Html.RadioList(name, nv, selectedValue, options);
        }

        /// <summary>
        /// 输出单个复选框
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public static string CheckBox(string name, string id, string value, bool selected, params string[] options)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = name;
            }
            if (string.IsNullOrEmpty(name))
            {
                name = id;
            }

            string option = "";
            if (options != null)
            {
                foreach (string s in options)
                {
                    option += " " + s;
                }
            }

            if (selected)
            {
                return "<input type=\"checkbox\" id=\"" + id + "\" name=\"" + name + "\" checked=\"checked\" value=\"" + value + "\" " + option + "/>";
            }
            else
            {
                return "<input type=\"checkbox\" id=\"" + id + "\" name=\"" + name + "\" value=\"" + value + "\" " + option + "/>";
            }
        }
        


        /// <summary>
        /// 构造复选框列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CheckBoxList(string name, NameValueCollection values, string value)
        {
            string[] arr = null;
            if (!string.IsNullOrEmpty(value))
            {
                arr = value.Split(new char[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries);
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < values.Count; i++)
            {
                bool selected = false;
                if (arr != null)
                {
                    foreach (string item in arr)
                    {
                        if (item == values[i])
                        {
                            selected = true;
                            break;
                        }
                    }
                }                

                string id = name + values[i];
                sb.Append("<span>");

                if (selected)
                {
                    sb.Append("<input type=\"checkbox\" checked=\"checked\" id=\"" + id + "\" name=\"" + name + "\" value=\"" + values[i] + "\"/>");
                }
                else
                {
                    sb.Append("<input type=\"checkbox\" id=\"" + id + "\" name=\"" + name + "\" value=\"" + values[i] + "\"/>");
                }

                sb.Append("<label for=\"" + id + "\">");
                sb.Append(values.GetKey(i));
                sb.Append("</label>");
                sb.Append("</span>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 构造复选框列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CheckBoxList(string name, List<NameValue> values, string value)
        {
            string[] arr = null;
            if (!string.IsNullOrEmpty(value))
            {
                arr = value.Split(new char[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries);
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < values.Count; i++)
            {
                NameValue nv = values[i];
                bool selected = false;
                if (arr != null)
                {
                    foreach (string item in arr)
                    {
                        if (item == nv.Value)
                        {
                            selected = true;
                            break;
                        }
                    }
                }
                
                string id = name + nv.Value;
                sb.Append("<span>");

                if (selected)
                {
                    sb.Append("<input type=\"checkbox\" checked=\"checked\" id=\"" + id + "\" name=\"" + name + "\" value=\"" + nv.Value + "\"/>");
                }
                else
                {
                    sb.Append("<input type=\"checkbox\" id=\"" + id + "\" name=\"" + name + "\" value=\"" + nv.Value + "\"/>");
                }                
                sb.Append("<label for=\"" + id + "\">");
                sb.Append(nv.Name);
                sb.Append("</label>");
                sb.Append("</span>");
            }

            return sb.ToString();
        }



        /// <summary>
        /// 构造隐藏域表单元素
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string InputHidden(string id, string name, string value)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = name;
            }
            if (string.IsNullOrEmpty(name))
            {
                name = id;
            }
            return "<input type=\"hidden\" id=\"" + id + "\" name=\"" + name + "\" value=\"" + value + "\" />";
        }


        /// <summary>
        /// 构造文本框
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string InputText(string id, string name, string value, string cssclass, params string[] options)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = name;
            }
            if (string.IsNullOrEmpty(name))
            {
                name = id;
            }

            if (string.IsNullOrEmpty(cssclass))
            {
                cssclass = "textbox";
            }
            else
            {
                cssclass = "textbox " + cssclass;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<input type=\"text\" id=\"" + id + "\" name=\"" + name + "\" value=\"" + value + "\"");
            sb.Append(" class=\"" + cssclass + "\"");
            foreach (string s in options)
            {
                sb.Append(" " + s);
            }
            sb.Append("/>");
            return sb.ToString();
        }


        /// <summary>
        /// 构造密码框
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string InputPassword(string id, string name, string value, string cssclass, params string[] options)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = name;
            }
            if (string.IsNullOrEmpty(name))
            {
                name = id;
            }

            if (string.IsNullOrEmpty(cssclass))
            {
                cssclass = "textbox";
            }
            else
            {
                cssclass = "textbox " + cssclass;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<input type=\"password\" id=\"" + id + "\" name=\"" + name + "\" value=\"" + value + "\"");
            sb.Append(" class=\"" + cssclass + "\"");
            foreach (string s in options)
            {
                sb.Append(" " + s);
            }
            sb.Append("/>");
            return sb.ToString();
        }



        /// <summary>
        /// 文件选择输入框
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string InputFile(string id, string name, string value, string cssclass, params string[] options)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = name;
            }
            if (string.IsNullOrEmpty(name))
            {
                name = id;
            }

            if (string.IsNullOrEmpty(cssclass))
            {
                cssclass = "filebox";
            }
            else
            {
                cssclass = "filebox " + cssclass;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<input type=\"file\" id=\"" + id + "\" name=\"" + name + "\" ");
            sb.Append(" class=\"" + cssclass + "\"");
            foreach (string s in options)
            {
                sb.Append(" " + s);
            }
            sb.Append("/>");
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append("<br/><a href=\"" + Html.Url(value) + "\" target=\"_blank\">" + Html.Url(value) + "</a>");
            }
            return sb.ToString();
        }



        /// <summary>
        /// 构造文本区域
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="cssclass"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string TextArea(string id, string name, string value, int rows, int cols, params string[] options)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = name;
            }
            if (string.IsNullOrEmpty(name))
            {
                name = id;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<textarea type=\"text\" id=\"" + id + "\" name=\"" + name + "\" rows=\"" + rows + "\" cols=\"" + cols + "\"");
            foreach (string s in options)
            {
                sb.Append(" " + s);
            }
            sb.Append(">");
            sb.Append(value);
            sb.Append("</textarea>");
            return sb.ToString();
        }


        /// <summary>
        /// 输出提交按钮
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string Submit(string id, string name, string value, params string[] options)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = name;
            }
            if (string.IsNullOrEmpty(name))
            {
                name = id;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<input type=\"submit\" id=\"" + id + "\" name=\"" + name + "\" value=\"" + value + "\" ");
            foreach (string s in options)
            {
                sb.Append(" " + s);
            }
            sb.Append(" />");
            return sb.ToString();
        }

        /// <summary>
        /// 输出图片
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        public static string Image(string url, int width, int height, string alt)
        {
            string path = HttpContext.Current.Server.MapPath("~/" + url);
            if (!System.IO.File.Exists(path))
            {
                url = "Themes/" + CurrentThemeName + "/Images/noimg/" + width + "x" + height + ".jpg";
            }

            //
            return "<img src=\"" + Url(url) + "\" alt=\"" + alt + "\" width=\"" + width + "\" height=\"" + height + "\">";
        }


        /// <summary>
        /// 提示消息
        /// </summary>
        /// <returns></returns>
        public static string Message(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"message\">");
            sb.Append("<h4>提示信息</h4>");
            sb.Append("<div>");
            sb.Append(msg);
            sb.Append("</div>");
            sb.Append("</div>");

            return sb.ToString();
        }
    }
}
