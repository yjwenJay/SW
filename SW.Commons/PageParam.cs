﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW
{
    /// <summary>
    /// 结构体Pager:封装分页查询的相关参数
    /// </summary>
    /// 
    [Serializable]
    public class PageParam
    {
        public PageParam()
        {

        }

        public PageParam(int index, int size)
        {
            Index = index;
            Size = size;
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int Index;
        /// <summary>
        /// 页大小
        /// </summary>
        public int Size;
        /// <summary>
        /// 总记录数,作为传出参数
        /// </summary>
        public int Total;
        /// <summary>
        /// 总页数,作为传出参数
        /// </summary>
        public int Count;
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy;
        /// <summary>
        /// 排序类型 asc / desc
        /// </summary>
        public string OrderType;
    }
}
