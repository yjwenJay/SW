using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW
{
    [Serializable]
    /// <summary>
    /// 符号
    /// </summary>
    public enum Symbol
    {
        /// <summary>
        /// 等于
        /// </summary>
        EqualTo = 1,
        /// <summary>
        /// 不等于
        /// </summary>
        NotEqualTo = 2,
        /// <summary>
        /// 小于
        /// </summary>
        LessThan = 3,
        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEqualTo = 4,
        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan = 5,
        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqualTo = 6,
        /// <summary>
        /// 相似
        /// </summary>
        Like = 7,
        /// <summary>
        /// 不相似
        /// </summary>
        NotLike = 8,
        /// <summary>
        /// 为空
        /// </summary>
        IsNull,
        /// <summary>
        /// 不为空
        /// </summary>
        IsNotNull,
        /// <summary>
        /// 在...之间
        /// </summary>
        Between,
        /// <summary>
        /// 不在...之间
        /// </summary>
        NotBetween,
        /// <summary>
        /// 在...之中
        /// </summary>
        In,
        /// <summary>
        /// 不在...之中
        /// </summary>
        NotIn

    }
}
