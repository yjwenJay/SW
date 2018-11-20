using System;
using System.Collections.Generic;

namespace Easpnet
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
            IsSimplePage = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="statitics"></param>
        public PageParam(int index, int size, string statitics)
        {
            Index = index;
            Size = size;
            StatisticFields = statitics;
            IsSimplePage = false;
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
        /// 是否简单分页（设为此项，则不进行总记录数的统计）
        /// </summary>
        public bool IsSimplePage { get; set; }

        /// <summary>
        /// 附加的统计列
        /// </summary>
        public string StatisticFields { get; set; }

        /// <summary>
        /// 附加的统计结果
        /// </summary>
        public Dictionary<string, object> StatisticResult { get; set; }
    }
}
