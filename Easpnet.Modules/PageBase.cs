using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using Easpnet.Modules.Models;

namespace Easpnet.Modules
{
    /// <summary>
    /// 网页页面基类
    /// </summary>
    public class PageBase : System.Web.UI.UserControl
    {
        public MainMaster MasterPage;

        public PageInfo CurrentPage;
        
        /// <summary>
        /// 配置
        /// </summary>
        public NameValueCollection Configs;

        /// <summary>
        /// 网站根目录
        /// </summary>
        public string WebRootUrl;


        /// <summary>
        /// 输出的内容
        /// </summary>
        public string ResponseText;

        /// <summary>
        /// 是否有页面错误信息
        /// </summary>
        public bool HasError
        {
            get
            {
                return !this.MasterPage.Error.Success;
            }
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return this.MasterPage.Error.Message;
            }
        }

        /// <summary>
        /// 成功消息
        /// </summary>
        public string SuccessMessage
        {
            get
            {
                return this.MasterPage.SuccessMessage.Message;
            }
        }
        

        //
        protected void Page_Init(object sender, EventArgs e)
        {
            MasterPage = this.Page.Master as MainMaster;
            this.WebRootUrl = MasterPage.WebRootUrl;
            this.CurrentPage = MasterPage.CurrentPage;
            this.Configs = MasterPage.Configs;

            //
        }


        /// <summary>
        /// 获取图片的路径
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        protected virtual string ImageUrl(string img)
        {
            string imgpath = "";
            string tmpstr = "";

            // 检测当前模板图片
            //检测当前模块下的当前页面是否存在图片
            if (File.Exists(Server.MapPath(tmpstr = "~/Themes/" + MasterPage.CurrentTheme + "/Modules/" + MasterPage.CurrentPage.Module + "/Pages/" + MasterPage.CurrentPage.PageName + "/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", ""); 
            }
            //检测默认模板中当前页面图片
            else if (File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/Modules/" + MasterPage.CurrentPage.Module + "/Pages/" + MasterPage.CurrentPage.PageName + "/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", "");
            }
            //检测当前模块下的页面分组中是否存在图片
            else if (!string.IsNullOrEmpty(MasterPage.CurrentPage.PageGroupName) &&
                File.Exists(Server.MapPath(tmpstr = "~/Themes/" + MasterPage.CurrentTheme + "/Modules/" + MasterPage.CurrentPage.Module + "/PageGroups/" + MasterPage.CurrentPage.PageGroupName + "/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", ""); 
            }
            //检测默认模板中当前模块下的页面分组中是否存在图片
            else if (!string.IsNullOrEmpty(MasterPage.CurrentPage.PageGroupName) &&
                File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/Modules/" + MasterPage.CurrentPage.Module + "/PageGroups/" + MasterPage.CurrentPage.PageGroupName + "/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", "");
            }
            //检测当前模版页面分组中是否存在图片
            else if (!string.IsNullOrEmpty(MasterPage.CurrentPage.PageGroupName)&&
                File.Exists(Server.MapPath(tmpstr = "~/Themes/" + MasterPage.CurrentTheme + "/PageGroups/" + MasterPage.CurrentPage.PageGroupName + "/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", "");
            }
            //检测默认模版页面分组中是否存在图片
            else if (!string.IsNullOrEmpty(MasterPage.CurrentPage.PageGroupName) &&
                File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/PageGroups/" + MasterPage.CurrentPage.PageGroupName + "/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", "");
            }
            //检测当前模板图片文件夹中的图片
            else if (File.Exists(Server.MapPath(tmpstr = "~/Themes/" + MasterPage.CurrentTheme + "/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", "");
            }
            //检测默认模板图片文件夹中的图片
            else if (File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", "");
            }


            if (!string.IsNullOrEmpty(imgpath))
            {
                return WebRootUrl + imgpath;
            }
            else
            {
                return "";
            }            
        }


        /// <summary>
        /// 设置网页标题，关键字，描述。
        /// </summary>
        /// <param name="str"></param>
        protected void SetPageTitle(string title, string keywords, string description)
        {
            if (!string.IsNullOrEmpty(title))
            {
                MasterPage.PageTitle = title;
            }
            if (!string.IsNullOrEmpty(keywords))
            {
                MasterPage.MetaKeywords = keywords;
            }
            if (!string.IsNullOrEmpty(description))
            {
                MasterPage.MetaDescription = description;
            }

        }

        /// <summary>
        /// 添加Js文件
        /// </summary>
        /// <param name="path"></param>
        protected void AddJsFile(string path)
        {
            MasterPage.ScriptsFileList.Add(path);              
        }

        /// <summary>
        /// 添加CSS文件
        /// </summary>
        /// <param name="path"></param>
        protected void AddCssFile(string path)
        {
            MasterPage.StyleFileList.Add(path);              
        }

        /// <summary>
        /// 插入CSS文件
        /// </summary>
        /// <param name="path"></param>
        protected void InsertCssFile(string path)
        {
            MasterPage.StyleFileList.Insert(0, path);              
        }

        /// <summary>
        /// 想模板页中增加上下文数据值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        protected void AddContext(string name, string value)
        {
            MasterPage.MainContext.Add(name, value);
        }


        /// <summary>
        /// 获取表单Get值
        /// </summary>
        /// <param name="name"></param>
        protected string Get(string name)
        {
            return HttpContext.Current.Request.QueryString[name];
        }

        /// <summary>
        /// 获取表单Post值
        /// </summary>
        /// <param name="name"></param>
        protected string Post(string name)
        {
            return HttpContext.Current.Request.Form[name];
        }

        /// <summary>
        /// 是否存在Request.QueryString数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected bool ExistGet(string name)
        {
            return !string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[name]);
        }

        /// <summary>
        /// 是否存在Request.Form数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected bool ExistForm(string name)
        {
            return !string.IsNullOrEmpty(HttpContext.Current.Request.Form[name]);
        }

        /// <summary>
        /// 输出字符串，并停止执行
        /// </summary>
        /// <param name="text"></param>
        protected void Die(string text)
        {
            HttpContext.Current.Response.Write(text);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 获取静态化的url
        /// </summary>
        /// <returns></returns>
        public virtual string GetSeoUrl(string module, string page_name, params string[] parameters)
        {
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

            return WebRootUrl + module.ToLower() + "/" + page_name.ToLower() + "." + Html.UrlRewriteExt + str;
        }

        /// <summary>
        /// 控件添加到页面后做的操作
        /// </summary>
        public virtual void AfterAdded()
        {
 
        }


        /// <summary>
        /// 翻译内容，若要实现多语言界面，请调用此方法
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string T(string s)
        {
            return Html.T(s);
        }

        /// <summary>
        /// 添加错误消息
        /// </summary>
        /// <param name="s"></param>
        protected void AddErrorMessage(string s)
        {
            if (MasterPage.Error == null)
            {
                MasterPage.Error = new ErrorMessage();
            }

            MasterPage.Error.AddMessage(T(s));
        }


        /// <summary>
        /// 添加成功消息
        /// </summary>
        /// <param name="s"></param>
        protected void AddSuccessMessage(string s)
        {
            if (MasterPage.SuccessMessage == null)
            {
                MasterPage.SuccessMessage = new SuccessMessage();
            }

            MasterPage.SuccessMessage.AddMessage(T(s));
        }

        /// <summary>
        /// 添加成功消息
        /// </summary>
        /// <param name="s"></param>
        protected void AddSuccessMessage()
        {
            AddSuccessMessage("操作已成功！");
        }

        /// <summary>
        /// Ajax返回成功
        /// </summary>
        protected void AjaxSuccess(AjaxJsonAction action, string message, string url)
        {
            AjaxJsonData data = new AjaxJsonData();
            data.action = action;
            data.message = message;
            data.success = true;
            data.url = url;
            Die(data.ToString());
        }

        /// <summary>
        /// Ajax返回成功
        /// </summary>
        protected void AjaxSuccess(AjaxJsonAction action, string message, string url, object extdata)
        {
            AjaxJsonData data = new AjaxJsonData();
            data.action = action;
            data.message = message;
            data.success = true;
            data.url = url;
            data.data = extdata;
            Die(data.ToString());
        }

        /// <summary>
        /// Ajax返回失败
        /// </summary>
        protected void AjaxFail(AjaxJsonAction action, string message, string url)
        {
            AjaxJsonData data = new AjaxJsonData();
            data.action = action;
            data.message = message;
            data.success = false;
            data.url = url;
            Die(data.ToString());
        }

        /// <summary>
        /// Ajax返回失败
        /// </summary>
        protected void AjaxFail(string message)
        {
            AjaxJsonData data = new AjaxJsonData();
            data.action = AjaxJsonAction.none;
            data.message = message;
            data.success = false;
            data.url = "";
            Die(data.ToString());
        }

    }
}
