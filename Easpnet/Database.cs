using System.Collections.Generic;
using System.Data;
using Local;
using System.Data.Common;

namespace Easpnet
{
    public static class Database
    {
        public static DbHelper db = new Sql.SqlDbHelper();

        /*
        static Database()
        {
            switch (LocalConfig.DatabaseType)
            {
                case "Sql":
                    db = new Sql.SqlDbHelper();
                    break;
                default:
                    db = new Sql.SqlDbHelper();
                    break;
            }
        }*/

        /// <summary>
        /// 根据指定的表和列插入数据
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static long Insert(string tableName, List<TableColumn> columns)
        {
            return db.Insert(tableName, columns);
        }


        /// <summary>
        /// 根据指定的表和列插入数据
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static long Insert(Transaction trans, string tableName, List<TableColumn> columns)
        {
            return db.Insert(trans,tableName, columns);
        }




        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="updateColumns">待更新的列集合</param>
        /// <param name="primaryKeyColumns">主键列集合</param>
        /// <returns></returns>
        public static bool Update(string tableName, List<TableColumn> updateColumns, List<TableColumn> primaryKeyColumns)
        {
            return db.Update(tableName, updateColumns, primaryKeyColumns);
        }


        /// <summary>
        /// 在指定的事务中进行更新数据（根据主键列）
        /// </summary>
        /// <param name="trans">事务</param> 
        /// <param name="tableName">表名</param>
        /// <param name="updateColumns">待更新的列集合</param>
        /// <param name="primaryKeyColumns">主键列集合</param>
        /// <returns></returns>
        public static bool Update(Transaction trans, string tableName, List<TableColumn> updateColumns, List<TableColumn> primaryKeyColumns)
        {
            return db.Update(trans,tableName, updateColumns, primaryKeyColumns);
        }


        /// <summary>
        /// 更新数据，条件为Easpnet.Query查询结构
        /// </summary>
        /// <param name="tableName">表的名称</param>
        /// <param name="updateColumns">要更新的字段名称</param>
        /// <param name="query">更新的条件</param>
        /// <returns></returns>
        public static int Update(string tableName, List<TableColumn> updateColumns, Query query)
        {
            return db.Update(tableName, updateColumns, query);
        }

        /// <summary>
        /// 在指定的事务中更新数据，条件为Easpnet.Query查询结构
        /// </summary>
        /// <param name="trans">事务</param> 
        /// <param name="tableName">表的名称</param>
        /// <param name="updateColumns">要更新的字段名称</param>
        /// <param name="query">更新的条件</param>
        /// <returns>返回更新的数据条数</returns>
        public static int Update(Transaction trans, string tableName, List<TableColumn> updateColumns, Query query)
        {
            return db.Update(trans, tableName, updateColumns, query);
        }

        /// <summary>
        /// 将某个字段进行数字的累加
        /// </summary>
        /// <param name="tableName">表的名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <param name="primaryKeyColumns">主键列</param>
        /// <returns></returns>
        public static int Cumulative(string tableName, string fieldName, double num, List<TableColumn> primaryKeyColumns)
        {
            return db.Cumulative(tableName, fieldName, num, primaryKeyColumns);
        }



        /// <summary>
        /// 在指定的事务中，将某个字段进行数字的累加
        /// </summary>
        /// <param name="trans">事务</param> 
        /// <param name="tableName">表的名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <param name="primaryKeyColumns">主键列</param>
        /// <returns></returns>
        public static int Cumulative(Transaction trans, string tableName, string fieldName, double num, List<TableColumn> primaryKeyColumns)
        {
            return db.Cumulative(trans, tableName, fieldName, num, primaryKeyColumns);
        }


        /// <summary>
        /// 将某个字段进行数字的累加
        /// </summary>
        /// <param name="tableName">表的名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <param name="query">条件</param>
        /// <returns></returns>
        public static int Cumulative(string tableName, string fieldName, double num, Query query)
        {
            return db.Cumulative(tableName, fieldName, num, query.GenerateQueryString());
        }


        /// <summary>
        /// 在指定的事务中，将某个字段进行数字的累加
        /// </summary>
        /// <param name="trans">事务</param> 
        /// <param name="tableName">表的名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <param name="query">条件</param>
        /// <returns></returns>
        public static int Cumulative(Transaction trans, string tableName, string fieldName, double num, Query query)
        {
            return db.Cumulative(trans, tableName, fieldName, num, query.GenerateQueryString());
        }



        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">要删除数据的表</param>
        /// <param name="primaryKeyColumns">主键列</param>
        /// <returns></returns>
        public static bool Delete(string tableName, List<TableColumn> primaryKeyColumns)
        {
            return db.Delete(tableName, primaryKeyColumns);
        }



        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="tableName">要删除数据的表</param>
        /// <param name="primaryKeyColumns">主键列</param>
        /// <returns></returns>
        public static bool Delete(Transaction trans, string tableName, List<TableColumn> primaryKeyColumns)
        {
            return db.Delete(trans,tableName, primaryKeyColumns);
        }



        /// <summary>
        /// 删除数据，反悔删除的数据条数
        /// </summary>
        /// <param name="tableName">要删除数据的表</param>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public static int Delete(string tableName, Query query)
        {
            return db.Delete(tableName, query.GenerateQueryString());
        }



        /// <summary>
        /// 删除数据，反悔删除的数据条数
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="tableName">要删除数据的表</param>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public static int Delete(Transaction trans, string tableName, Query query)
        {
            return db.Delete(trans, tableName, query.GenerateQueryString());
        }



        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="primaryKeyColumns"></param>
        /// <returns></returns>
        public static IDataReader GetModelReader(string tableName, List<TableColumn> columns, List<TableColumn> primaryKeyColumns)
        {
            return db.GetModelReader(tableName, columns, primaryKeyColumns);
        }

        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="primaryKeyColumns"></param>
        /// <returns></returns>
        public static IDataReader GetModelReader(string tableName, List<TableColumn> columns, List<TableColumn> primaryKeyColumns, DbLock lockType, Transaction trans)
        {
            return db.GetModelReader(tableName, columns, primaryKeyColumns, lockType, trans);
        }

        /// <summary>
        /// 执行选择操作，使用通用存储过程
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="condition"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public static IDataReader SelectReader(string tableName, string identifier, string condition, string orderby, PageParam page, out IDataParameter count, out IDataParameter total)
        {
            return db.SelectReader(tableName, identifier, condition, orderby, page, out count, out total);
        }

        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="identifier"></param>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IDataReader SelectReader(string tableName, string identifier, Query query, string orderby, PageParam page, out IDataParameter count, out IDataParameter total)
        {
            return db.SelectReader(tableName, identifier, query.GenerateQueryString(), orderby, page, out count, out total);
        }

        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="identifier"></param>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IDataReader SelectReader(string tableName, List<TableColumn> columns, string identifier, Query query, string orderby, PageParam page, out IDataParameter count, out IDataParameter total)
        {
            return db.SelectReader(tableName, columns, identifier, query.GenerateQueryString(), orderby, page, out count, out total);
        }


        /// <summary>
        /// 执行选择操作
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public static IDataReader SelectReader(string tableName, List<TableColumn> columns, Query query, string orderby)
        {
            return db.SelectReader(tableName, columns, query.GenerateQueryString(), orderby, DbLock.None, null);
        }


        /// <summary>
        /// 执行选择操作
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public static IDataReader SelectReader(string tableName, List<TableColumn> columns, Query query, string orderby, DbLock lockType, Transaction trans)
        {
            return db.SelectReader(tableName, columns, query.GenerateQueryString(), orderby, lockType, trans);
        }



        /// <summary>
        /// 执行选中，值选中前count条记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IDataReader SelectReader(string tableName, List<TableColumn> columns, Query query, string orderby, int count)
        {
            return db.SelectReader(tableName, columns, query.GenerateQueryString(), orderby, count);
        }

        /// <summary>
        /// 执行选中，值选中前count条记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IDataReader SelectReader(string tableName, List<TableColumn> columns, Query query, string orderby, int count, DbLock lockType, Transaction trans)
        {
            return db.SelectReader(tableName, columns, query.GenerateQueryString(), orderby, count, lockType, trans);
        }

        /// <summary>
        /// 统计数量
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int SelectCount(string tableName, Query query)
        {
            return db.SelectCount(tableName, query.GenerateQueryString());
        }


        /// <summary>
        /// 统计总数
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static double SelectSum(string tableName, string fieldName, Query query)
        {
            return db.SelectSum(tableName, fieldName, query.GenerateQueryString());
        }

        /// <summary>
        /// 根据查询条件查询表格
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="query">查询条件</param>
        /// <param name="count">选择的数量，设为小于或者等于0的数表示查询全部</param>
        /// <returns></returns>
        public static DataTable SelectTable(string tableName, Query query, int count)
        {
            return db.SelectTable(tableName, query, count);
        }
        /// <summary>
        /// 根据查询条件查询表格
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="query">查询条件</param>
        /// <param name="count">选择的数量，设为小于或者等于0的数表示查询全部</param>
        /// <returns></returns>
        public static DataTable SelectTable(string tableName, Query query, int count, DbLock lockType, Transaction trans)
        {
            return db.SelectTable(tableName, query, count, lockType, trans);
        }
        /// <summary>
        /// 查询最大值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static double SelectMax(string tableName, string fieldName, Query query)
        {
            return db.SelectMax(tableName, fieldName, query);
        }

        /// <summary>
        /// 查询最小值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static double SelectMin(string tableName, string fieldName, Query query)
        {
            return db.SelectMin(tableName, fieldName, query);
        }

        /// <summary>
        /// 执行不返回记录集的存储过程
        /// </summary>
        /// <returns>影响的行数</returns>
        public static int ExecuteProcedureNonQuery(Procedure proc)
        {
            return db.ExecuteProcedureNonQuery(proc);
        }

        /*
        /// <summary>
        /// 执行不返回记录集的存储过程
        /// </summary>
        /// <returns>影响的行数</returns>
        public static int ExecuteProcedureNonQuery(Transaction trans, Procedure proc)
        {
            return db.ExecuteProcedureNonQuery(trans, proc);
        }
        */

        /// <summary>
        /// 执行存储过程，返回DataTable
        /// </summary>
        /// <returns>影响的行数</returns>
        public static DataTable ExecuteProcedureDataTable(Procedure proc)
        {
            return db.ExecuteProcedureDataTable(proc);
        }


        /// <summary>
        /// 执行自定义Sql语句
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int Execute(Transaction trans, string sql, params object[] parameters)
        {
            return db.Execute(trans, null, null, sql, parameters);
        }

        /// <summary>
        /// 执行自定义Sql语句
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int Execute(string sql, params object[] parameters)
        {
            return db.Execute(null, null, null, sql, parameters);
        }
        /// <summary>
        /// 执行自定义Sql语句
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>返回影响的行数</returns>
        public static int Execute(DbConnection conn, string sql, params object[] parameters)
        {
            return Database.db.Execute(null, conn, null, sql, parameters);
        }

        /// <summary>
        /// 执行自定义Sql语句
        /// </summary>
        /// <param name="connstr">连接字符串</param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>返回影响的行数</returns>
        public static int Execute(string connstr, string sql, params object[] parameters)
        {
            return Database.db.Execute(null, null, connstr, sql, parameters);
        }


    }
}
