using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.News.Models
{
    /// <summary>
    /// 新闻分类
    /// </summary>
    [Table(PrimaryKey = "CategoryId", TableName = "NewsCategory")]
    public class Category : ModelBase
    {
        /// <summary>
        /// 新闻分类id
        /// </summary>
        [TableField(IsTableField = true, IsIdentifier = true)]
        public long CategoryId { get; set; }
        /// <summary>
        /// 新闻分类名称
        /// </summary>
        [TableField(50)]
        public string CategoryName { get; set; }
        /// <summary>
        /// 父分类Id
        /// </summary>
        [TableField]
        public long ParentId { get; set; }
        /// <summary>
        /// 用于SEO Url的名称如：体育新闻可命名为sports
        /// </summary>
        [TableField(50)]
        public string SeoName { get; set; }
        /// <summary>
        /// 排序有限顺序的数字
        /// </summary>
        [TableField]
        public int SortOrder { get; set; }
        /// <summary>
        /// 区块名称
        /// </summary>
        [TableField(255)]
        public string MetaKeywords { get; set; }
        /// <summary>
        /// 区块名称
        /// </summary>
        [TableField(255)]
        public string MetaDescription { get; set; }
    }
}
