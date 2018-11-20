using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Forum.Models
{
    [Table(PrimaryKey = "TheedId", TableName = "ForumTheed")]
    public class Theed : ModelBase
    {
        /// <summary>
        /// 新闻id
        /// </summary>
        [TableField(IsTableField = true, IsIdentifier = true)]
        public long TheedId { get; set; }

        [TableField(255)]
        public string Title { get; set; }

        [TableField]
        public string Body { get; set; }

        [TableField]
        public DateTime AddTime { get; set; }
    }
}
