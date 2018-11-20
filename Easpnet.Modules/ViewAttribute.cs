using System;

namespace Easpnet.Modules
{
    public class ViewAttribute : Attribute
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// 主键列，若多个列构成主键，则列以","号隔开
        /// </summary>
        public string PrimaryKey { get; set; }

        public ViewAttribute()
        { 
            
        }

        public ViewAttribute(string _ViewName, string _PrimaryKey)
        {
            ViewName = _ViewName;
            PrimaryKey = _PrimaryKey;
        }

        public ViewAttribute(string _PrimaryKey)
        {
            PrimaryKey = _PrimaryKey;
        }
    }
}
