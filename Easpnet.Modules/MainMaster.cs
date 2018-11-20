using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Easpnet.Modules.Models;

namespace Easpnet.Modules
{
    public class MainMaster : System.Web.UI.MasterPage
    {
        /// <summary>
        /// 标题
        /// </summary>
        private string m_PageTitle;


        /// <summary>
        /// 当前的页面信息
        /// </summary>
        public PageInfo CurrentPage;

        /// <summary>
        /// 当前所属的页面分组，若没有指定，则为空
        /// </summary>
        public PageGroup CurrentPageGroup;

        /// <summary>
        /// 网页标题
        /// </summary>
        public string PageTitle { get { return m_PageTitle; } set { m_PageTitle = value; } }

        /// <summary>
        /// 网页关键字
        /// </summary>
        public string MetaKeywords;

        /// <summary>
        /// 网页描述
        /// </summary>
        public string MetaDescription;

        /// <summary>
        /// 网站根目录路径：如: http://www.kexinsoft.com/
        /// </summary>
        public string WebRootUrl = "/";

        /// <summary>
        /// 当前的模板
        /// </summary>
        public string CurrentTheme = "Classic";

        /// <summary>
        /// 样式表
        /// </summary>
        public string Styles;
        /// <summary>
        /// 样式表文件列表
        /// </summary>
        public List<string> StyleFileList = new List<string>();
        /// <summary>
        /// 脚本
        /// </summary>
        public string Scripts;
        /// <summary>
        /// 脚本文件列表
        /// </summary>
        public List<string> ScriptsFileList = new List<string>();

        /// <summary>
        /// 配置
        /// </summary>
        public NameValueCollection Configs;

        /// <summary>
        /// 存储值
        /// </summary>
        public NameObjectCollection MainContext = new NameObjectCollection();
        /// <summary>
        /// 获取或设置是否启用多语言
        /// </summary>
        public bool MultiLanguageEnabled = false;
        /// <summary>
        /// 获取或设置当前语言
        /// </summary>
        public string LanguageCode = "zh-cn";

        /// <summary>
        /// 主框架的Id
        /// </summary>
        public string MainWrapperId = "contentMainWrapper";

        /// <summary>
        /// 主框架的Class
        /// </summary>
        public string MainWrapperClass = "";

        /// <summary>
        /// 网页主框架Div的Id
        /// </summary>
        public string BodyId = "";

        //错误消息
        public ErrorMessage Error = new ErrorMessage();

        //成功消息
        public SuccessMessage SuccessMessage = new SuccessMessage();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            //当前页面信息
            IndexPage tmp_page = this.Page as IndexPage;
            this.CurrentPage = tmp_page.CurrentPage;
            this.CurrentTheme = tmp_page.CurrentTheme;
            this.CurrentPageGroup = tmp_page.CurrentPageGroup;
            this.Configs = tmp_page.Configs;

            //加载多语言内容
            NameValueCollection Langs = new NameValueCollection();
            LanguageHelper.ReadLanguages(Langs, Server.MapPath("~/Languages/" + LanguageCode + "/Language.xml"));
            LanguageHelper.ReadLanguages(Langs, Server.MapPath("~/Languages/" + LanguageCode + "/" + CurrentPage.Module + "/" + CurrentPage.Module + ".xml"));
            LanguageHelper.ReadLanguages(Langs, Server.MapPath("~/Languages/" + LanguageCode + "/" + CurrentPage.Module + "/Pages/" + CurrentPage.PageName + ".xml"));
            Html.Langs = Langs;

            //配置读取
            WebRootUrl = Html.WebRootUrl;

            //添加CSS样式
            AddCssStyle();

            //添加js文件
            AddJsFiles();

            //PageTitle
            PageTitle = Configs["site_name"] + Configs["site_seo_title"];

            //SEO Keywords
            MetaKeywords = Configs["site_seo_meta_keywords"];

            //SEO Description
            MetaDescription = Configs["site_seo_meta_description"];

            //
            if (!string.IsNullOrEmpty(CurrentPage.Module))
            {
                BodyId = CurrentPage.Module + "-";
            }
            BodyId += CurrentPage.PageName;

            MainWrapperId += "-" + BodyId;
        }


        //即将显示网页时执行
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ///////// 构造样式文件输出 /////////
            foreach (string str in StyleFileList)
            {
                Styles += "<link href=\"" + WebRootUrl + str + "\" rel=\"stylesheet\" type=\"text/css\" />";
            }

            ///////// 构造脚本输出 /////////
            //当前页面js文件
            string dir = "";
            FileInfo[] files;
            string filePath;    //临时存储文件路径的变量
            string[] arr;       //临时存储器，在加载js.load，存储分析得到的js文件路径
            char[] sp = new char[] { '\n'};

            #region [ 加载 js.load 文件中指定的js文件 ]
            //////////// 加载 js.load 文件中指定的js文件
            // 每行为一条js代码【注意，这里不会检测js文件是否存在，请确认书写正确】
            //加载顺序：模版目录 > 通用页面分组目录 > 当前模块页面分组目录 > 当前页面目录            

            #region [Default模板]
            //Default模板目录js.load
            dir = "Themes/Default/";
            if (File.Exists(filePath = Server.MapPath("~/" + dir + "js.load")))
            {
                string content = File.ReadAllText(filePath);
                arr = content.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in arr)
                {
                    if (!ScriptsFileList.Exists(s => s == item))
                    {
                        ScriptsFileList.Add(item);
                    }
                }                                
            }

            //模块目录js.load
            dir = "Themes/Default/Modules/" + CurrentPage.Module + "/";
            if (File.Exists(filePath = Server.MapPath("~/" + dir + "js.load")))
            {
                string content = File.ReadAllText(filePath);
                arr = content.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in arr)
                {
                    if (!ScriptsFileList.Exists(s => s == item))
                    {
                        ScriptsFileList.Add(item);
                    }
                }
            }

            //通用页面分组目录js.load
            dir = "Themes/Default/PageGroups/" + CurrentPage.PageGroupName + "/";
            if (File.Exists(filePath = Server.MapPath("~/" + dir + "js.load")))
            {
                string content = File.ReadAllText(filePath);
                arr = content.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in arr)
                {
                    if (!ScriptsFileList.Exists(s => s == item))
                    {
                        ScriptsFileList.Add(item);
                    }
                }
            }
            //当前模块页面分组目录js.load
            dir = "Themes/Default/Modules/" + CurrentPage.Module + "/PageGroups/" + CurrentPage.PageGroupName + "/";            
            if (File.Exists(filePath = Server.MapPath("~/" + dir + "js.load")))
            {
                string content = File.ReadAllText(filePath);
                arr = content.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in arr)
                {
                    if (!ScriptsFileList.Exists(s => s == item))
                    {
                        ScriptsFileList.Add(item);
                    }
                }
            }
            //当前页面目录js.load
            dir = "Themes/Default/Modules/" + CurrentPage.Module + "/Pages/" + CurrentPage.PageName + "/";
            if (File.Exists(filePath = Server.MapPath("~/" + dir + "js.load")))
            {
                string content = File.ReadAllText(filePath);
                arr = content.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in arr)
                {
                    if (!ScriptsFileList.Exists(s => s == item))
                    {
                        ScriptsFileList.Add(item);
                    }
                }
            }

            #endregion

            #region [当前模板]
            
            //当前模板目录js.load
            dir = "Themes/" + CurrentTheme + "/";
            if (File.Exists(filePath = Server.MapPath("~/" + dir + "js.load")))
            {
                string content = File.ReadAllText(filePath);
                arr = content.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in arr)
                {
                    if (!ScriptsFileList.Exists(s => s == item))
                    {
                        ScriptsFileList.Add(item);
                    }
                }
            }

            //模块目录js.load
            dir = "Themes/" + CurrentTheme + "/Modules/" + CurrentPage.Module + "/";
            if (File.Exists(filePath = Server.MapPath("~/" + dir + "js.load")))
            {
                string content = File.ReadAllText(filePath);
                arr = content.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in arr)
                {
                    if (!ScriptsFileList.Exists(s => s == item))
                    {
                        ScriptsFileList.Add(item);
                    }
                }
            }

            //通用页面分组目录js.load
            dir = "Themes/" + CurrentTheme + "/PageGroups/" + CurrentPage.PageGroupName + "/";
            if (File.Exists(filePath = Server.MapPath("~/" + dir + "js.load")))
            {
                string content = File.ReadAllText(filePath);
                arr = content.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in arr)
                {
                    if (!ScriptsFileList.Exists(s => s == item))
                    {
                        ScriptsFileList.Add(item);
                    }
                }
            }
            //当前模块页面分组目录js.load
            dir = "Themes/" + CurrentTheme + "/Modules/" + CurrentPage.Module + "/PageGroups/" + CurrentPage.PageGroupName + "/";
            if (File.Exists(filePath = Server.MapPath("~/" + dir + "js.load")))
            {
                string content = File.ReadAllText(filePath);
                arr = content.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in arr)
                {
                    if (!ScriptsFileList.Exists(s => s == item))
                    {
                        ScriptsFileList.Add(item);
                    }
                }
            }
            //当前页面目录js.load
            dir = "Themes/" + CurrentTheme + "/Modules/" + CurrentPage.Module + "/Pages/" + CurrentPage.PageName + "/";
            if (File.Exists(filePath = Server.MapPath("~/" + dir + "js.load")))
            {
                string content = File.ReadAllText(filePath);
                arr = content.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in arr)
                {
                    if (!ScriptsFileList.Exists(s => s == item))
                    {
                        ScriptsFileList.Add(item);
                    }
                }
            }

            #endregion


            #endregion


            #region [ 加载页面文件夹中的js文件 ]

            //////////// 加载当前页面中的js文件
            //添加页面文件夹中的js文件 - 默认模板中
            dir = "Themes/Default/Modules/" + CurrentPage.Module + "/Pages/" + CurrentPage.PageName + "/";
            if (Directory.Exists(Server.MapPath("~/" + dir)))
            {
                files = new DirectoryInfo(Server.MapPath("~/" + dir)).GetFiles("*.js");
                foreach (System.IO.FileInfo item in files)
                {
                    ScriptsFileList.Add(dir + item.Name);
                }
            }

            //添加页面文件夹中的js文件 - 当前模板
            dir = "Themes/" + CurrentTheme + "/Modules/" + CurrentPage.Module + "/Pages/" + CurrentPage.PageName + "/";
            if (Directory.Exists(Server.MapPath("~/" + dir)))
            {
                files = new DirectoryInfo(Server.MapPath("~/" + dir)).GetFiles("*.js");
                foreach (System.IO.FileInfo item in files)
                {
                    ScriptsFileList.Add(dir + item.Name);
                }
            }

            #endregion

            //////////// 加载通用的js文件
            //将jquery框架和通用的js代码插入到开头
            ScriptsFileList.Insert(0, "Static/js/dnet.js");
            ScriptsFileList.Insert(0, "Static/js/jquery.js");

            foreach (string str in ScriptsFileList)
            {
                Scripts += "<script languages=\"javascript\" src=\"" + WebRootUrl + str + "\"></script>";
            }
        }


        /// <summary>
        /// 添加CSS样式
        /// </summary>
        private void AddCssStyle()
        {
            //样式表
            StyleFileList.Add("Themes/Default/style.css");                              //加载默认主题的通用样式
            StyleFileList.Add("Themes/" + CurrentTheme + "/style.css");                 //加载当前主题的通用样式

            //其他公共样式表
            FileInfo[] page_style_files = new DirectoryInfo(Server.MapPath("~/Themes/" + CurrentTheme)).GetFiles("*.css");
            foreach (System.IO.FileInfo item in page_style_files)
            {
                if (item.Name != "style.css")
                {
                    StyleFileList.Add("Themes/" + CurrentTheme + "/" + item.Name);
                }                
            }
            //
            string dir = "";

            //当前模块的样式
            dir = "Themes/Default/Modules/" + CurrentPage.Module + "/style.css";
            if (File.Exists(Server.MapPath("~/" + dir)))
            {
                StyleFileList.Add(dir);
            }
            dir = "Themes/" + CurrentTheme + "/Modules/" + CurrentPage.Module + "/style.css";
            if (File.Exists(Server.MapPath("~/" + dir)))
            {
                StyleFileList.Add(dir);
            }


            //页面组的样式
            if (!string.IsNullOrEmpty(CurrentPage.PageGroupName))
            {
                //公共
                dir = "Themes/Default/PageGroups/" + CurrentPage.PageGroupName + "/";
                if (Directory.Exists(Server.MapPath("~/" + dir)))
                {
                    page_style_files = new DirectoryInfo(Server.MapPath("~/" + dir)).GetFiles("*.css");
                    foreach (System.IO.FileInfo item in page_style_files)
                    {
                        StyleFileList.Add(dir + item.Name);
                    }
                }

                //
                dir = "Themes/" + CurrentTheme + "/PageGroups/" + CurrentPage.PageGroupName + "/";
                if (Directory.Exists(Server.MapPath("~/" + dir)))
                {
                    page_style_files = new DirectoryInfo(Server.MapPath("~/" + dir)).GetFiles("*.css");
                    foreach (System.IO.FileInfo item in page_style_files)
                    {
                        StyleFileList.Add(dir + item.Name);
                    }
                }

                //当前模块
                dir = "Themes/Default/Modules/" + CurrentPage.Module + "/PageGroups/" + CurrentPage.PageGroupName + "/";
                if (Directory.Exists(Server.MapPath("~/" + dir)))
                {
                    page_style_files = new DirectoryInfo(Server.MapPath("~/" + dir)).GetFiles("*.css");
                    foreach (System.IO.FileInfo item in page_style_files)
                    {
                        StyleFileList.Add(dir + item.Name);
                    }
                }

                //
                dir = "Themes/" + CurrentTheme + "/Modules/" + CurrentPage.Module + "/PageGroups/" + CurrentPage.PageGroupName + "/";
                if (Directory.Exists(Server.MapPath("~/" + dir)))
                {
                    page_style_files = new DirectoryInfo(Server.MapPath("~/" + dir)).GetFiles("*.css");
                    foreach (System.IO.FileInfo item in page_style_files)
                    {
                        StyleFileList.Add(dir + item.Name);
                    }
                }
            }
            
            //当前页面样式
            dir = "Themes/Default/Modules/" + CurrentPage.Module + "/Pages/" + CurrentPage.PageName + "/";
            if (Directory.Exists(Server.MapPath("~/" + dir)))
            {
                page_style_files = new DirectoryInfo(Server.MapPath("~/" + dir)).GetFiles("*.css");
                foreach (System.IO.FileInfo item in page_style_files)
                {
                    StyleFileList.Add(dir + item.Name);
                }
            }

            dir = "Themes/" + CurrentTheme + "/Modules/" + CurrentPage.Module + "/Pages/" + CurrentPage.PageName + "/";
            if (Directory.Exists(Server.MapPath("~/" + dir)))
            {
                page_style_files = new DirectoryInfo(Server.MapPath("~/" + dir)).GetFiles("*.css");
                foreach (System.IO.FileInfo item in page_style_files)
                {
                    StyleFileList.Add(dir + item.Name);
                }
            }

        }


        /// <summary>
        /// 添加js文件
        /// </summary>
        private void AddJsFiles()
        {
            
        }

        #region [ 通用函数 ]

        /********** 通用函数 **********************************/
        /// <summary>
        /// 获取图片的路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string ImageUrl(string img)
        {
            string imgpath = "";
            string tmpstr = "";

            // 检测当前模板图片
            //检测当前模块下的当前页面是否存在图片
            if (File.Exists(Server.MapPath(tmpstr = "~/Themes/" + CurrentTheme + "/Modules/" + CurrentPage.Module + "/Pages/" + CurrentPage.PageName + "/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", "");
            }
            //检测默认模板中当前页面图片
            else if (File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/Modules/" + CurrentPage.Module + "/Pages/" + CurrentPage.PageName + "/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", "");
            }
            //检测当前模块下的页面分组中是否存在图片
            else if (!string.IsNullOrEmpty(CurrentPage.PageGroupName) &&
                File.Exists(Server.MapPath(tmpstr = "~/Themes/" + CurrentTheme + "/Modules/" + CurrentPage.Module + "/PageGroups/" + CurrentPage.PageGroupName + "/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", "");
            }
            //检测默认模板中当前模块下的页面分组中是否存在图片
            else if (!string.IsNullOrEmpty(CurrentPage.PageGroupName) &&
                File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/Modules/" + CurrentPage.Module + "/PageGroups/" + CurrentPage.PageGroupName + "/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", "");
            }
            //检测当前模版页面分组中是否存在图片
            else if (!string.IsNullOrEmpty(CurrentPage.PageGroupName) &&
                File.Exists(Server.MapPath(tmpstr = "~/Themes/" + CurrentTheme + "/PageGroups/" + CurrentPage.PageGroupName + "/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", "");
            }
            //检测默认模版页面分组中是否存在图片
            else if (!string.IsNullOrEmpty(CurrentPage.PageGroupName) &&
                File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/PageGroups/" + CurrentPage.PageGroupName + "/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", "");
            }
            //检测当前模板图片文件夹中的图片
            else if (File.Exists(Server.MapPath(tmpstr = "~/Themes/" + CurrentTheme + "/Images/" + img)))
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
        /// 获取上传文件的路径
        /// </summary>
        /// <param name="up"></param>
        /// <returns></returns>
        public string UploadUrl(string up)
        {
            return WebRootUrl + "upload/" + up;
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

        #endregion
    }
}
