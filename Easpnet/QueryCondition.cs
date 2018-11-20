using System;
using System.Collections.Generic;

namespace Easpnet
{    
    /// <summary>
    /// 查询条件
    /// </summary>
    [Serializable] 
    public class QueryCondition: QueryConditionBase
    {
        /// <summary>
        /// 列名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 符号
        /// </summary>
        public Symbol Symbol { get; set; }
        /// <summary>
        /// 比较的值
        /// </summary>
        public List<object> Values { get; set; }

        /// <summary>
        /// 构造一个查询条件的实例
        /// </summary>
        public QueryCondition()
        { 
        
        }

        /// <summary>
        /// 构造实例,根据指定的参数
        /// </summary>
        /// <param name="pColumnName">数据库列名称</param>
        /// <param name="pSymbol">比较关系</param>
        /// <param name="pValues">值的集合</param>
        /// <param name="pRalation">和下一组条件之间的关系</param>
        public QueryCondition(string pColumnName, Symbol pSymbol, List<object> pValues, Ralation pRalation)
        {
            ColumnName = pColumnName;
            Symbol = pSymbol;
            Values = pValues;
            Ralation = pRalation;
        }


        /// <summary>
        /// 构造实例,根据指定的参数
        /// </summary>
        /// <param name="pColumnName">数据库列名称</param>
        /// <param name="pSymbol">比较关系</param>
        /// <param name="value">值</param>
        /// <param name="pRalation">和下一组条件之间的关系</param>
        public QueryCondition(string pColumnName, Symbol pSymbol, string value, Ralation pRalation)
        {
            ColumnName = pColumnName;
            Symbol = pSymbol;

            //
            List<object> lst = new List<object>();
            lst.Add(value);
            Values = lst;

            //
            Ralation = pRalation;
        }
 


        /// <summary>
        /// 转化为Sql查询字符串
        /// </summary>
        /// <returns></returns>
        public override string GenerateQueryString()
        {
            return base.GenerateQueryString();
        }
    }
}
