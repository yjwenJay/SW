using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.News.Models
{
    /// <summary>
    /// 新闻内容
    /// </summary>
    [Table(PrimaryKey = "ArticleId", TableName = "NewsArticle")]
    public class Article : ModelBase
    {
        /// <summary>
        /// 新闻id
        /// </summary>
        [TableField(IsTableField = true, IsIdentifier = true)]
        public long ArticleId { get; set; }
        /// <summary>
        /// 新闻分类id
        /// </summary>
        [TableField]
        public long CategoryId { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        [TableField(255)]
        public string Title { get; set; }
        /// <summary>
        /// 新闻正文
        /// </summary>
        [TableField]
        public string Body { get; set; }
        /// <summary>
        /// 新闻发布时间
        /// </summary>
        [TableField]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 新闻图片
        /// </summary>
        [TableField(255)]
        public string NewsImage { get; set; }
        /// <summary>
        /// Meta关键字
        /// </summary>
        [TableField(255)]
        public string MetaKeywords { get; set; }
        /// <summary>
        /// Meta描述
        /// </summary>
        [TableField(255)]
        public string MetaDescription { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        [TableField(255)]
        public string Tags { get; set; }
    
    }
}
