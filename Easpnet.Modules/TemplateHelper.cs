using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

namespace Easpnet.Modules
{
    /// <summary>
    /// 模板相关处理
    /// 模板存放目录在：Templates目录下面
    /// </summary>
    public class TemplateHelper
    {
        /// <summary>
        /// 给定模板的相对路径，获取模板的内容
        /// </summary>
        /// <param name="relativeTempDir">模板的相对路径</param>
        /// <returns></returns>
        public static string ReadTemplateContent(string relativeTempDir, params NameObject[] replace)
        {
            string filePath = GetTemplateDirectory(relativeTempDir);
            if (!string.IsNullOrEmpty(filePath))
            {
                string s = File.ReadAllText(filePath);

                if (replace != null)
                {
                    foreach (NameObject item in replace)
                    {
                        string rep = "";
                        if (item.Value != null)
                        {
                            rep = item.Value.ToString();          
                        }
                        s = s.Replace(item.Name, rep);
                    }
                }

                return s;
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 给定模板的相对路径，获取模板的路径：
        /// 按照如下的顺序查找，若文件存在，则立即返回，顺序为：
        /// 当前模板当前页面的 Templates目录 > 
        /// 默认模板当前页面的 Templates目录 > 
        /// 当前模板当前模块的页面分组的 Templates目录 > 
        /// 默认模板当前模块的页面分组的 Templates目录 >
        /// 当前模板公共页面分组的 Templates目录 >
        /// 默认模板公共页面分组的 Templates目录 >
        /// 当前模板的当前模块的 Templates目录 >
        /// 默认模板的当前模块的 Templates目录 >
        /// 当前模板的 Templates目录 >
        /// 默认模板的 Templates目录 > 
        /// </summary>
        /// <param name="tempDir">模板的相对路径</param>
        /// <returns></returns>
        public static string GetTemplateDirectory(string relativeTempDir)
        {
            HttpServerUtility Server = HttpContext.Current.Server;
            string dir = "";
            string filePath = "";
            string CurrentTheme = Html.CurrentThemeName;
            string Module = Html.CurrentPage.Module;
            string PageName = Html.CurrentPageName;
            string PageGroupName = Html.CurrentPage.PageGroupName;


            //当前模板当前页面的 Templates目录 > 
            dir = "Themes/" + CurrentTheme + "/Modules/" + Module + "/Pages/" + PageName + "/Templates/" + relativeTempDir;
            if (File.Exists(filePath = Server.MapPath(dir)))
            {
                return filePath;
            }

            //默认模板当前页面的 Templates目录 > 
            dir = "Themes/Default/Modules/" + Module + "/Pages/" + PageName + "/Templates/" + relativeTempDir;
            if (File.Exists(filePath = Server.MapPath(dir)))
            {
                return filePath;
            }

            //页面分组
            if (!string.IsNullOrEmpty(PageGroupName))
            {
                // 当前模板当前模块的页面分组的 Templates目录 > 
                dir = "Themes/" + CurrentTheme + "/Modules/" + Module + "/PageGroups/" + PageGroupName + "/Templates/" + relativeTempDir;
                if (File.Exists(filePath = Server.MapPath(dir)))
                {
                    return filePath;
                }

                //默认模板当前模块的页面分组的 Templates目录 >
                dir = "Themes/Default/Modules/" + Module + "/PageGroups/" + PageGroupName + "/Templates/" + relativeTempDir;
                if (File.Exists(filePath = Server.MapPath(dir)))
                {
                    return filePath;
                }

                //当前模板公共页面分组的 Templates目录 >
                dir = "Themes/" + CurrentTheme + "/PageGroups/" + PageGroupName + "/Templates/" + relativeTempDir;
                if (File.Exists(filePath = Server.MapPath(dir)))
                {
                    return filePath;
                }

                //默认模板公共页面分组的 Templates目录 >
                dir = "Themes/Default/PageGroups/" + PageGroupName + "/Templates/" + relativeTempDir;
                if (File.Exists(filePath = Server.MapPath(dir)))
                {
                    return filePath;
                }
            }

            //当前模板的当前模块的 Templates目录 >
            dir = "Themes/" + CurrentTheme + "/Modules/" + Module + "/Templates/" + relativeTempDir;
            if (File.Exists(filePath = Server.MapPath(dir)))
            {
                return filePath;
            }

            //默认模板的当前模块的 Templates目录 >
            dir = "Themes/Default/Modules/" + Module + "/Templates/" + relativeTempDir;
            if (File.Exists(filePath = Server.MapPath(dir)))
            {
                return filePath;
            }

            //当前模板的 Templates目录 >
            dir = "Themes/" + CurrentTheme + "/Templates/" + relativeTempDir;
            if (File.Exists(filePath = Server.MapPath(dir)))
            {
                return filePath;
            }

            //默认模板的 Templates目录 >
            dir = "Themes/Default/Templates/" + relativeTempDir;
            if (File.Exists(filePath = Server.MapPath(dir)))
            {
                return filePath;
            }

            return "";
        }



    }
}
