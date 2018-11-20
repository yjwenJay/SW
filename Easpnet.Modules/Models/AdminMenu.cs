using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Easpnet.Modules.Models
{
    /// <summary>
    /// 管理员菜单
    /// </summary>
    [Table(PrimaryKey = "AdminMenuId", TableName = "CoreAdminMenu")]
    public class AdminMenu : ModelBase
    {
        /// <summary>
        /// 管理员菜单id
        /// </summary>
        [TableField(IsTableField = true, IsIdentifier = true)]
        public long AdminMenuId { get; set; }
        /// <summary>
        /// 菜单文本描述
        /// </summary>
        [TableField(50)]
        public string MenuText { get; set; }
        /// <summary>
        /// 菜单链接，若该项不为空，则该链接生效
        /// </summary>
        [TableField(255)]
        public string MenuUrl { get; set; }
        /// <summary>
        /// 目标框架，若该字段值为空，则为默认框架navTab
        /// </summary>
        [TableField(50)]
        public string Target { get; set; }
        /// <summary>
        /// 父菜单Id，若为0，则为分组菜单
        /// </summary>
        [TableField]
        public long ParentId { get; set; }
        /// <summary>
        /// 菜单排序顺序
        /// </summary>
        [TableField]
        public long SortOrder { get; set; }
        /// <summary>
        /// 所属的模块
        /// </summary>
        [TableField(50)]
        public string Module { get; set; }
        /// <summary>
        /// 页面名称
        /// </summary>
        [TableField(255)]
        public string Page { get; set; }
        /// <summary>
        /// 包含的参数
        /// </summary>
        [TableField(255)]
        public string Parameters { get; set; }
        /// <summary>
        /// 是否显示菜单
        /// </summary>
        [TableField]
        public bool DisaplayMenu { get; set; }
        /// <summary>
        /// 是否是核心管理(网站开发人员使用的功能)
        /// </summary>
        [TableField]
        public bool IsCoreManage { get; set; }
        /// <summary>
        /// 子菜单列表
        /// </summary>
        public List<AdminMenu> ChildMenus { get; set; }


        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public static List<AdminMenu> GetMenuTree(long parentId)
        {
            List<AdminMenu> lstMenu = GetAllMenu().FindAll(s=>s.ParentId == parentId);
            foreach (AdminMenu menu in lstMenu)
            {
                menu.ChildMenus = GetMenuTree(menu.AdminMenuId);
            }
            return lstMenu;
        }


        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public static List<AdminMenu> GetMenuTree()
        {
            return GetMenuTree(0);
        }


        /// <summary>
        /// 获取所有的菜单
        /// </summary>
        /// <returns></returns>
        public static List<AdminMenu> GetAllMenu()
        {
            AdminMenu m = new AdminMenu();
            string cachname = "GetAllMenu" + m.GetType().AssemblyQualifiedName;
            List<AdminMenu> lstMenu = HttpRuntime.Cache.Get(cachname) as List<AdminMenu>;

            if (lstMenu == null)
            {
                lstMenu = new List<AdminMenu>();
                Query q = Query.NewQuery();
                q.OrderBy("SortOrder");
                List<AdminMenu> lst = new AdminMenu().GetList<AdminMenu>(q);                
                foreach (ModelBase item in lst)
                {
                    AdminMenu menu = item as AdminMenu;
                    lstMenu.Add(menu);
                }

                HttpRuntime.Cache.Add(cachname, lstMenu, 
                    null, DateTime.Now.AddHours(1.0), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
            
            return lstMenu;
        }
    }
}
