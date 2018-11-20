using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Sql2K
{
    public class SqlQuery : Query
    {
        /// <summary>
        /// 获取查询选择的列
        /// </summary>
        /// <returns></returns>
        public override string GenerateSelectColumnString()
        {
            string sql = "";
            if (SelectColumns == null || SelectColumns.Count == 0)
            {
                sql += " * ";
            }
            else
            {
                int i = 0;
                foreach (QuerySelectColumn item in SelectColumns)
                {
                    if (i > 0)
                    {
                        sql += ",";
                    }

                    //
                    switch (item.AggregateType)
                    {
                        case AggregateType.None:
                            sql += "[" + item.ColumnName + "]";
                            break;
                        case AggregateType.AVG:
                            sql += "AVG([" + item.ColumnName + "])";
                            break;
                        case AggregateType.COUNT:
                            sql += "COUNT(0)";
                            break;
                        case AggregateType.MAX:
                            sql += "MAX([" + item.ColumnName + "])";
                            break;
                        case AggregateType.MIN:
                            sql += "MIN([" + item.ColumnName + "])";
                            break;
                        case AggregateType.SUM:
                            sql += "SUM([" + item.ColumnName + "])";
                            break;
                        default:
                            break;
                    }

                    //AS
                    if (!string.IsNullOrEmpty(item.ColumnAs))
                    {
                        sql += " AS [" + item.ColumnAs + "]";
                    }

                    //
                    i++;
                }
            }

            return sql;
        }


        /// <summary>
        /// 获取分组依据字符串
        /// </summary>
        /// <returns></returns>
        public override string GenerateGroupByString()
        {
            string sql = "";
            if (GroupByColumns != null && GroupByColumns.Count > 0)
            {
                for (int i = 0; i < GroupByColumns.Count; i++)
                {
                    if (i > 0)
                    {
                        sql += ",";
                    }
                    sql += "[" + GroupByColumns[i] + "]";
                }
            }

            return sql;
        }


        /// <summary>
        /// 获取排序列字符串
        /// </summary>
        /// <returns></returns>
        public override string GenerateOrderByString()
        {
            string sql = "";
            if (OrderByType != null && OrderByType.Fields != null && OrderByType.Fields.Count > 0)
            {
                for (int i = 0; i < OrderByType.Fields.Count; i++)
                {
                    if (i > 0)
                    {
                        sql += ",";
                    }

                    string orderType = OrderByType.Fields[i].OrderType == OrderType.ASC ? "asc" : "desc";
                    sql += "[" + OrderByType.Fields[i].FieldName + "] " + orderType;
                }
            }

            return sql;
        }
    }
}
