using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Local;
using System.Text.RegularExpressions;
using System.Data.Common;

namespace Easpnet.Sql
{
    public class SqlDbHelper : DbHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string connectionString = ConfigurationManager.AppSettings["DB_ReadWrite_ConnString"];

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        public override System.Data.Common.DbConnection Connection
        {
            get
            {
                return new SqlConnection(SqlDbHelper.connectionString);
            }
        }

        /// <summary>
        /// 用给定的连接字符串创建连接
        /// </summary>
        /// <param name="connstr"></param>
        /// <returns></returns>
        public override System.Data.Common.DbConnection MakeConnection(string connstr)
        {
            return new SqlConnection(connstr);
        }

        /// <summary>
        /// 根据指定的表和列插入数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public override long Insert(string tableName, List<Easpnet.TableColumn> columns)
        {
            Easpnet.TableColumn identifier = columns.Find(f => f.IsIdentifier);

            //
            columns = columns.FindAll(f => f.IsIdentifier != true);

            //构造Sql语句
            StringBuilder s = new StringBuilder();
            s.Append("insert into [" + tableName + "](");
            for (int i = 0; i < columns.Count; i++)
            {
                if (i > 0)
                {
                    s.Append(",");
                }

                s.Append("[" + columns[i].ColumnName + "]");
            }
            s.Append(")");
            s.Append("values(");
            for (int i = 0; i < columns.Count; i++)
            {
                if (i > 0)
                {
                    s.Append(",");
                }

                s.Append("@" + columns[i].ColumnName);
            }
            s.Append(")");

            SqlParameter[] par;

            //有自增列的情况
            if (identifier != null)
            {
                s.Append(" set @Id=@@identity");
                //构造参数
                par = new SqlParameter[columns.Count + 1];
                par[0] = MakeOutputParam("@Id", SqlDbType.Int, 4);
                for (int i = 0; i < columns.Count; i++)
                {
                    Easpnet.TableColumn cl = columns[i];
                    object val = cl.IsIdentifier ? null : cl.Value;
                    par[i + 1] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, val);
                }
            }
            //无自增列的情况
            else
            {
                //构造参数
                par = new SqlParameter[columns.Count];
                for (int i = 0; i < columns.Count; i++)
                {
                    Easpnet.TableColumn cl = columns[i];
                    object val = cl.IsIdentifier ? null : cl.Value;
                    par[i] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, val);
                }
            }

            string sql = s.ToString();
            //执行插入操作
            int j;
            if ((j = ExecuteNonQuery(connectionString, CommandType.Text, sql, par)) > 0)
            {
                try
                {
                    if (identifier == null)
                    {
                        return j;
                    }
                    else
                    {
                        return Convert.ToInt32(par[0].Value);
                    }                    
                }
                catch
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据指定的表和列插入数据，在事务中执行
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public override long Insert(Transaction trans, string tableName, List<TableColumn> columns)
        {
            Easpnet.TableColumn identifier = columns.Find(f => f.IsIdentifier);

            //
            columns = columns.FindAll(f => f.IsIdentifier != true);

            //构造Sql语句
            StringBuilder s = new StringBuilder();
            s.Append("insert into [" + tableName + "](");
            for (int i = 0; i < columns.Count; i++)
            {
                if (i > 0)
                {
                    s.Append(",");
                }

                s.Append("[" + columns[i].ColumnName + "]");
            }
            s.Append(")");
            s.Append("values(");
            for (int i = 0; i < columns.Count; i++)
            {
                if (i > 0)
                {
                    s.Append(",");
                }

                s.Append("@" + columns[i].ColumnName);
            }
            s.Append(")");

            /*
            s.Append(" set @Id=@@identity");



            //构造参数
            SqlParameter[] par = new SqlParameter[columns.Count + 1];
            par[0] = MakeOutputParam("@Id", SqlDbType.Int, 4);
            for (int i = 0; i < columns.Count; i++)
            {
                Easpnet.TableColumn cl = columns[i];
                object val = cl.IsIdentifier ? null : cl.Value;
                par[i + 1] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, val);
            }*/
            
            SqlParameter[] par;

            //有自增列的情况
            if (identifier != null)
            {
                s.Append(" set @Id=@@identity");
                //构造参数
                par = new SqlParameter[columns.Count + 1];
                par[0] = MakeOutputParam("@Id", SqlDbType.Int, 4);
                for (int i = 0; i < columns.Count; i++)
                {
                    Easpnet.TableColumn cl = columns[i];
                    object val = cl.IsIdentifier ? null : cl.Value;
                    par[i + 1] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, val);
                }
            }
            //无自增列的情况
            else
            {
                //构造参数
                par = new SqlParameter[columns.Count];
                for (int i = 0; i < columns.Count; i++)
                {
                    Easpnet.TableColumn cl = columns[i];
                    object val = cl.IsIdentifier ? null : cl.Value;
                    par[i] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, val);
                }
            }

            string sql = s.ToString();
            //执行插入操作
            int j;
            if ((j = ExecuteNonQuery(trans, CommandType.Text, sql, par)) > 0)
            {
                try
                {
                    if (identifier == null)
                    {
                        return j;
                    }
                    else
                    {
                        return Convert.ToInt32(par[0].Value);
                    }
                }
                catch
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public override bool Update(string tableName, List<TableColumn> updateColumns, List<TableColumn> primaryKeyColumns)
        {
            if (primaryKeyColumns.Count == 0)
            {
                throw new Exception("对表" + tableName + "更新时，未能找到主键列，不能进行更新！");
            }

            //
            StringBuilder s = new StringBuilder();
            s.Append("update [" + tableName + "] set ");
            for (int i = 0; i < updateColumns.Count - 1; i++)
            {
                s.Append("[" + updateColumns[i].ColumnName + "]=@" + updateColumns[i].ColumnName + ",");
            }
            s.Append("[" + updateColumns[updateColumns.Count - 1].ColumnName + "]=@" + updateColumns[updateColumns.Count - 1].ColumnName);


            s.Append(" where ");

            for (int i = 0; i < primaryKeyColumns.Count - 1; i++)
            {
                s.Append("[" + primaryKeyColumns[i].ColumnName + "]=@" + primaryKeyColumns[i].ColumnName + " and ");
            }

            s.Append("[" + primaryKeyColumns[primaryKeyColumns.Count - 1].ColumnName + "]=@" + primaryKeyColumns[primaryKeyColumns.Count - 1].ColumnName);

            //构造参数
            SqlParameter[] par = new SqlParameter[updateColumns.Count + primaryKeyColumns.Count];
            for (int i = 0; i < updateColumns.Count; i++)
            {
                Easpnet.TableColumn cl = updateColumns[i];
                par[i] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, cl.Value);
            }

            int updateColumnsCount = updateColumns.Count;
            for (int i = 0; i < primaryKeyColumns.Count; i++)
            {
                Easpnet.TableColumn cl = primaryKeyColumns[i];
                par[i + updateColumnsCount] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, cl.Value);
            }
            
            //
            string sql = s.ToString();
            //执行更新操作
            return ExecuteNonQuery(connectionString, CommandType.Text, sql, par) > 0;
        }


        /// <summary>
        /// 更新数据，条件根据主键列
        /// </summary>
        /// <param name="trans">事务</param> 
        /// <param name="tableName">表名</param>
        /// <param name="columns">列集合</param>
        /// <returns></returns>
        public override bool Update(Transaction trans, string tableName, List<TableColumn> updateColumns, List<TableColumn> primaryKeyColumns)
        {
            if (primaryKeyColumns.Count == 0)
            {
                throw new Exception("对表" + tableName + "更新时，未能找到主键列，不能进行更新！");
            }

            //
            StringBuilder s = new StringBuilder();
            s.Append("update [" + tableName + "] set ");
            for (int i = 0; i < updateColumns.Count - 1; i++)
            {
                s.Append("[" + updateColumns[i].ColumnName + "]=@" + updateColumns[i].ColumnName + ",");
            }
            s.Append("[" + updateColumns[updateColumns.Count - 1].ColumnName + "]=@" + updateColumns[updateColumns.Count - 1].ColumnName);


            s.Append(" where ");

            for (int i = 0; i < primaryKeyColumns.Count - 1; i++)
            {
                s.Append("[" + primaryKeyColumns[i].ColumnName + "]=@" + primaryKeyColumns[i].ColumnName + " and ");
            }

            s.Append("[" + primaryKeyColumns[primaryKeyColumns.Count - 1].ColumnName + "]=@" + primaryKeyColumns[primaryKeyColumns.Count - 1].ColumnName);

            //构造参数
            SqlParameter[] par = new SqlParameter[updateColumns.Count + primaryKeyColumns.Count];
            for (int i = 0; i < updateColumns.Count; i++)
            {
                Easpnet.TableColumn cl = updateColumns[i];
                par[i] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, cl.Value);
            }

            int updateColumnsCount = updateColumns.Count;
            for (int i = 0; i < primaryKeyColumns.Count; i++)
            {
                Easpnet.TableColumn cl = primaryKeyColumns[i];
                par[i + updateColumnsCount] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, cl.Value);
            }

            //
            string sql = s.ToString();
            //执行更新操作
            return ExecuteNonQuery(trans, CommandType.Text, sql, par) > 0;
        }
        

        /// <summary>
        /// 更新数据，条件为Easpnet.Query查询结构
        /// </summary>
        /// <param name="tableName">表的名称</param>
        /// <param name="updateColumns">要更新的字段名称</param>
        /// <param name="query">更新的条件</param>
        /// <returns></returns>
        public override int Update(string tableName, List<TableColumn> updateColumns, Query query)
        {
            StringBuilder s = new StringBuilder();
            s.Append("update [" + tableName + "] set ");
            for (int i = 0; i < updateColumns.Count - 1; i++)
            {
                s.Append("[" + updateColumns[i].ColumnName + "]=@" + updateColumns[i].ColumnName + ",");
            }
            s.Append("[" + updateColumns[updateColumns.Count - 1].ColumnName + "]=@" + updateColumns[updateColumns.Count - 1].ColumnName);

            if (query != null)
            {
                string where = query.GenerateQueryString();
                if (!string.IsNullOrEmpty(where))
                {
                    s.Append(" where " + where);
                }
            }

            //构造参数
            SqlParameter[] par = new SqlParameter[updateColumns.Count];
            for (int i = 0; i < updateColumns.Count; i++)
            {
                Easpnet.TableColumn cl = updateColumns[i];
                par[i] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, cl.Value);
            }

            //
            string sql = s.ToString();
            //执行更新操作
            return ExecuteNonQuery(connectionString, CommandType.Text, sql, par);
        }


        /// <summary>
        /// 在指定的事务中更新数据，条件为Easpnet.Query查询结构
        /// </summary>
        /// <param name="trans">事务</param> 
        /// <param name="tableName">表的名称</param>
        /// <param name="updateColumns">要更新的字段名称</param>
        /// <param name="query">更新的条件</param>
        /// <returns>返回更新的数据条数</returns>
        public override int Update(Transaction trans, string tableName, List<TableColumn> updateColumns, Query query)
        {
            StringBuilder s = new StringBuilder();
            s.Append("update [" + tableName + "] set ");
            for (int i = 0; i < updateColumns.Count - 1; i++)
            {
                s.Append("[" + updateColumns[i].ColumnName + "]=@" + updateColumns[i].ColumnName + ",");
            }
            s.Append("[" + updateColumns[updateColumns.Count - 1].ColumnName + "]=@" + updateColumns[updateColumns.Count - 1].ColumnName);

            if (query != null)
            {
                string where = query.GenerateQueryString();
                if (!string.IsNullOrEmpty(where))
                {
                    s.Append(" where " + where);
                }
            }

            //构造参数
            SqlParameter[] par = new SqlParameter[updateColumns.Count];
            for (int i = 0; i < updateColumns.Count; i++)
            {
                Easpnet.TableColumn cl = updateColumns[i];
                par[i] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, cl.Value);
            }

            //
            string sql = s.ToString();
            //执行更新操作
            return ExecuteNonQuery(trans, CommandType.Text, sql, par);
        }
       

        /// <summary>
        /// 将某个字段进行数字的累加
        /// </summary>
        /// <param name="tableName">表的名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <returns></returns>
        public override int Cumulative(string tableName, string fieldName, double num, List<TableColumn> primaryKeyColumns)
        {
            if (primaryKeyColumns.Count == 0)
            {
                throw new Exception("对表" + tableName + "删除数据时，未能找到主键列，不能进行更新！");
            }

            StringBuilder s = new StringBuilder();
            s.Append("update [" + tableName + "]");

            if (num > 0)
            {
                s.Append(" set [" + fieldName + "]=[" + fieldName + "]+" + num + "");
            }
            else
            {
                num = -num;
                s.Append(" set [" + fieldName + "]=[" + fieldName + "]-" + num + "");
            }

            s.Append(" where ");

            for (int i = 0; i < primaryKeyColumns.Count - 1; i++)
            {
                s.Append("[" + primaryKeyColumns[i].ColumnName + "]=@" + primaryKeyColumns[i].ColumnName + " and ");
            }

            s.Append("[" + primaryKeyColumns[primaryKeyColumns.Count - 1].ColumnName + "]=@" + primaryKeyColumns[primaryKeyColumns.Count - 1].ColumnName);
            
            //
            string sql = s.ToString();

            //构造参数
            SqlParameter[] par = new SqlParameter[primaryKeyColumns.Count];
            for (int i = 0; i < primaryKeyColumns.Count; i++)
            {
                Easpnet.TableColumn cl = primaryKeyColumns[i];
                par[i] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, cl.Value);
            }

            //执行插入操作
            return ExecuteNonQuery(connectionString, CommandType.Text, sql, par);
        }



        /// <summary>
        /// 在指定的事务中，将某个字段进行数字的累加
        /// </summary>
        /// <param name="trans">事务</param> 
        /// <param name="tableName">表的名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <returns></returns>
        public override int Cumulative(Transaction trans, string tableName, string fieldName, double num, List<TableColumn> primaryKeyColumns)
        {
            if (primaryKeyColumns.Count == 0)
            {
                throw new Exception("对表" + tableName + "删除数据时，未能找到主键列，不能进行更新！");
            }

            StringBuilder s = new StringBuilder();
            s.Append("update [" + tableName + "]");

            if (num > 0)
            {
                s.Append(" set [" + fieldName + "]=[" + fieldName + "]+" + num + "");
            }
            else
            {
                num = -num;
                s.Append(" set [" + fieldName + "]=[" + fieldName + "]-" + num + "");
            }

            s.Append(" where ");

            for (int i = 0; i < primaryKeyColumns.Count - 1; i++)
            {
                s.Append("[" + primaryKeyColumns[i].ColumnName + "]=@" + primaryKeyColumns[i].ColumnName + " and ");
            }

            s.Append("[" + primaryKeyColumns[primaryKeyColumns.Count - 1].ColumnName + "]=@" + primaryKeyColumns[primaryKeyColumns.Count - 1].ColumnName);

            //
            string sql = s.ToString();

            //构造参数
            SqlParameter[] par = new SqlParameter[primaryKeyColumns.Count];
            for (int i = 0; i < primaryKeyColumns.Count; i++)
            {
                Easpnet.TableColumn cl = primaryKeyColumns[i];
                par[i] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, cl.Value);
            }

            //执行操作
            return ExecuteNonQuery(trans, CommandType.Text, sql, par);
        }


        /// <summary>
        /// 将某个字段进行数字的累加
        /// </summary>
        /// <param name="tableName">表的名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public override int Cumulative(string tableName, string fieldName, double num, string condition)
        {
            string s = "";
            if (num > 0)
            {
                s = "update [{0}] set [{1}]=[{1}]+{2}";
            }
            else
            {
                num = -num;
                s = "update [{0}] set [{1}]=[{1}]-{2}";
            }
             
            s = string.Format(s, tableName, fieldName, num);
            if (!string.IsNullOrEmpty(condition))
            {
                s += " where " + condition;
            }

            return ExecuteNonQuery(connectionString, CommandType.Text, s, null);
        }



        /// <summary>
        /// 在指定的事务中，将某个字段进行数字的累加
        /// </summary>
        /// <param name="trans">事务</param> 
        /// <param name="tableName">表的名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public override int Cumulative(Transaction trans, string tableName, string fieldName, double num, string condition)
        {
            string s = "";
            if (num > 0)
            {
                s = "update [{0}] set [{1}]=[{1}]+{2}";
            }
            else
            {
                num = -num;
                s = "update [{0}] set [{1}]=[{1}]-{2}";
            }

            s = string.Format(s, tableName, fieldName, num);
            if (!string.IsNullOrEmpty(condition))
            {
                s += " where " + condition;
            }

            return ExecuteNonQuery(trans, CommandType.Text, s, null);
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">要删除数据的表</param>
        /// <param name="primaryKeyColumns">主键列</param>
        /// <returns></returns>
        public override bool Delete(string tableName, List<TableColumn> primaryKeyColumns)
        {
            if (primaryKeyColumns.Count == 0)
            {
                throw new Exception("对表" + tableName + "删除数据时，未能找到主键列，不能进行更新！");
            }

            StringBuilder s = new StringBuilder();
            s.Append("delete from [" + tableName + "]");
            s.Append(" where ");

            for (int i = 0; i < primaryKeyColumns.Count - 1; i++)
            {
                s.Append("[" + primaryKeyColumns[i].ColumnName + "]=@" + primaryKeyColumns[i].ColumnName + " and ");
            }

            s.Append("[" + primaryKeyColumns[primaryKeyColumns.Count - 1].ColumnName + "]=@" + primaryKeyColumns[primaryKeyColumns.Count - 1].ColumnName);
            //
            string sql = s.ToString();

            //构造参数
            SqlParameter[] par = new SqlParameter[primaryKeyColumns.Count];
            for (int i = 0; i < primaryKeyColumns.Count; i++)
            {
                Easpnet.TableColumn cl = primaryKeyColumns[i];
                par[i] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, cl.Value);
            }
            
            //执行插入操作
            return ExecuteNonQuery(connectionString, CommandType.Text, sql, par) > 0;
        }

        /// <summary>
        /// 根据条件删除删除数据
        /// </summary>
        /// <param name="tableName">要删除数据的表</param>
        /// <param name="primaryKeyColumns">主键列</param>
        /// <returns></returns>
        public override bool Delete(Transaction trans, string tableName, List<TableColumn> primaryKeyColumns)
        {
            if (primaryKeyColumns.Count == 0)
            {
                throw new Exception("对表" + tableName + "删除数据时，未能找到主键列，不能进行更新！");
            }

            StringBuilder s = new StringBuilder();
            s.Append("delete from [" + tableName + "]");
            s.Append(" where ");

            for (int i = 0; i < primaryKeyColumns.Count - 1; i++)
            {
                s.Append("[" + primaryKeyColumns[i].ColumnName + "]=@" + primaryKeyColumns[i].ColumnName + " and ");
            }

            s.Append("[" + primaryKeyColumns[primaryKeyColumns.Count - 1].ColumnName + "]=@" + primaryKeyColumns[primaryKeyColumns.Count - 1].ColumnName);
            //
            string sql = s.ToString();

            //构造参数
            SqlParameter[] par = new SqlParameter[primaryKeyColumns.Count];
            for (int i = 0; i < primaryKeyColumns.Count; i++)
            {
                Easpnet.TableColumn cl = primaryKeyColumns[i];
                par[i] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, cl.Value);
            }

            //执行插入操作
            return ExecuteNonQuery(trans, CommandType.Text, sql, par) > 0;
        }


        /// <summary>
        /// 删除数据，反悔删除的数据条数
        /// </summary>
        /// <param name="tableName">要删除数据的表</param>
        /// <param name="primaryKeyColumns">主键列</param>
        /// <returns></returns>
        public override int Delete(string tableName, string condition)
        {
            string sql = "delete from [" + tableName + "]";
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " where " + condition;
            }

            return ExecuteNonQuery(connectionString, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 在指定的事务中删除数据
        /// </summary>
        /// <param name="tableName">要删除数据的表</param>
        /// <param name="primaryKeyColumns">主键列</param>
        /// <returns></returns>
        public override int Delete(Transaction trans, string tableName, string condition)
        {
            string sql = "delete from [" + tableName + "]";
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " where " + condition;
            }

            return ExecuteNonQuery(trans, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 获取单个的数据访问方法
        /// </summary>
        /// <param name="primaryKeyColumns"></param>
        /// <returns></returns>
        public override IDataReader GetModelReader(string tableName, List<TableColumn> columns, List<TableColumn> primaryKeyColumns)
        {
            return GetModelReader(tableName, columns, primaryKeyColumns, DbLock.None, null);
        }

        /// <summary>
        /// 获取单个的数据访问方法
        /// </summary>
        /// <param name="primaryKeyColumns"></param>
        /// <returns></returns>
        public override IDataReader GetModelReader(string tableName, List<TableColumn> columns, List<TableColumn> primaryKeyColumns, DbLock lockType, Transaction trans)
        {
            //构造查询的列
            string selectColumns = MakeSelectColumnString(columns);

            StringBuilder s = new StringBuilder();
            s.Append("select top 1 " + selectColumns + " from [" + tableName + "]");
            s.Append(GetDbLockString(lockType));            

            //构造参数
            SqlParameter[] par = new SqlParameter[primaryKeyColumns.Count];

            //
            if (primaryKeyColumns.Count > 0)
            {
                s.Append(" where ");
                for (int i = 0; i < primaryKeyColumns.Count - 1; i++)
                {
                    s.Append("[" + primaryKeyColumns[i].ColumnName + "]=@" + primaryKeyColumns[i].ColumnName + " and ");
                }

                s.Append("[" + primaryKeyColumns[primaryKeyColumns.Count - 1].ColumnName + "]=@" + primaryKeyColumns[primaryKeyColumns.Count - 1].ColumnName);

                //
                for (int i = 0; i < primaryKeyColumns.Count; i++)
                {
                    Easpnet.TableColumn cl = primaryKeyColumns[i];
                    par[i] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, cl.Value);
                }
            }

            //
            string sql = s.ToString();
            if (trans == null)
            {
                return ExecuteReader(connectionString, CommandType.Text, sql, par);
            }
            else
            {
                return ExecuteReader(trans, CommandType.Text, sql, par);
            }
        }



        /// <summary>
        /// 执行选择操作，使用通用存储过程
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="condition"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public override IDataReader SelectReader(string tableName, string identifier, string condition, string orderby, PageParam page, out IDataParameter count, out IDataParameter total)
        {
            return SelectReader(tableName, null, identifier, condition, orderby, page, out count, out total);
        }

        /// <summary>
        /// 执行选择操作，使用通用存储过程
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public override IDataReader SelectReader(string tableName, List<TableColumn> columns, string identifier, string condition, string orderby, PageParam page, out IDataParameter count, out IDataParameter total)
        {
            //构造查询的列
            string selectColumns = MakeSelectColumnString(columns);
            SqlParameter[] par = new SqlParameter[] { 
                MakeOutputParam("@PageCount", SqlDbType.Int,4),
                MakeOutputParam("@TotalRecords", SqlDbType.Int,4),
                MakeInputParam("@m_TableName", SqlDbType.VarChar, 255,tableName),
                MakeInputParam("@Columns", SqlDbType.VarChar, 2000, selectColumns),
                MakeInputParam("@PrimaryKey", SqlDbType.VarChar,255, identifier),
                MakeInputParam("@Condition", SqlDbType.VarChar,2000,condition),
                MakeInputParam("@OrderBy", SqlDbType.VarChar,500,orderby),
                MakeInputParam("@GroupBy", SqlDbType.VarChar,255,""),
                MakeInputParam("@PageIndex", SqlDbType.Int,4,page.Index),
                MakeInputParam("@PageSize", SqlDbType.Int,4,page.Size)
            };

            count = par[0];
            total = par[1];

            return ExecuteReader(connectionString, CommandType.StoredProcedure, "SingleTableCommonPager", par);
        }
        
        /// <summary>
        /// 执行选择操作
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public override IDataReader SelectReader(string tableName, List<TableColumn> columns, string condition, string orderby, DbLock lockType, Transaction trans)
        {
            //构造查询的列
            string selectColumns = MakeSelectColumnString(columns);

            //构造Sql语句
            string sql = "select " + selectColumns + " from [" + tableName + "]";
            sql += GetDbLockString(lockType);
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " where " + condition;
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                sql += " order by " + orderby;
            }

            if (trans == null)
            {
                return ExecuteReader(connectionString, CommandType.Text, sql, null);
            }
            else
            {
                return ExecuteReader(trans, CommandType.Text, sql, null);
            }            
        }

        /// <summary>
        /// 执行选中，值选中前count条记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override IDataReader SelectReader(string tableName, List<TableColumn> columns, string condition, string orderby, int count)
        {
            return SelectReader(tableName, columns, condition, orderby, count, DbLock.None, null);
        }

        /// <summary>
        /// 执行选中，值选中前count条记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override IDataReader SelectReader(string tableName, List<TableColumn> columns, string condition, string orderby, int count, DbLock lockType, Transaction trans)
        {
            //构造查询的列
            string selectColumns = MakeSelectColumnString(columns);

            //构造Sql语句
            string sql = "select top " + count + " " + selectColumns + " from [" + tableName + "]";
            sql += GetDbLockString(lockType);
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " where " + condition;
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                sql += " order by " + orderby;
            }

            if (trans == null)
            {
                return ExecuteReader(connectionString, CommandType.Text, sql, null);
            }
            else
            {
                return ExecuteReader(trans, CommandType.Text, sql, null);
            }
        }


        /// <summary>
        /// 统计数量
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public override int SelectCount(string tableName, string condition)
        {
            string sql = "select count(1) from [" + tableName + "]";
            //锁定方式
            sql += " with(nolock) ";
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " where " + condition;
            }

            return TypeConvert.ToInt32(ExecuteScalar(connectionString, CommandType.Text, sql, null));
        }


        /// <summary>
        /// 统计总数
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public override double SelectSum(string tableName, string fieldName, string condition)
        {
            string sql = "select sum([" + fieldName + "]) from [" + tableName + "]";
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " where " + condition;
            }

            return TypeConvert.ToDouble(ExecuteScalar(connectionString, CommandType.Text, sql, null));
        }

        /// <summary>
        /// 根据查询条件查询表格
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="query">查询条件</param>
        /// <param name="count">选择的数量，设为小于或者等于0的数表示查询全部</param>
        /// <returns></returns>
        public override DataTable SelectTable(string tableName, Query query, int count)
        {
            return SelectTable(tableName, query, count, DbLock.None, null);
        }

        /// <summary>
        /// 根据查询条件查询表格
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="query">查询条件</param>
        /// <param name="count">选择的数量，设为小于或者等于0的数表示查询全部</param>
        /// <returns></returns>
        public override DataTable SelectTable(string tableName, Query query, int count, DbLock lockType, Transaction trans)
        {
            string sql = "select ";

            //
            if (count > 0)
            {
                sql += " top " + count.ToString() + " ";
            }

            //columns
            string columns = query.GenerateSelectColumnString();
            if (!string.IsNullOrEmpty(columns))
            {
                sql += columns;
            }

            //table
            sql += " from [" + tableName + "]";

            //加锁
            sql += GetDbLockString(lockType);

            //where
            string where = query.GenerateQueryString();
            if (!string.IsNullOrEmpty(where))
            {
                sql += " where " + where;
            }

            //group by
            string groupby = query.GenerateGroupByString();
            if (!string.IsNullOrEmpty(groupby))
            {
                sql += " group by " + groupby;
            }

            //
            string orderby = query.GenerateOrderByString();
            if (!string.IsNullOrEmpty(orderby))
            {
                sql += " order by " + orderby;
            }

            //
            if (trans == null)
            {
                return ExecuteDataTable(connectionString, CommandType.Text, sql, null);
            }
            else
            {
                return ExecuteDataTable(trans, CommandType.Text, sql, null);
            }
            
        }

        /// <summary>
        /// 查询最大值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public override double SelectMax(string tableName, string fieldName, Query query)
        {
            string sql = "select max([" + fieldName + "]) from [" + tableName + "]";

            //锁定方式
            sql += " with(nolock) ";

            string condition = query.GenerateQueryString();
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " where " + condition;
            }

            return TypeConvert.ToDouble(ExecuteScalar(connectionString, CommandType.Text, sql, null));
        }

        /// <summary>
        /// 查询最小值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public override double SelectMin(string tableName, string fieldName, Query query)
        {
            string sql = "select min([" + fieldName + "]) from [" + tableName + "]";
            //锁定方式
            sql += " with(nolock) ";
            string condition = query.GenerateQueryString();
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " where " + condition;
            }

            return TypeConvert.ToDouble(ExecuteScalar(connectionString, CommandType.Text, sql, null));
        }

        /// <summary>
        /// 执行不返回记录集的存储过程
        /// </summary>
        /// <returns>影响的行数</returns>
        public override int ExecuteProcedureNonQuery(Procedure proc)
        {
            //
            SqlParameter[] par = GetProcedureParameters(proc);

            //执行
            int effectedCount = ExecuteNonQuery(proc.DbTransaction, proc.DbConnection, proc.DbConnectionString, CommandType.StoredProcedure, proc.ProcedureName, par);

            //输出赋值
            for (int i = 0; i < proc.OutParameters.Count; i++)
            {
                int j = i + proc.InParameters.Count;
                proc.OutParameters[i].Value = par[j].Value;
            }

            //返回值赋值
            proc.ReturnValue = par[proc.InParameters.Count + proc.OutParameters.Count].Value;
            return effectedCount;
        }

        /*
        /// <summary>
        /// 在指定的事务中，执行不返回记录集的存储过程
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="proc">执行的存储过程</param>
        /// <returns>影响的行数</returns>
        public override int ExecuteProcedureNonQuery(Transaction trans, Procedure proc)
        {
            //
            SqlParameter[] par = GetProcedureParameters(proc);

            //执行
            int effectedCount = ExecuteNonQuery(trans, CommandType.StoredProcedure, proc.ProcedureName, par);

            //输出赋值
            for (int i = 0; i < proc.OutParameters.Count; i++)
            {
                int j = i + proc.InParameters.Count;
                proc.OutParameters[i].Value = par[j].Value;
            }

            //返回值赋值
            proc.ReturnValue = par[proc.InParameters.Count + proc.OutParameters.Count].Value;
            return effectedCount;
        }*/


        /// <summary>
        /// 执行存储过程，返回DataTable
        /// </summary>
        /// <returns>影响的行数</returns>
        public override DataTable ExecuteProcedureDataTable(Procedure proc)
        {
            //
            SqlParameter[] par = GetProcedureParameters(proc);

            //执行
            DataTable dt = ExecuteDataTable(proc.DbTransaction, proc.DbConnection, proc.DbConnectionString, CommandType.StoredProcedure, proc.ProcedureName, par);

            //输出赋值
            for (int i = 0; i < proc.OutParameters.Count; i++)
            {
                int j = i + proc.InParameters.Count;
                proc.OutParameters[i].Value = par[j].Value;
            }

            //返回值赋值
            proc.ReturnValue = par[proc.InParameters.Count + proc.OutParameters.Count].Value;
            return dt;
        }


        /// <summary>
        /// 执行存储过程，返回DataReader
        /// </summary>
        /// <returns>影响的行数</returns>
        public override RecordList<Record> ExecuteProcedureRecordList(Procedure proc)
        {
            SqlParameter[] par = GetProcedureParameters(proc);
            IDataReader reader = ExecuteReader(proc.DbTransaction, proc.DbConnection, proc.DbConnectionString, CommandType.StoredProcedure, proc.ProcedureName, par);
            RecordList<Record> coll = new RecordList<Record>();
            while (reader.Read())
            {
                Record rec = Db.MakeRecordFromDataReader(reader);
                coll.Add(rec);
            }
            //关闭reader
            reader.Close();

            //输出赋值
            for (int i = 0; i < proc.OutParameters.Count; i++)
            {
                int j = i + proc.InParameters.Count;
                proc.OutParameters[i].Value = par[j].Value;
            }
            
            //返回值赋值
            proc.ReturnValue = par[proc.InParameters.Count + proc.OutParameters.Count].Value;
            return coll;
        }


        /// <summary>
        /// 构造存储过程的参数列表
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        private SqlParameter[] GetProcedureParameters(Procedure proc)
        {
            SqlParameter[] par = new SqlParameter[proc.InParameters.Count + proc.OutParameters.Count + 1];

            //输入参数
            for (int i = 0; i < proc.InParameters.Count; i++)
            {
                par[i] = MakeInputParam("@" + proc.InParameters[i].ParameterName,
                    ConvertDbType(proc.InParameters[i].DbType),
                    proc.InParameters[i].Size,
                    proc.InParameters[i].Value);
            }

            //输出参数
            for (int i = 0; i < proc.OutParameters.Count; i++)
            {
                int j = i + proc.InParameters.Count;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@" + proc.OutParameters[i].ParameterName;
                p1.DbType = proc.OutParameters[i].DbType;
                p1.Direction = ParameterDirection.Output;
                if (proc.OutParameters[i].Size > 0)
                {
                    p1.Size = proc.OutParameters[i].Size;
                }
                if (proc.OutParameters[i].Precision > 0)
                {
                    p1.Precision = proc.OutParameters[i].Precision;
                }
                if (proc.OutParameters[i].Scale > 0)
                {
                    p1.Scale = proc.OutParameters[i].Scale;
                }
                par[j] = p1;
            }

            //返回值参数
            SqlParameter preturn = new SqlParameter();
            preturn.ParameterName = "@ProcedureReturnValue";
            preturn.DbType = DbType.Int32;
            preturn.Direction = ParameterDirection.ReturnValue;
            par[proc.InParameters.Count + proc.OutParameters.Count] = preturn;

            //返回值
            return par;
        }

        /// <summary>
        /// 转换数据类型
        /// </summary>
        /// <returns></returns>
        private SqlDbType ConvertDbType(DbType dbtype)
        {
            SqlDbType ty = SqlDbType.VarChar;
            switch (dbtype)
            {
                case DbType.UInt16:
                case DbType.UInt32:
                case DbType.UInt64:
                case DbType.VarNumeric:
                    ty = SqlDbType.Int;
                    break;
                case DbType.AnsiString:
                    ty = SqlDbType.VarChar;
                    ty = SqlDbType.VarChar;
                    break;
                case DbType.Binary:
                    ty = SqlDbType.Image;
                    break;
                case DbType.Boolean:
                    ty = SqlDbType.Bit;
                    break;
                case DbType.Byte:
                    ty = SqlDbType.Image;
                    break;
                case DbType.Currency:
                    ty = SqlDbType.Money;
                    break;
                case DbType.Date:
                    ty = SqlDbType.Date;
                    break;
                case DbType.DateTime:
                    ty = SqlDbType.DateTime;
                    break;
                case DbType.DateTime2:
                    ty = SqlDbType.DateTime2;
                    break;
                case DbType.DateTimeOffset:
                    ty = SqlDbType.DateTimeOffset;
                    break;
                case DbType.Decimal:
                    ty = SqlDbType.Decimal;
                    break;
                case DbType.Double:
                    ty = SqlDbType.Float;
                    break;
                case DbType.Guid:
                    ty = SqlDbType.UniqueIdentifier;
                    break;
                case DbType.Int16:
                    ty = SqlDbType.SmallInt;
                    break;
                case DbType.Int32:
                    ty = SqlDbType.Int;
                    break;
                case DbType.Int64:
                    ty = SqlDbType.BigInt;
                    break;
                case DbType.Object:
                    ty = SqlDbType.Image;
                    break;
                case DbType.SByte:
                    ty = SqlDbType.Image;
                    break;
                case DbType.Single:
                    ty = SqlDbType.Float;
                    break;
                case DbType.String:
                    ty = SqlDbType.VarChar;
                    break;
                case DbType.StringFixedLength:
                    ty = SqlDbType.VarChar;
                    break;
                case DbType.Time:
                    ty = SqlDbType.Time;
                    break;
                case DbType.Xml:
                    ty = SqlDbType.Xml;
                    break;
                default:
                    break;
            }

            return ty;
        }


        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        private static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        private static int ExecuteNonQuery(Transaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, (System.Data.SqlClient.SqlConnection)trans.Connection, (System.Data.SqlClient.SqlTransaction)trans.DbTransaction, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        private static int ExecuteNonQuery(System.Data.SqlClient.SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }


        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="conn"></param>
        /// <param name="constr"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        private static int ExecuteNonQuery(Transaction trans, DbConnection conn, string constr, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            if (trans != null)
            {
                return ExecuteNonQuery(trans, cmdType, cmdText, commandParameters);
            }
            else
            {
                if (conn != null)
                {
                    return ExecuteNonQuery((System.Data.SqlClient.SqlConnection)conn, cmdType, cmdText, commandParameters);
                }
                else if (!string.IsNullOrEmpty(constr))
                {
                    return ExecuteNonQuery(constr, cmdType, cmdText, commandParameters);
                }
                else
                {
                    return ExecuteNonQuery(connectionString, cmdType, cmdText, commandParameters);
                }
            }
        }


        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return rdr;
        }

        /// <summary>
        /// 执行DataReader
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(Transaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, (System.Data.SqlClient.SqlConnection)trans.Connection, (System.Data.SqlClient.SqlTransaction)trans.DbTransaction, cmdType, cmdText, commandParameters);
            SqlDataReader rdr = cmd.ExecuteReader();

            return rdr;
        }


        /// <summary>
        /// 执行DataReader
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(System.Data.SqlClient.SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            SqlDataReader rdr = cmd.ExecuteReader();
            return rdr;
        }


        /// <summary>
        /// 执行DataReader
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="conn"></param>
        /// <param name="constr"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(Transaction trans, DbConnection conn, string constr, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            //若有事务，则在事务下面执行
            if (trans != null)
            {
                return ExecuteReader(trans, cmdType, cmdText, commandParameters);
            }
            else
            {
                if (conn != null)
                {
                    return ExecuteReader((System.Data.SqlClient.SqlConnection)conn, cmdType, cmdText, commandParameters);
                }
                else if (!string.IsNullOrEmpty(constr))
                {
                    return ExecuteReader(constr, cmdType, cmdText, commandParameters);
                }
                else
                {
                    return ExecuteReader(connectionString, cmdType, cmdText, commandParameters);
                }
            }
        }
        


        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行返回DataTable
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    DataSet ds = new DataSet();
                    SqlDataAdapter sa = new SqlDataAdapter(cmd);
                    sa.Fill(ds);
                    cmd.Parameters.Clear();
                    if (ds.Tables.Count > 0)
                    {
                        return ds.Tables[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SQL执行错误：" + cmdText, ex);
            }
            
        }

        /// <summary>
        /// 执行返回DataTable
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(Transaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, (System.Data.SqlClient.SqlConnection)trans.Connection, (System.Data.SqlClient.SqlTransaction)trans.DbTransaction, cmdType, cmdText, commandParameters);
            DataSet ds = new DataSet();
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            sa.Fill(ds);
            cmd.Parameters.Clear();
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 执行返回DataTable
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(Transaction trans, DbConnection conn, string constr, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            if (trans != null)
            {
                return ExecuteDataTable(trans, cmdType, cmdText, commandParameters);
            }
            else if (conn != null)
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, (System.Data.SqlClient.SqlConnection)conn, null, cmdType, cmdText, commandParameters);
                DataSet ds = new DataSet();
                SqlDataAdapter sa = new SqlDataAdapter(cmd);
                sa.Fill(ds);
                cmd.Parameters.Clear();
                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            else if (!string.IsNullOrEmpty(constr))
            {
                return ExecuteDataTable(constr, cmdType, cmdText, commandParameters);
            }
            else
            {
                return ExecuteDataTable(connectionString, cmdType, cmdText, commandParameters);
            }
        }


        //
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    if (parm != null)
                    {
                        cmd.Parameters.Add(parm);
                    }
                }
            }
        }


        /// <summary>
        /// 创建输入参数
        /// </summary>
        public static SqlParameter MakeInputParam(string paramName, SqlDbType dbType, int size, object value)
        {
            if (value == null)
            {
                value = DBNull.Value;
            }

            if (value.GetType() == typeof(string) && string.IsNullOrEmpty(value.ToString()))
            {
                value = DBNull.Value;
            }

            SqlParameter par = new SqlParameter(paramName, dbType);
            if (size > 0)
            {
                par.Size = size;
            }
            par.Direction = ParameterDirection.Input;
            par.Value = value;
            return par;
        }


        /// <summary>
        /// 创建输出参数
        /// </summary>
        public static SqlParameter MakeOutputParam(string paramName, SqlDbType dbType, int size)
        {
            SqlParameter par = new SqlParameter(paramName, dbType, size);
            par.Direction = ParameterDirection.Output;
            return par;
        }

        /// <summary>
        /// 创建输出参数
        /// </summary>
        private static SqlParameter MakeOutputParam(string paramName, SqlDbType dbType, byte precision, byte scale)
        {
            SqlParameter par = new SqlParameter(paramName, dbType);
            par.Direction = ParameterDirection.Output;
            par.Precision = precision;
            par.Scale = scale;

            return par;
        }

        /// <summary>
        /// 构造查询的列的字符串
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        private static string MakeSelectColumnString(List<TableColumn> columns)
        {
            //构造查询的列
            string selectColumns = "*";
            if (columns != null && columns.Count > 0)
            {
                selectColumns = "";
                for (int i = 0; i < columns.Count; i++)
                {
                    if (i > 0)
                    {
                        selectColumns += ",";
                    }

                    selectColumns += "[" + columns[i].ColumnName + "]";
                }
            }

            return selectColumns;
        }


        /// <summary>
        /// 根据数据库锁类型获取锁sql字符串
        /// </summary>
        /// <param name="lockType"></param>
        /// <returns></returns>
        private static string GetDbLockString(DbLock lockType)
        {
            switch (lockType)
            {
                case DbLock.UpdLock:
                    return " with(updlock) ";
                case DbLock.RowLock:
                    return " with(rowlock) ";
                case DbLock.None:
                case DbLock.NoLock:
                default:
                    return " with(nolock) ";
            }
        }





        /////////// 新版 Model 数据访问 //////////////////////
        private void SetDbRawSql(Db m, string sql, SqlParameter[] par)
        {
            sql = sql.Trim();
            m.RawSql = sql;
        }

        private SqlParameter MakeMakeInputParamByObjectValue(string paraName, Object value)
        {
            SqlDbType dbType = SqlDbType.VarChar;
            int size = 0;
            Type vType = value.GetType();
            if (vType == typeof(String))
            {
            }
            else if (vType == typeof(Int32))
            {
                dbType = SqlDbType.Int;
                size = 4;
            }
            else if (vType == typeof(Int64))
            {
                dbType = SqlDbType.BigInt;
                size = 8;
            }
            else if (vType == typeof(DateTime))
            {
                dbType = SqlDbType.DateTime;
                size = 8;
            }

            return MakeInputParam(paraName, dbType, size, value);
        }
        /// <summary>
        /// 根据Join获取sql字符串
        /// </summary>
        /// <param name="joinType"></param>
        /// <returns></returns>
        private string getJoinTypeSqlStr(JoinType joinType)
        {
            switch (joinType)
            {
                case JoinType.Inner:
                    return "inner join";
                case JoinType.Left:
                    return "left join";
                case JoinType.Right:
                    return "right join";
                default:
                    return "inner join";
            }
        }
        /// <summary>
        /// 构造where语句于参数
        /// </summary>
        /// <param name="m"></param>
        /// <param name="parArr"></param>
        /// <returns></returns>
        private string makeWhereAndParams(Db m, out SqlParameter[] parArr)
        {
            return makeWhereAndParams(m.pWhereSql, m.WhereParams, out parArr);
        }

        /// <summary>
        /// 构造where语句于参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <param name="parArr"></param>
        /// <returns></returns>
        private string makeWhereAndParams(string sql, object[] data, out SqlParameter[] parArr)
        {
            string where = sql;
            //  (?<=(\s+like|=|>|<)\s*)\?
            //  (?<=(\s+like|in|=|>|<)\s*)\?
            var regstr = @"(?<=(\s+like|in|=|>|<)[\s\(]*)\?";
            var regstr2 = @"(\s+like|in|=|>|<)(?=[\s\(]*\?)";
            Regex r = new Regex(regstr, RegexOptions.IgnoreCase);
            MatchCollection coll = r.Matches(where);
            MatchCollection coll2 = new Regex(regstr2, RegexOptions.IgnoreCase).Matches(where);

            //参数不足
            if (data.Length < coll.Count)
            {
                throw new Exception("语句提供的查询参数不足 " + where + "  需要个" + coll.Count + "参数，只提供了" + data.Length + "个");
            }
            //parArr = new SqlParameter[coll.Count];
            List<SqlParameter> listParameter = new List<SqlParameter>();
            for (int i = 0; i < coll.Count; i++)
            {
                string symbol = coll2[i].Value.ToLower();
                //IN 或则 not in 查询
                if (symbol.Contains("in"))
                {
                    object[] inValues = (object[])data[i];
                    if (inValues != null && inValues.Length >= 0)
                    {
                        string paraNameGroup = "";
                        for (int j = 0; j < inValues.Length; j++)
                        {
                            string paraName = "@Param_" + i.ToString() + "_" + j.ToString();
                            if (j > 0)
                            {
                                paraNameGroup += ",";
                            }
                            paraNameGroup += paraName;
                            listParameter.Add(MakeMakeInputParamByObjectValue(paraName, inValues[j]));
                        }
                        where = r.Replace(where, paraNameGroup, 1);
                    }
                    else
                    {
                        throw new Exception("查询条件 " + symbol + "  至少需要提供1个值");
                    }
                }
                else
                {
                    string paraName = "@Param_" + i.ToString();
                    where = r.Replace(where, paraName, 1);
                    //parArr[i] = MakeMakeInputParamByObjectValue(paraName, data[i]);
                    listParameter.Add(MakeMakeInputParamByObjectValue(paraName, data[i]));
                }           
            }

            parArr = listParameter.ToArray();
            return where;
        }

        /// <summary>
        /// 根据模型进行查询
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override IDataReader Select(Db m)
        {
            bool isPageSelect = m.PageParam != null;      //是否分页查询

            //构造查询的列
            string selectColumns = string.IsNullOrEmpty(m.SelectFields) ? "*" : m.SelectFields; //MakeSelectColumnString(columns);

            //分页查询，则增加 row_number
            if (isPageSelect && string.IsNullOrEmpty(m.OrderByField))
            {
                throw new Exception("查询异常：分页查询未指定排序字段");
            }

            //1.构造Sql语句
            string selectTopStr = m.SelectLimit > 0 ? " top " + m.SelectLimit : "";
            string sql = " from [" + m.m_TableName + "]";
            if (!string.IsNullOrEmpty(m.TableAs))
            {
                sql += " as " + m.TableAs;
            }
            sql += GetDbLockString(m.LockType);

            //2.处理Join
            if (m.Joins.Count > 0)
            {
                foreach (Join join in m.Joins)
                {
                    //若无特殊字符，则认为是表名称，否则认为是子查询
                    if (Regex.Match(join.Table, "^[A-Za-z0-9_]+$").Success)
                    {
                        sql += " " + getJoinTypeSqlStr(join.JoinType) + " [" + join.Table + "]";
                        if (!string.IsNullOrEmpty(join.TableAs))
                        {
                            sql += " as " + join.TableAs;
                        }
                        sql += " with(nolock) on " + join.On;
                    }
                    else
                    {
                        sql += " " + getJoinTypeSqlStr(join.JoinType) + join.Table;
                        if (!string.IsNullOrEmpty(join.TableAs))
                        {
                            sql += " as " + join.TableAs;
                        }
                        sql += " on " + join.On;
                    }
                }
            }

            SqlParameter[] parArr = null;

            //2.查询条件
            if (!string.IsNullOrEmpty(m.pWhereSql))
            {
                string wh = makeWhereAndParams(m, out parArr);
                if (!string.IsNullOrEmpty(wh))
                {
                    sql += " where " + wh;
                }                
            }

            //3.处理分组
            if (!string.IsNullOrEmpty(m.GroupByField))
            {
                sql += " group by " + m.GroupByField;
            }

            //4.处理排序-非分页查询，则增加排序字段
            if (!isPageSelect)
            {
                if (!string.IsNullOrEmpty(m.OrderByField))
                {
                    sql += " order by " + m.OrderByField;
                }

                sql = "select " + selectTopStr + " " + selectColumns + sql;
            }
            //若为分页查询，则将sql重新组装
            else
            {
                //简单分页不进行数据统计
                if (!m.PageParam.IsSimplePage)
                {
                    string selectStatisticField = "count(1) _total";    //统计字段
                    if (!string.IsNullOrEmpty(m.PageParam.StatisticFields))
                    {
                        selectStatisticField += "," + m.PageParam.StatisticFields;
                    }

                    //查询记录数-And 一些其他的统计
                    string sql2 = "select " + selectStatisticField + sql;

                    //将执行的SQL返回到查询对象
                    SetDbRawSql(m, sql2, null);

                    DataTable dt = ExecuteDataTable(connectionString, CommandType.Text, sql2, parArr);
                    int _total = dt.Rows[0]["_total"].ToInt32();
                    int _pageCount = _total / m.PageParam.Size + (_total % m.PageParam.Size > 0 ? 1 : 0);
                    m.PageParam.Total = _total;
                    m.PageParam.Count = _pageCount;
                    //处理统计数据
                    if (dt.Columns.Count > 1)
                    {
                        m.PageParam.StatisticResult = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (!"_total".Equals(col.ColumnName))
                            {
                                m.PageParam.StatisticResult.Add(col.ColumnName, dt.Rows[0][col.ColumnName]);
                            }
                        }
                    }
                }

                //拼装最终的sql - row_number
                selectColumns += ",ROW_NUMBER() over(order by " + m.OrderByField + ") as _row_id";
                sql = "select " + selectColumns + sql;
                sql = "select top " + m.PageParam.Size + " * from (" + sql + ")T where _row_id>" + (m.PageParam.Size * (m.PageParam.Index - 1));
            }

            //将执行的SQL返回到查询对象
            SetDbRawSql(m, sql, parArr);

            //若有事务，则在事务下面执行: 所用链接优先级别：事务 > 指定连接 > 指定连接字符串 > 默认连接字符串
            return ExecuteReader(m.DbTransaction, m.DbConnection, m.DbConnectionString, CommandType.Text, sql, parArr);
        }

        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="m"></param>
        /// <param name="data">要修改的数据库列</param>
        /// <returns></returns>
        public override int Update(Db m, NameObject[] data)
        {
            StringBuilder s = new StringBuilder();
            s.Append("update [" + m.m_TableName + "] set ");
            for (int i = 0; i < data.Length; i++)
            {
                if (i > 0)
                {
                    s.Append(",");
                }
                string parName = "@" + data[i].Name;
                s.Append("[" + data[i].Name + "]=" + parName);
            }

            //2.查询条件
            SqlParameter[] parArr = null;
            if (!string.IsNullOrEmpty(m.pWhereSql))
            {
                string wh = makeWhereAndParams(m, out parArr);
                if (!string.IsNullOrEmpty(wh))
                {
                    s.Append(" where " + wh);
                }
            }
            
            //3.最终sql的构造参数[组合更新列和where列]
            SqlParameter[] par = new SqlParameter[data.Length + parArr.Length];
            for (int i = 0; i < data.Length; i++)
            {
                string parName = "@" + data[i].Name;
                par[i] = MakeMakeInputParamByObjectValue(parName, data[i].Value);
            }
            for (int i = 0; i < parArr.Length; i++)
            {
                par[data.Length + i] = parArr[i];
            }

            //字符串
            string sql = s.ToString();

            //将执行的SQL返回到查询对象
            SetDbRawSql(m, sql, par);

            return ExecuteNonQuery(m.DbTransaction, m.DbConnection, m.DbConnectionString, CommandType.Text, sql, par);
        }


        /// <summary>
        /// 执行插入操作
        /// </summary>
        /// <param name="m"></param>
        /// <param name="data"></param>
        /// <param name="isGetId">是否获取identity</param>
        /// <returns></returns>
        public override long Insert(Db m, NameObject[] data, bool isGetId)
        {
            StringBuilder s = new StringBuilder();

            StringBuilder s1 = new StringBuilder();     //字段名称
            StringBuilder s2 = new StringBuilder();     //值的参数
            SqlParameter[] par = new SqlParameter[isGetId ? data.Length + 1 : data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                if (i > 0)
                {
                    s1.Append(",");
                    s2.Append(",");
                }
                string parName = "@" + data[i].Name;
                s1.Append("[" + data[i].Name + "]");
                s2.Append(parName);
                par[isGetId ? i + 1 : i] = MakeMakeInputParamByObjectValue(parName, data[i].Value);
            }

            s.Append("insert into [" + m.m_TableName + "] (" + s1.ToString() + ") values(" + s2.ToString() + ")");

            //获取自增ID
            if (isGetId)
            {
                s.Append(" set @identity=@@identity");
                par[0] = MakeOutputParam("@identity", SqlDbType.BigInt, 8);
            }

            //生成执行的sql
            string sql = s.ToString();
            int j;  //影响的行数

            //将执行的SQL返回到查询对象
            SetDbRawSql(m, sql, par);

            j = ExecuteNonQuery(m.DbTransaction, m.DbConnection, m.DbConnectionString, CommandType.Text, sql, par);

            //若获取自增ID
            if (isGetId)
            {
                return Convert.ToInt64(par[0].Value);
            }
            else
            {
                return j;
            }
        }


        /// <summary>
        /// 执行原生的Sql
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="conn">数据库连接</param>
        /// <param name="constr">连接字符串</param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override int Execute(Transaction trans, DbConnection conn, string constr, string sql, params object[] parameters)
        {
            SqlParameter[] par = null;
            sql = makeWhereAndParams(sql, parameters, out par);
            return ExecuteNonQuery(trans, conn, connectionString, CommandType.Text, sql, par);
        }



        /// <summary>
        /// 执行原生的查询
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="conn">数据库连接</param>
        /// <param name="constr">连接字符串</param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        public override IDataReader ExecuteSelect(Transaction trans, DbConnection conn, string constr, string sql, params object[] parameters)
        {
            SqlParameter[] par = null;
            sql = makeWhereAndParams(sql, parameters, out par);
            return ExecuteReader(trans, conn, constr, CommandType.Text, sql, par);
        }
        

    }
}
