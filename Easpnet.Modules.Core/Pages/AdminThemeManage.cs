using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Easpnet.Modules.Models;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminThemeManage : PageBase
    {
        Theme theme;
        protected List<Theme> themes;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            theme = new Theme();
            //检测新模板
            CheckNewTheme();

            //
            Handler();

            //获取现有的模板列表            
            Query q = Query.NewQuery();
            q.OrderBy("ThemeName");
            themes = theme.GetList<Theme>(q);

            
        }


        /// <summary>
        /// 检测是否有新的模板，如有，则加入数据库
        /// </summary>
        void CheckNewTheme()
        {
            //
            string themepath = Server.MapPath("~/Themes");
            string[] dirs = Directory.GetDirectories(themepath);

            foreach (string item in dirs)
            {
                Match match = Regex.Match(item, "[a-zA-Z0-9 _\\.]*$");
                if (match.Success)
                {
                    //
                    Theme theme = new Theme();
                    theme.DirectoryName = match.Value;


                    //
                    string fpath = item + "\\theme.info";
                    if (File.Exists(fpath))
                    {
                        string content = File.ReadAllText(fpath);
                        Match m = Regex.Match(content, "(?<=name=)[^\n]+");
                        theme.ThemeName = m.Success ? m.Value : "";

                        m = Regex.Match(content, "(?<=core_version=)[^\n]+");
                        theme.CoreVersion = m.Success ? m.Value : "";

                        m = Regex.Match(content, "(?<=snapshot=)[^\n]+");
                        theme.Snapshot = m.Success ? m.Value : "";

                        m = Regex.Match(content, "(?<=author=)[^\n]+");
                        theme.Author = m.Success ? m.Value : "";

                        m = Regex.Match(content, "(?<=description=)[^\n]+");
                        theme.Description = m.Success ? m.Value : "";

                        //
                        if (!theme.Exists())
                        {
                            theme.Create();
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        void Handler()
        {
            if (Post("action") == "save")
            {
                //
                theme.Update("IsCurrentTheme", false);

                //
                theme.ThemeId = TypeConvert.ToInt64(Post("selectedTheme"));
                theme.GetModel();
                theme.IsCurrentTheme = true;
                theme.Update();
            }
        }
    }
}
