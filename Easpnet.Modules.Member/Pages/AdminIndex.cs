using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Modules.Models;

namespace Easpnet.Modules.Member.Pages
{
    /// <summary>
    /// 后台管理首页
    /// </summary>
    public class AdminIndex : AdminPageBase
    {
        List<AdminMenu> menus;
        protected StringBuilder sbmenu = new StringBuilder();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            PopulateMenuHtml();
        }


        void PopulateMenuHtml()
        {
            //系统设置菜单
            Config config = new Config();
            List<NameValue> types = config.GetConfigTypeList();
            sbmenu.AppendLine("<div class=\"accordionHeader\">");
            sbmenu.AppendLine("<h2><span>Folder</span>" + T("系统设置") + "</h2>");
            sbmenu.AppendLine("</div>");
            sbmenu.AppendLine("<div class=\"accordionContent\">");
            sbmenu.AppendLine("    <ul class=\"tree treeFolder\">");
            AdminMenu menu;
            //
            for (int i = 0; i < types.Count; i++ )
            {
                NameValue nv = types[i];
                menu = new AdminMenu();
                menu.AdminMenuId = 100000 + i;
                menu.MenuText = T(nv.Name);
                menu.Module = "Core";
                menu.Page = "AdminConfigManage";
                menu.Parameters = "type=" + nv.Value;
                sbmenu.Append(PopulateMenu(menu));
            }

            //模板设置菜单
            menu = new AdminMenu();
            menu.AdminMenuId = 100000 + types.Count;
            menu.MenuText = T("模板设置");
            menu.Module = "Core";
            menu.Page = "AdminThemeManage";
            sbmenu.Append(PopulateMenu(menu));            

            //
            sbmenu.AppendLine("    </ul>");
            sbmenu.AppendLine("</div>");


            //数据库中的菜单。
            menus = AdminMenu.GetMenuTree();
            foreach (AdminMenu group in menus)
            {
                sbmenu.AppendLine("<div class=\"accordionHeader\">");
                sbmenu.AppendLine("<h2><span>Folder</span>" + group.MenuText + "</h2>");
                sbmenu.AppendLine("</div>");

                sbmenu.AppendLine("<div class=\"accordionContent\">");
                sbmenu.AppendLine("    <ul class=\"tree treeFolder\">");
                foreach (AdminMenu m in group.ChildMenus)
                {
                    sbmenu.AppendLine(PopulateMenu(m));
                }
                sbmenu.AppendLine("    </ul>");
                sbmenu.AppendLine("</div>");
            }
        }

        string PopulateMenu(AdminMenu menu)
        {
            StringBuilder sb = new StringBuilder();

            if (menu.ChildMenus == null || menu.ChildMenus.Count == 0)
            {
                string url = "";
                if (!string.IsNullOrEmpty(menu.MenuUrl))
                {
                    url = menu.MenuUrl;
                }
                else if(!string.IsNullOrEmpty(menu.Page))
                {
                    url = Html.HrefLink(menu.Module, menu.Page, menu.Parameters);
                }
                else
                {
                    url = "javascript:void(0);";
                }

                sb.AppendLine("<li><a href=\"" + url + "\" target=\"navTab\" rel=\"page" + menu.AdminMenuId.ToString() + "\">" + menu.MenuText + "</a></li>");
            }
            else
            {
                sb.AppendLine("<li><a href=\"javascript:void(0)\">" + menu.MenuText + "</a>");
                sb.AppendLine("    <ul>");
                foreach (AdminMenu m in menu.ChildMenus)
                {
                    sb.AppendLine(PopulateMenu(m));
                }
                sb.AppendLine("    </ul>");
                sb.AppendLine("</li>");
            }

            return sb.ToString();
        }
    }
}
