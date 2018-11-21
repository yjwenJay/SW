using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW
{
    [Serializable]
    /// <summary>
    /// 条件基类,子类可以为条件,或者条件组
    /// </summary>
    public abstract class ConditionBase
    {
        /// <summary>
        /// 该条件和下一个条件之间的关系
        /// </summary>
        public Ralation Ralation { get; set; }

        /// <summary>
        /// 转化为Sql查询字符串
        /// </summary>
        /// <returns></returns>
        public abstract string GenerateQueryString();
    }
}
