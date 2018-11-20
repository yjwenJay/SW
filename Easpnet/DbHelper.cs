using Easpnet;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Easpnet
{
    public abstract class DbHelper
    {
        public abstract System.Data.Common.DbConnection Connection { get; }

        /// <summary>
        /// 用给定的连接字符串创建连接
        /// </summary>
        /// <param name="connstr"></param>
        /// <returns></returns>
        public abstract System.Data.Common.DbConnection MakeConnection(string connstr);

        /// <summary>
        /// 根据指定的表和列插入数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public abstract long Insert(string tableName, List<TableColumn> columns);

        /// <summary>
        /// 在指定的事务中执行，根据指定的表和列插入数据
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public abstract long Insert(Transaction trans, string tableName, List<TableColumn> columns);


        /// <summary>
        /// 更新数据，条件根据主键列
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public abstract bool Update(string tableName, List<TableColumn> updateColumns, List<TableColumn> primaryKeyColumns);


        /// <summary>
        /// 在指定的事务中，根据主键列，进行数据更新
        /// </summary>
        /// <param name="trans">事务</param> 
        /// <param name="tableName">表名</param>
        /// <param name="columns">列集合</param>
        /// <returns></returns>
        public abstract bool Update(Transaction trans, string tableName, List<TableColumn> updateColumns, List<TableColumn> primaryKeyColumns);


        /// <summary>
        /// 更新数据，条件为Easpnet.Query查询结构
        /// </summary>
        /// <param name="tableName">表的名称</param>
        /// <param name="updateColumns">要更新的字段名称</param>
        /// <param name="query">更新的条件</param>
        /// <returns>返回更新的数据条数</returns>
        public abstract int Update(string tableName, List<TableColumn> updateColumns, Query query);


        /// <summary>
        /// 在指定的事务中更新数据，条件为Easpnet.Query查询结构
        /// </summary>
        /// <param name="trans">事务</param> 
        /// <param name="tableName">表的名称</param>
        /// <param name="updateColumns">要更新的字段名称</param>
        /// <param name="query">更新的条件</param>
        /// <returns>返回更新的数据条数</returns>
        public abstract int Update(Transaction trans, string tableName, List<TableColumn> updateColumns, Query query);


        /// <summary>
        /// 将某个字段进行数字的累加
        /// </summary>
        /// <param name="tableName">表的名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <returns></returns>
        public abstract int Cumulative(string tableName, string fieldName, double num, List<TableColumn> primaryKeyColumns);



        /// <summary>
        /// 在指定的事务中，将某个字段进行数字的累加
        /// </summary>
        /// <param name="trans">事务</param> 
        /// <param name="tableName">表的名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <returns></returns>
        public abstract int Cumulative(Transaction trans, string tableName, string fieldName, double num, List<TableColumn> primaryKeyColumns);



        /// <summary>
        /// 将某个字段进行数字的累加
        /// </summary>
        /// <param name="tableName">表的名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public abstract int Cumulative(string tableName, string fieldName, double num, string condition);


        /// <summary>
        /// 在指定的事务中，将某个字段进行数字的累加
        /// </summary>
        /// <param name="trans">事务</param> 
        /// <param name="tableName">表的名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public abstract int Cumulative(Transaction trans, string tableName, string fieldName, double num, string condition);
        

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">要删除数据的表</param>
        /// <param name="primaryKeyColumns">主键列</param>
        /// <returns></returns>
        public abstract int Delete(string tableName, string condition);


        /// <summary>
        /// 在指定的事务中删除数据
        /// </summary>
        /// <param name="tableName">要删除数据的表</param>
        /// <param name="primaryKeyColumns">主键列</param>
        /// <returns></returns>
        public abstract int Delete(Transaction trans, string tableName, string condition);



        /// <summary>
        /// 根据条件删除删除数据
        /// </summary>
        /// <param name="tableName">要删除数据的表</param>
        /// <param name="primaryKeyColumns">主键列</param>
        /// <returns></returns>
        public abstract bool Delete(string tableName, List<TableColumn> primaryKeyColumns);



        /// <summary>
        /// 根据条件删除删除数据
        /// </summary>
        /// <param name="tableName">要删除数据的表</param>
        /// <param name="primaryKeyColumns">主键列</param>
        /// <returns></returns>
        public abstract bool Delete(Transaction trans, string tableName, List<TableColumn> primaryKeyColumns);


        /// <summary>
        /// 获取单个的数据访问方法
        /// </summary>
        /// <param name="primaryKeyColumns"></param>
        /// <returns></returns>
        public abstract IDataReader GetModelReader(string tableName, List<TableColumn> columns, List<TableColumn> primaryKeyColumns);

        /// <summary>
        /// 获取单个的数据访问方法 - 指定锁方式
        /// </summary>
        /// <param name="primaryKeyColumns"></param>
        /// <returns></returns>
        public abstract IDataReader GetModelReader(string tableName, List<TableColumn> columns, List<TableColumn> primaryKeyColumns, DbLock lockType, Transaction trans);


        /// <summary>
        /// 执行选择操作，使用通用存储过程
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public abstract IDataReader SelectReader(string tableName, string identifier, string condition, string orderby, PageParam page, out IDataParameter count, out IDataParameter total);

        /// <summary>
        /// 执行选择操作，使用通用存储过程
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public abstract IDataReader SelectReader(string tableName, List<TableColumn> columns, string identifier, string condition, string orderby, PageParam page, out IDataParameter count, out IDataParameter total);
        
        /// <summary>
        /// 执行选择操作
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public abstract IDataReader SelectReader(string tableName, List<TableColumn> columns, string condition, string orderby, DbLock lockType, Transaction trans);
        
        /// <summary>
        /// 执行选中，值选中前count条记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public abstract IDataReader SelectReader(string tableName, List<TableColumn> columns, string condition, string orderby, int count);

        /// <summary>
        /// 执行选中，值选中前count条记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public abstract IDataReader SelectReader(string tableName, List<TableColumn> columns, string condition, string orderby, int count, DbLock lockType, Transaction trans);



        /// <summary>
        /// 统计数量
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public abstract int SelectCount(string tableName, string condition);
        /// <summary>
        /// 统计总数
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public abstract double SelectSum(string tableName, string fieldName, string condition);
        /// <summary>
        /// 根据查询条件查询表格
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="query">查询条件</param>
        /// <param name="count">选择的数量，设为小于或者等于0的数表示查询全部</param>
        /// <returns></returns>
        public abstract DataTable SelectTable(string tableName, Query query, int count);
        /// <summary>
        /// 根据查询条件查询表格
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="query">查询条件</param>
        /// <param name="count">选择的数量，设为小于或者等于0的数表示查询全部</param>
        /// <returns></returns>
        public abstract DataTable SelectTable(string tableName, Query query, int count, DbLock lockType, Transaction trans);
        /// <summary>
        /// 查询最大值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public abstract double SelectMax(string tableName, string fieldName, Query query);
        /// <summary>
        /// 查询最小值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public abstract double SelectMin(string tableName, string fieldName, Query query);
        /// <summary>
        /// 执行不返回记录集的存储过程
        /// </summary>
        /// <returns>影响的行数</returns>
        public abstract int ExecuteProcedureNonQuery(Procedure proc);

        /// <summary>
        /// 在指定的事务中，执行不返回记录集的存储过程
        /// </summary>
        /// <returns>影响的行数</returns>
        //public abstract int ExecuteProcedureNonQuery(Transaction trans, Procedure proc);

        /// <summary>
        /// 执行存储过程，返回DataTable
        /// </summary>
        /// <returns>影响的行数</returns>
        public abstract DataTable ExecuteProcedureDataTable(Procedure proc);

        /// <summary>
        /// 执行存储过程，返回DataReader
        /// </summary>
        /// <returns>影响的行数</returns>
        public abstract RecordList<Record> ExecuteProcedureRecordList(Procedure proc);


        /////////// 新版 Model 数据访问 //////////////////////
        /// <summary>
        /// 根据模型进行查询
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public abstract IDataReader Select(Db m);
        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="m"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract int Update(Db m, NameObject[] data);

        /// <summary>
        /// 执行插入操作
        /// </summary>
        /// <param name="m"></param>
        /// <param name="data"></param>
        /// <param name="isGetId">是否获取identity</param>
        /// <returns></returns>
        public abstract long Insert(Db m, NameObject[] data,bool isGetId);

        /// <summary>
        /// 执行原生的Sql
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="conn">数据库连接</param>
        /// <param name="constr">连接字符串</param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract int Execute(Transaction trans,DbConnection conn, string constr, string sql, params object[] parameters);


        /// <summary>
        /// 执行原生的查询
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="conn">数据库连接</param>
        /// <param name="constr">连接字符串</param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        public abstract IDataReader ExecuteSelect(Transaction trans, DbConnection conn, string constr, string sql, params object[] parameters);

        
    }
}
