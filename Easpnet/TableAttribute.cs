using System;

namespace Easpnet.Modules
{
    /// <summary>
    /// 表属性
    /// </summary>
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 主键列，若多个列构成主键，则列以","号隔开
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// 是否启用缓存依赖
        /// </summary>
        public bool EnableCacheDependency { get; set; }

        public TableAttribute()
        { 
            
        }


        /// <summary>
        /// 默认不启用缓存依赖
        /// </summary>
        /// <param name="_TableName"></param>
        /// <param name="_PrimaryKey"></param>
        public TableAttribute(string _TableName, string _PrimaryKey)
        {
            TableName = _TableName;
            PrimaryKey = _PrimaryKey;
            EnableCacheDependency = false;
        }

        /// <summary>
        /// 构造实例
        /// </summary>
        /// <param name="_TableName"></param>
        /// <param name="_PrimaryKey"></param>
        public TableAttribute(string _TableName, string _PrimaryKey, bool _EnableCacheDependency)
        {
            TableName = _TableName;
            PrimaryKey = _PrimaryKey;
            EnableCacheDependency = _EnableCacheDependency;
        }

        /// <summary>
        /// 默认不启用缓存依赖
        /// </summary>
        /// <param name="_PrimaryKey"></param>
        public TableAttribute(string _PrimaryKey)
        {
            PrimaryKey = _PrimaryKey;
            EnableCacheDependency = false;
        }

        /// <summary>
        /// 默认不启用缓存依赖
        /// </summary>
        /// <param name="_PrimaryKey"></param>
        public TableAttribute(string _PrimaryKey, bool _EnableCacheDependency)
        {
            PrimaryKey = _PrimaryKey;
            EnableCacheDependency = _EnableCacheDependency;
        }
    }
}
