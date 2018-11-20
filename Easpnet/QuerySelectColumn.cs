
namespace Easpnet
{
    /// <summary>
    /// 聚合类型。
    /// </summary>
    public enum AggregateType
    { 
        /// <summary>
        /// 非聚合函数
        /// </summary>
        None = 0,
        /// <summary>
        /// 平均值
        /// </summary>
        AVG,
        /// <summary>
        /// 统计数量
        /// </summary>
        COUNT,
        /// <summary>
        /// 求最大值
        /// </summary>
        MAX,
        /// <summary>
        /// 求最小值
        /// </summary>
        MIN,
        /// <summary>
        /// 求和
        /// </summary>
        SUM
    }


    /// <summary>
    /// 列作为聚合函数的参数类型
    /// </summary>
    public enum QuerySelectColumnType
    { 
        /// <summary>
        /// 以索引形式作为聚合函数的参数
        /// </summary>
        ColumnIndex = 0,
        /// <summary>
        /// 以列名作为聚合函数的参数
        /// </summary>
        ColumnName = 1
    }

    /// <summary>
    /// 要查询的列
    /// </summary>
    public class QuerySelectColumn
    {
        /// <summary>
        /// 列作为聚合函数的参数类型
        /// </summary>
        public QuerySelectColumnType ColumnType { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 将选择的列进行的命名：转换为sql语句为： select count(0) as ColumnAs
        /// </summary>
        public string ColumnAs { get; set; }

        /// <summary>
        /// 聚合类型。
        /// </summary>
        public AggregateType AggregateType { get; set; }

        /// <summary>
        /// 构造查询列
        /// </summary>
        /// <param name="column_name">查询的列</param>
        /// <param name="aggregate_type">聚合种类</param>
        public QuerySelectColumn(string column_name, AggregateType aggregate_type, string column_as)
        {
            ColumnType = QuerySelectColumnType.ColumnName;
            ColumnName = column_name;
            AggregateType = aggregate_type;
            ColumnAs = column_as;
        }

        /// <summary>
        /// 构造查询列
        /// </summary>
        /// <param name="column_name">查询的列</param>
        /// <param name="aggregate_type">聚合种类</param>
        public QuerySelectColumn(string column_name)
        {
            ColumnType = QuerySelectColumnType.ColumnName;
            ColumnName = column_name;
            AggregateType = AggregateType.None;
        }

        /// <summary>
        /// 构造查询列
        /// </summary>
        /// <param name="column_name">查询的列</param>
        /// <param name="aggregate_type">聚合种类</param>
        public QuerySelectColumn(int column_index, AggregateType aggregate_type, string column_as)
        {
            ColumnType = QuerySelectColumnType.ColumnIndex;
            ColumnName = column_index.ToString();
            AggregateType = aggregate_type;
            ColumnAs = column_as;
        }



    }
}
