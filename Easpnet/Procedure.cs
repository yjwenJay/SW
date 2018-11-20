using System.Data;
using System.Data.Common;

namespace Easpnet
{
    /// <summary>
    /// 描述存储过程
    /// </summary>
    public class Procedure
    {
        /// <summary>
        /// 数据库事务
        /// </summary>
        internal Transaction DbTransaction { get; set; }
        /// <summary>
        /// 设定的数据库连接
        /// </summary>
        internal System.Data.Common.DbConnection DbConnection { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        internal string DbConnectionString { get; set; }

        /// <summary>
        /// 存储过程名称
        /// </summary>
        public string ProcedureName { get; set; }

        /// <summary>
        /// 输入参数集合
        /// </summary>
        public ProcedureParameterCollection InParameters { get; set; }

        /// <summary>
        /// 输出参数集合
        /// </summary>
        public ProcedureParameterCollection OutParameters { get; set; }

        /// <summary>
        /// 返回值
        /// </summary>
        public object ReturnValue { get; set; }

        /// <summary>
        /// 用指定的存储过程名称构造存储过程的实例
        /// </summary>
        /// <param name="procName"></param>
        public Procedure(string procName)
        {
            this.ProcedureName = procName;
            this.InParameters = new ProcedureParameterCollection();
            this.OutParameters = new ProcedureParameterCollection();
        }

        /// <summary>
        /// 用指定的存储过程名称构造存储过程的实例
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="trans"></param>
        public Procedure(string procName, Transaction trans)
        {
            this.ProcedureName = procName;
            this.InParameters = new ProcedureParameterCollection();
            this.OutParameters = new ProcedureParameterCollection();
            this.DbTransaction = trans;
        }


        /// <summary>
        /// 用指定的存储过程名称构造存储过程的实例
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="conn"></param>
        public Procedure(string procName, DbConnection conn)
        {
            this.ProcedureName = procName;
            this.InParameters = new ProcedureParameterCollection();
            this.OutParameters = new ProcedureParameterCollection();
            this.DbConnection = conn;
        }

        /// <summary>
        /// 用指定的存储过程名称构造存储过程的实例
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="connstr"></param>
        public Procedure(string procName, string connstr)
        {
            this.ProcedureName = procName;
            this.InParameters = new ProcedureParameterCollection();
            this.OutParameters = new ProcedureParameterCollection();
            this.DbConnectionString = connstr;
        }

        /// <summary>
        /// 添加输入参数
        /// </summary>
        /// <param name="parameterName">参数的名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">大小</param>
        /// <param name="value">参数的值</param>
        public Procedure AddInParameter(string parameterName, DbType dbType, int size, object value)
        {
            InParameters.Add(new ProcedureParameter(parameterName, dbType, size, value));
            return this;
        }


        /// <summary>
        /// 添加输出参数
        /// </summary>
        /// <param name="parameterName">参数的名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">大小</param>
        public Procedure AddOutParameter(string parameterName, DbType dbType, int size)
        {
            OutParameters.Add(new ProcedureParameter(parameterName, dbType, size, null));
            return this;
        }


        /// <summary>
        /// 添加输出参数
        /// </summary>
        /// <param name="parameterName">参数的名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">大小</param>
        /// <param name="value">参数的值</param>
        public Procedure AddOutParameter(string parameterName, DbType dbType, byte precision, byte scale)
        {
            OutParameters.Add(new ProcedureParameter(parameterName, dbType, precision, scale));
            return this;
        }
        

        /// <summary>
        /// 获取返回参数的值
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public object GetOutParameterValue(string paramName)
        {
            ProcedureParameter proc = this.OutParameters.Find(s => s.ParameterName == paramName);
            if (proc != null)
            {
                return proc.Value;
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 执行不带查询的存储过程
        /// </summary>
        /// <returns></returns>
        public int ExecuteNonQuery()
        {
            return Database.ExecuteProcedureNonQuery(this);
        }

        /*
        /// <summary>
        /// 在指定的事务中，执行不带查询的存储过程
        /// </summary>
        /// <returns></returns>
        public int ExecuteNonQuery(Transaction trans)
        {
            return Database.ExecuteProcedureNonQuery(trans, this);
        }
        */

        /// <summary>
        /// 执行存储过程，返回表格
        /// </summary>
        /// <returns></returns>
        public DataTable ExecuteDataTable()
        {
            return Database.ExecuteProcedureDataTable(this);
        }

        /// <summary>
        /// 执行存储过程，返回RecordList
        /// </summary>
        /// <returns></returns>
        public RecordList<Record> ExecuteRecordList()
        {
            return Database.db.ExecuteProcedureRecordList(this);
        }

        /// <summary>
        /// 执行存储过程，返回Record
        /// </summary>
        /// <returns></returns>
        public Record ExecuteRecord()
        {
            RecordList<Record> lst = ExecuteRecordList();
            return lst.Count > 0 ? lst[0] : null;
        }


    }
}
