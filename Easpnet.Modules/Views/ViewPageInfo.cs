using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Web;
using System.Web.Caching;

namespace Easpnet.Modules.Views
{
    /// <summary>
    /// 页面
    /// </summary>
    [Table(PrimaryKey = "PageId", TableName = "ViewCorePageInfo")]
    public class ViewPageInfo : Models.PageInfo
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        [TableField(255)]
        public string AssemblyName { get; set; }


        /// <summary>
        /// 获取所有的页面信息
        /// </summary>
        /// <returns></returns>
        public List<ViewPageInfo> GetCachedList()
        {
            //
            string _cacheName = this.GetType().AssemblyQualifiedName;

            //
            List<ViewPageInfo> lst = HttpRuntime.Cache.Get(_cacheName) as List<ViewPageInfo>;

            if (lst == null)
            {
                List<ViewPageInfo> lst_base = GetList<ViewPageInfo>();
                foreach (ModelBase md in lst_base)
                {
                    lst.Add(md as ViewPageInfo);
                }

                //                
                HttpRuntime.Cache.Add(_cacheName, lst, null, DateTime.Now.AddHours(1.0), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
            
            return lst;
        }

        
    }
}
