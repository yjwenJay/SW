using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Modules.Models;
using Easpnet.Modules.Member.Models;

namespace Easpnet.Modules.Member.Pages
{
    public class AdminUserPermissions : PageBase
    {
        protected StringBuilder tree;
        List<AdminMenu> menus;
        List<AdminMenu> all_menus;
        protected AdminUser user;
        protected string back_url = "";
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            back_url = Html.HrefLink("Member", "AdminEmployeeManage");

            //
            if (string.IsNullOrEmpty(Get("user_id")))
            {
                Response.Redirect(back_url);
            }

            //获取用户信息，若没有获取到，则跳转到用户列表页
            user = new AdminUser();
            user.UserId = TypeConvert.ToInt64(Get("user_id"));
            if (!user.GetModel() || user.UserType != UserType.Employee)
            {
                Response.Redirect(back_url);
            }

            //数据库中的菜单。
            menus = AdminMenu.GetMenuTree();

            //处理请求
            Handler();

            //// 获取树形控件 ////
            tree = new StringBuilder();
            tree.Append("<div class=\"dhtmlxTree\" id=\"treeboxbox_tree\" setImagePath=\"" + Html.Url("Static/js/dhtmlxTree/imgs/csh_vista/")
                + "\" enableCheckBoxes=\"1\">");
            tree.Append("<ul>");

            foreach (AdminMenu root in menus)
            {
                PopulateTree(root);
            }

            tree.Append("</ul>");
            tree.Append("</div>");

            
        }


        /// <summary>
        /// 处理请求
        /// </summary>
        void Handler()
        {
            if (Post("option") == "save")
            {
                //所有的权限列表
                all_menus = new List<AdminMenu>();
                List<AdminMenu> ls = new AdminMenu().GetList<AdminMenu>();
                foreach (ModelBase item in ls)
                {
                    all_menus.Add(item as AdminMenu);
                }

                //
                string values = Post("values");
                string[] arr = values.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries);

                //构造权限列表
                List<Permission> lst = new List<Permission>();
                foreach (string str in arr)
                {
                    AdminMenu adminMenu = all_menus.Find(s => s.AdminMenuId.ToString() == str);
                    if (adminMenu != null)
                    {
                        Permission per = new Permission();
                        per.Module = adminMenu.Module;
                        per.PageName = adminMenu.Page;
                        per.PermissionId = adminMenu.AdminMenuId;
                        lst.Add(per);
                    }
                }

                //更新权限
                if (AdminUser.UpdatePermission(user.UserId, lst))
                {
                    Response.Redirect(back_url);
                }
                else
                {
                    AddErrorMessage("对不起，操作失败，请重试！");
                }
            }
        }

        /// <summary>
        /// 构造树形控件
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        void PopulateTree(AdminMenu mm)
        {

            //是否选中
            string check = "";
            if (user.Permissions != null && user.Permissions.Exists(s=>s.PermissionId == mm.AdminMenuId))
            {
                check = "checked=\"checked\" ";
            }

            tree.Append("<li " + check + " id=\"" + mm.AdminMenuId + "\"> " + mm.MenuText);

            if (mm.ChildMenus.Count > 0)
            {
                tree.Append("<ul>");
                foreach (AdminMenu menu in mm.ChildMenus)
                {
                    PopulateTree(menu);
                }
                tree.Append("</ul>");
            }

            tree.Append("</li>");
        }
    }
}
