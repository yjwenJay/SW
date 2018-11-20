using System.Collections.Generic;
using System.Data;

namespace Easpnet
{
    /// <summary>
    /// 描述存储过程的参数
    /// </summary>
    public class ProcedureParameter
    {
        /// <summary>
        /// 参数的名称
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType DbType { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 位数
        /// </summary>
        public byte Precision { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public byte Scale { get; set; }

        /// <summary>
        /// 参数的值
        /// </summary>
        public object Value { get; set; }


        /// <summary>
        /// 构造方法
        /// </summary>
        public ProcedureParameter()
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="parameterName">参数的名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">大小</param>
        /// <param name="value">参数的值</param>
        public ProcedureParameter(string parameterName, DbType dbType, int size, object value)
        {
            ParameterName = parameterName;
            DbType = dbType;
            Size = size;
            Value = value;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="parameterName">参数的名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">大小</param>
        /// <param name="value">参数的值</param>
        public ProcedureParameter(string parameterName, DbType dbType, int size)
        {
            ParameterName = parameterName;
            DbType = dbType;
            Size = size;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="parameterName">参数的名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">大小</param>
        /// <param name="value">参数的值</param>
        public ProcedureParameter(string parameterName, DbType dbType, byte precision, byte scale)
        {
            ParameterName = parameterName;
            DbType = dbType;
            Precision = precision;
            Scale = scale;
            Size = 0;
        }


    }


    /// <summary>
    /// 存储过程参数的集合
    /// </summary>
    public class ProcedureParameterCollection : List<ProcedureParameter>
    {
        public ProcedureParameter this[string s]
        {
            get
            {
                ProcedureParameter p = this.Find(f => f.ParameterName == s);
                return p == null ? new ProcedureParameter() : p;
            }
        }

    }
}
