using System;
using System.Collections.Generic;
using Local;

namespace Easpnet
{
    /// <summary>
    /// 查询生成器
    /// </summary>
    [Serializable]    
    public class Query : QueryConditionGroup
    {
        /// <summary>
        /// 排序顺序：
        /// 例如， AddTime desc, 为时间降序排列
        /// </summary>
        public QueryOrder OrderByType { get; set; }

        /// <summary>
        /// 分组方式
        /// </summary>
        public List<string> GroupByColumns { get; set; }

        /// <summary>
        /// 要查询的列集合
        /// </summary>
        public List<QuerySelectColumn> SelectColumns { get; set; }

        /// <summary>
        /// 构造一个查询生成器实例
        /// </summary>
        protected Query()
            : base()
        {
            this.Ralation = Ralation.End;
        }


        /// <summary>
        /// 创建一个查询条件
        /// </summary>
        /// <returns></returns>
        public static Query NewQuery()
        {
            Query q = new Query();
            switch (LocalConfig.DatabaseType)
            {
                case "Sql":
                case "Sql2K":
                    q = new Sql.SqlQuery();
                    break;

            }

            return q;
        }
        

        /// <summary>
        /// 清空条件
        /// </summary>
        public void Clear()
        {
            if (OrderByType != null && OrderByType.Fields != null && OrderByType.Fields.Count > 0)
            {
                OrderByType.Fields.Clear();
            }

            if (Conditions != null && Conditions.Count > 0)
            {
                Conditions.Clear();
            }

            if (SelectColumns != null)
            {
                SelectColumns.Clear();
            }

            if (GroupByColumns != null && GroupByColumns.Count > 0)
            {
                GroupByColumns.Clear();
            }
        }


        /// <summary>
        /// 构造查询列
        /// </summary>
        /// <param name="column_name">查询的列</param>
        /// <param name="aggregate_type">聚合种类</param>
        public Query Select(string column_name, AggregateType aggregate_type, string column_as)
        {
            if (this.SelectColumns == null)
            {
                this.SelectColumns = new List<QuerySelectColumn>();
            }
            
            //添加查询列
            this.SelectColumns.Add(new QuerySelectColumn(column_name, aggregate_type, column_as));
            return this;
        }

        /// <summary>
        /// 构造查询列
        /// </summary>
        /// <param name="column_name">查询的列</param>
        public Query Select(string column_name)
        {
            if (this.SelectColumns == null)
            {
                this.SelectColumns = new List<QuerySelectColumn>();
            }

            //添加查询列
            this.SelectColumns.Add(new QuerySelectColumn(column_name));
            return this;
        }


        /// <summary>
        /// 构造查询列，可构造多个
        /// </summary>
        /// <param name="column_name"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public Query Select(string column_name, params string[] columns)
        {
            if (this.SelectColumns == null)
            {
                this.SelectColumns = new List<QuerySelectColumn>();
            }

            //添加查询列
            this.SelectColumns.Add(new QuerySelectColumn(column_name));

            foreach (string s in columns)
            {
                this.SelectColumns.Add(new QuerySelectColumn(s));
            }

            return this;
        }

        /// <summary>
        /// 构造查询列
        /// </summary>
        /// <param name="column_name">查询的列</param>
        /// <param name="aggregate_type">聚合种类</param>
        public Query Select(int column_index, AggregateType aggregate_type, string column_as)
        {
            if (this.SelectColumns == null)
            {
                this.SelectColumns = new List<QuerySelectColumn>();
            }

            //添加查询列
            this.SelectColumns.Add(new QuerySelectColumn(column_index, aggregate_type, column_as));
            return this;
        }

        /// <summary>
        /// 分组类型
        /// </summary>
        /// <param name="groupByType"></param>
        /// <returns></returns>
        public Query GroupBy(string groupByType)
        {
            if (GroupByColumns == null)
            {
                GroupByColumns = new List<string>();
            }

            GroupByColumns.Add(groupByType);
            return this;
        }


        /// <summary>
        /// 构造排序方式
        /// </summary>
        /// <param name="_fieldName">字段名称</param>
        /// <param name="_orderType">排序方式</param>
        public Query OrderBy(string _fieldName, OrderType _orderType)
        {
            if (OrderByType == null)
            {
                OrderByType = new QueryOrder();
            }

            OrderByType.Fields.Add(new QueryOrderField(_fieldName, _orderType));
            return this;
        }


        /// <summary>
        /// 构造排序方式-默认为升序
        /// </summary>
        /// <param name="_fieldName">字段名称</param>
        public Query OrderBy(string _fieldName)
        {
            if (OrderByType == null)
            {
                OrderByType = new QueryOrder();
            }

            OrderByType.Fields.Add(new QueryOrderField(_fieldName, OrderType.ASC));
            return this;
        }


        /// <summary>
        /// 构造查询条件。
        /// </summary>
        /// <param name="pColumnName"></param>
        /// <param name="pSymbol"></param>
        /// <param name="pRalation"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Query Where(string pColumnName, Symbol pSymbol, Ralation pRalation, params object[] args)
        {
            if (this.Conditions == null)
            {
                this.Conditions = new List<QueryConditionBase>();
            }

            QueryCondition condition = CreateCondition(pColumnName, pSymbol, pRalation, args);
            this.Conditions.Add(condition);
            return this;
        }


        /// <summary>
        /// 构造查询条件。
        /// </summary>
        /// <param name="pColumnName"></param>
        /// <param name="symbol"></param>
        /// <param name="pRalation"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Query Where(string pColumnName, String symbol, Ralation pRalation, params object[] args)
        {
            if (this.Conditions == null)
            {
                this.Conditions = new List<QueryConditionBase>();
            }

            Symbol pSymbol = Symbol.EqualTo;
            switch (symbol)
            {
                case "=": pSymbol = Symbol.EqualTo;break;
                case ">": pSymbol = Symbol.GreaterThan; break;
                case ">=": pSymbol = Symbol.GreaterThanOrEqualTo; break;
                case "<": pSymbol = Symbol.LessThan; break;
                case "<=": pSymbol = Symbol.LessThanOrEqualTo; break;
                case "<>": pSymbol = Symbol.NotEqualTo; break;
                case "like": pSymbol = Symbol.Like; break;
                case "!like": pSymbol = Symbol.NotLike; break;
                case "null": pSymbol = Symbol.IsNull; break;
                case "!null": pSymbol = Symbol.IsNotNull; break; 
                case "between": pSymbol = Symbol.Between; break;
                case "!between": pSymbol = Symbol.NotBetween; break;
                case "in": pSymbol = Symbol.In; break;
                case "!in": pSymbol = Symbol.NotIn; break;
                default:throw new Exception("符号【"+ symbol + "】不支持");
            }

            QueryCondition condition = CreateCondition(pColumnName, pSymbol, pRalation, args);
            this.Conditions.Add(condition);
            return this;
        }


        /// <summary>
        /// 添加查询条件“在……之中”
        /// </summary>
        /// <param name="pColumnName"></param>
        /// <param name="pRalation"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Query In(string pColumnName, Ralation pRalation, object[] args)
        {
            if (this.Conditions == null)
            {
                this.Conditions = new List<QueryConditionBase>();
            }

            QueryCondition condition = CreateConditionValueAsArray(pColumnName, Symbol.In, pRalation, args);
            this.Conditions.Add(condition);
            return this;
        }

        /// <summary>
        /// 添加查询条件“不在在……之中”
        /// </summary>
        /// <param name="pColumnName"></param>
        /// <param name="pRalation"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Query NotIn(string pColumnName, Ralation pRalation, object[] args)
        {
            if (this.Conditions == null)
            {
                this.Conditions = new List<QueryConditionBase>();
            }

            QueryCondition condition = CreateConditionValueAsArray(pColumnName, Symbol.NotIn, pRalation, args);
            this.Conditions.Add(condition);
            return this;
        }

        /// <summary>
        /// 添加查询条件“在……之间”
        /// </summary>
        /// <param name="pColumnName"></param>
        /// <param name="pRalation"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public Query Between(string pColumnName, Ralation pRalation, object arg1, object arg2)
        {
            if (this.Conditions == null)
            {
                this.Conditions = new List<QueryConditionBase>();
            }

            QueryCondition condition = CreateCondition(pColumnName, Symbol.Between, pRalation, arg1, arg2);
            this.Conditions.Add(condition);
            return this;
        }


        /// <summary>
        /// 根据指定的参数创建条件实例
        /// </summary>
        /// <param name="pColumnName"></param>
        /// <param name="pSymbol"></param>
        /// <param name="pRalation"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static QueryCondition CreateCondition(string pColumnName, Symbol pSymbol, Ralation pRalation, params object[] args)
        {
            QueryCondition q = CreateCondition();
            q.ColumnName = pColumnName;
            q.Symbol = pSymbol;
            q.Ralation = pRalation;
            q.Values = new List<object>();
            foreach (object obj in args)
            {
                q.Values.Add(obj);
            }
            return q;
        }

        /// <summary>
        /// 根据指定的参数创建条件实例
        /// </summary>
        /// <param name="pColumnName"></param>
        /// <param name="pSymbol"></param>
        /// <param name="pRalation"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static QueryCondition CreateConditionValueAsArray(string pColumnName, Symbol pSymbol, Ralation pRalation, object[] args)
        {
            QueryCondition q = CreateCondition();
            q.ColumnName = pColumnName;
            q.Symbol = pSymbol;
            q.Ralation = pRalation;
            q.Values = new List<object>();
            foreach (object obj in args)
            {
                q.Values.Add(obj);
            }
            return q;
        }


        /// <summary>
        /// 根据指定的参数创建条件实例
        /// </summary>
        /// <param name="pColumnName"></param>
        /// <param name="pSymbol"></param>
        /// <param name="pRalation"></param>
        /// <param name="args0"></param>
        /// <returns></returns>
        public static QueryCondition CreateCondition(string pColumnName, Symbol pSymbol, Ralation pRalation, object args0)
        {
            QueryCondition q = CreateCondition();
            q.ColumnName = pColumnName;
            q.Symbol = pSymbol;
            q.Ralation = pRalation;
            q.Values = new List<object>();
            q.Values.Add(args0);
            return q;
        }


        /// <summary>
        /// 根据指定的参数创建条件实例
        /// </summary>
        /// <param name="pColumnName"></param>
        /// <param name="pSymbol"></param>
        /// <param name="pRalation"></param>
        /// <param name="args0"></param>
        /// <param name="args1"></param>
        /// <returns></returns>
        public static QueryCondition CreateCondition(string pColumnName, Symbol pSymbol, Ralation pRalation, object args0, object args1)
        {
            QueryCondition q = CreateCondition();
            q.ColumnName = pColumnName;
            q.Symbol = pSymbol;
            q.Ralation = pRalation;
            q.Values = new List<object>();
            q.Values.Add(args0);
            q.Values.Add(args1);
            return q;
        }

        /// <summary>
        /// 获取查询列
        /// </summary>
        /// <returns></returns>
        public virtual string GenerateSelectColumnString()
        {            
            if (this.SelectColumns.Count > 0)
            {
                string s = "";
                for (int i = 0; i < SelectColumns.Count; i++)
                {
                    QuerySelectColumn col = SelectColumns[i];
                    if (i > 0)
                    {
                        s += ",";
                    }

                    string columns_name = col.ColumnName;
                    //如果是列名，则加上[]符号
                    if (col.ColumnType == QuerySelectColumnType.ColumnName)
                    {
                        columns_name = "[" + col.ColumnName + "]";
                    }

                    //一般
                    if (col.AggregateType == AggregateType.None)
                    {
                        s += columns_name;
                    }
                    //聚合函数
                    else
                    {
                        switch (col.AggregateType)
                        {
                            case AggregateType.AVG:
                                s += "AVG(" + columns_name + ")";
                                break;
                            case AggregateType.COUNT:
                                s += "COUNT(" + columns_name + ")";
                                break;
                            case AggregateType.MAX:
                                s += "MAX(" + columns_name + ")";
                                break;
                            case AggregateType.MIN:
                                s += "MIN(" + columns_name + ")";
                                break;
                            case AggregateType.SUM:
                                s += "SUM(" + columns_name + ")";
                                break;
                            default:
                                s += columns_name;
                                break;
                        }
                    }

                    //AS
                    if (!string.IsNullOrEmpty(col.ColumnAs))
                    {
                        s += " " + col.ColumnAs;
                    }

                }

                //
                return s;
            }
            else
            {
                return " * ";
            }
        }


        /// <summary>
        /// 获取分组依据字符串
        /// </summary>
        /// <returns></returns>
        public virtual string GenerateGroupByString()
        {
            return "";
        }

        /// <summary>
        /// 获取排序列字符串
        /// </summary>
        /// <returns></returns>
        public virtual string GenerateOrderByString()
        {
            return "";
        }

        /// <summary>
        /// 创建条件实例
        /// </summary>
        /// <returns></returns>
        private static QueryCondition CreateCondition()
        {
            QueryCondition q = null;
            switch (LocalConfig.DatabaseType)
            {
                case "Sql":
                    q = new Sql.SqlQueryCondition();
                    break;
                case "Sql2K":
                    q = new Sql2K.SqlQueryCondition();
                    break;
            }

            return q;
        }
    }
}
