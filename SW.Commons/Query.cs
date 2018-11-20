using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.Commons
{
    [Serializable]
    /// <summary>
    /// 查询生成器
    /// </summary>
    public class Query : ConditionGroup
    {
        /// <summary>
        /// 排序顺序：
        /// 例如， AddTime desc, 为时间降序排列
        /// </summary>
        public string OrderBy { get; set; }

        public Query()
            : base()
        {
            this.Ralation = Ralation.End;
        }
    }
}
