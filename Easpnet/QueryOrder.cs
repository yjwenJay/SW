using System;
using System.Collections.Generic;

namespace Easpnet
{
    /// <summary>
    /// 查询排序
    /// </summary>
    public class QueryOrder
    {
        public List<QueryOrderField> Fields { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public QueryOrder()
        {
            Fields = new List<QueryOrderField>();
        }


        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="fields"></param>
        public QueryOrder(params QueryOrderField[] fields)
        {
            Fields = new List<QueryOrderField>();
            foreach (QueryOrderField item in fields)
            {
                Fields.Add(item);
            }
        }


    }


    /// <summary>
    /// 排序的字段
    /// </summary>
    [Serializable]
    public class QueryOrderField
    {
        /// <summary>
        /// 排序的字段
        /// </summary>
        public string FieldName { get; set; }
        public OrderType OrderType { get; set; }


        /// <summary>
        /// 构造排序方式
        /// </summary>
        /// <param name="_fieldName">字段名称</param>
        /// <param name="_orderType">排序方式</param>
        public QueryOrderField(string _fieldName, OrderType _orderType)
        {
            FieldName = _fieldName;
            OrderType = _orderType;
        }

        /// <summary>
        /// 构造排序方式，默认为升序排列
        /// </summary>
        /// <param name="_fieldName">字段名称</param>
        public QueryOrderField(string _fieldName)
        {
            FieldName = _fieldName;
            OrderType = OrderType.ASC;
        }
    }

    /// <summary>
    /// 排序方式
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 升序排列
        /// </summary>
        ASC,
        /// <summary>
        /// 降序排列
        /// </summary>
        DESC
    }
}
