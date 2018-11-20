using System;

namespace Easpnet
{
    [Serializable]
    /// <summary>
    /// 条件基类,子类可以为条件,或者条件组
    /// </summary>
    public class QueryConditionBase
    {
        /// <summary>
        /// 该条件和下一个条件之间的关系
        /// </summary>
        public Ralation Ralation { get; set; }

        /// <summary>
        /// 转化为Sql查询字符串
        /// </summary>
        /// <returns></returns>
        public virtual string GenerateQueryString()
        {
            return null;
        }
    }

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
