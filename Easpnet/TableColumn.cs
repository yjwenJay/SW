using System.Data;

namespace Easpnet
{
    public class TableColumn
    {
        /// <summary>
        /// 实体类中的字段名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 表中字段名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public DbType DbType { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 是否是标志字段
        /// </summary>
        public bool IsIdentifier { get; set; }
    }
}
