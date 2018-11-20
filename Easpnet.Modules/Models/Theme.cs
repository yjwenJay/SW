using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Easpnet.Modules.Models
{
    /// <summary>
    /// 网站外观主题
    /// </summary>
    [Table(PrimaryKey = "ThemeId", TableName = "CoreTheme", EnableCacheDependency = true)]
    public class Theme : ModelBase
    {
        /// <summary>
        /// 配置id
        /// </summary>
        [TableField(IsTableField = true, IsIdentifier = true)]
        public long ThemeId { get; set; }
        /// <summary>
        /// 主题名称
        /// </summary>
        [TableField(50)]
        public string ThemeName { get; set; }
        /// <summary>
        /// 主题描述
        /// </summary>
        [TableField(2000)]
        public string Description { get; set; }
        /// <summary>
        /// 是否是当前主题
        /// </summary>
        [TableField]
        public bool IsCurrentTheme { get; set; }
        /// <summary>
        /// 效果图
        /// </summary>
        [TableField(255)]
        public string Snapshot { get; set; }
        /// <summary>
        /// 核心框架
        /// </summary>
        [TableField(50)]
        public string CoreVersion { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        [TableField(50)]
        public string Author { get; set; }
        /// <summary>
        /// 模板目录名称
        /// </summary>
        [TableField(255)]
        public string DirectoryName { get; set; }
        /// <summary>
        /// 是否开放
        /// </summary>
        [TableField]
        public bool IsOpen { get; set; }
        /// <summary>
        /// 商户Id-指定为某一商户定制的模板
        /// </summary>
        [TableField]
        public long MerchantId { get; set; }

        /// <summary>
        /// 判断是否重复(目录名称唯一)
        /// </summary>
        /// <param name="dirname"></param>
        /// <returns></returns>
        public bool Exists()
        {
            Query q = Query.NewQuery();
            q.Where("DirectoryName", Symbol.EqualTo, Ralation.End, DirectoryName);
            return Count(q) > 0;
        }


        /// <summary>
        /// 获取当前的模板
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentTheme()
        {
            Theme theme = new Theme();
            string cachname = "GetCurrentTheme" + theme.GetType().AssemblyQualifiedName;
            string themename = HttpRuntime.Cache.Get(cachname) as string;

            if (string.IsNullOrEmpty(themename))
            {
                Query q = Query.NewQuery();
                q.Select("DirectoryName");
                q.Where("IsCurrentTheme", Symbol.EqualTo, Ralation.End, true);

                if (theme.GetModel(q))
                {
                    themename = theme.DirectoryName;
                }
                else
                {
                    themename = "Classic";
                } 

                HttpRuntime.Cache.Add(cachname, themename, theme.Dependency, DateTime.Now.AddHours(1.0), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }

            return themename;
        }
    }
}
