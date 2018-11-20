using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.Commons
{
    [Serializable]
    /// <summary>
    /// 条件之间的关系
    /// </summary>
    public enum Ralation
    {
        /// <summary>
        /// 并且
        /// </summary>
        And = 1,
        /// <summary>
        /// 或者
        /// </summary>
        Or = 2,
        /// <summary>
        /// 结束
        /// </summary>
        End = 3
    }
}
