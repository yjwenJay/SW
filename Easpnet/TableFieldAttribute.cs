using System;

namespace Easpnet.Modules
{
    public class TableFieldAttribute : Attribute
    {
        public bool IsTableField { get; set; }
        public string TableFieldName { get; set; }

        /// <summary>
        /// 是否为标识列
        /// </summary>
        public bool IsIdentifier { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int Size { get; set; }

        public TableFieldAttribute()
        {
            IsTableField = true;
        }


        /// <summary>
        /// 构造函数，指定是否为自增的标志。
        /// </summary>
        /// <param name="isIdentifier"></param>
        public TableFieldAttribute(bool isIdentifier)
        {
            IsTableField = true;
            IsIdentifier = isIdentifier;
        }

        /// <summary>
        /// 构造属性
        /// </summary>
        /// <param name="_IsTableField">是否是表格的字段</param>
        /// <param name="_TableFieldName">表格字段的名称</param>
        public TableFieldAttribute(string _TableFieldName)
        {
            IsTableField = true;
            TableFieldName = _TableFieldName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IsTableField"></param>
        public TableFieldAttribute(int _Size)
        {
            IsTableField = true;
            Size = _Size;
        }



    }
}
