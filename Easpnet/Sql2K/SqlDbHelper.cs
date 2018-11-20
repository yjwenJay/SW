using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Local;

namespace Easpnet.Sql2K
{
    /*
    public class SqlDbHelper : DbHelper
    {
        public static readonly string connectionString = LocalConfig.ConnectionString;

        /// <summary>
        /// 获取一个连接
        /// </summary>
        public override System.Data.Common.DbConnection Connection 
        {
            get
            {
                return new SqlConnection(SqlDbHelper.connectionString);
            }
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
            s.Append(" set @Id=@@identity");



            //构造参数
            SqlParameter[] par = new SqlParameter[columns.Count + 1];
            par[0] = MakeOutputParam("@Id", SqlDbType.BigInt, 4);
            for (int i = 0; i < columns.Count; i++)
            {
                Easpnet.TableColumn cl=columns[i];
                object val = cl.IsIdentifier ? null : cl.Value;
                par[i + 1] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, val);
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
            s.Append(" set @Id=@@identity");



            //构造参数
            SqlParameter[] par = new SqlParameter[columns.Count + 1];
            par[0] = MakeOutputParam("@Id", SqlDbType.Int, 4);
            for (int i = 0; i < columns.Count; i++)
            {
                Easpnet.TableColumn cl = columns[i];
                object val = cl.IsIdentifier ? null : cl.Value;
                par[i + 1] = MakeInputParam("@" + cl.ColumnName, ConvertDbType(cl.DbType), cl.Size, val);
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

            //执行操作
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
            //构造查询的列
            string selectColumns = MakeSelectColumnString(columns);

            //构造Sql语句
            StringBuilder s = new StringBuilder();
            s.Append("select top 1 " + selectColumns + " from [" + tableName + "]");
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
            //
            return ExecuteReader(connectionString, CommandType.Text, sql, par);
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

            //调用存储过程的参数构造
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

            return ExecuteReader(connectionString, CommandType.StoredProcedure, "SingleTableCommonPager_V2", par);
        }
        

        /// <summary>
        /// 执行选择操作
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public override IDataReader SelectReader(string tableName, List<TableColumn> columns, string condition, string orderby)
        {
            //构造查询的列
            string selectColumns = MakeSelectColumnString(columns);

            //
            string sql = "select " + selectColumns + " from [" + tableName + "]";
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " where " + condition;
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                sql += " order by " + orderby;
            }

            return ExecuteReader(connectionString, CommandType.Text, sql, null);
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
            //构造查询的列
            string selectColumns = MakeSelectColumnString(columns);

            //构造Sql语句
            string sql = "select top " + count + " " + selectColumns + " from [" + tableName + "]";
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " where " + condition;
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                sql += " order by " + orderby;
            }

            return ExecuteReader(connectionString, CommandType.Text, sql, null);
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
            return ExecuteDataTable(connectionString, CommandType.Text,sql, null);
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
            int effectedCount = ExecuteNonQuery(connectionString, CommandType.StoredProcedure, proc.ProcedureName, par);

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
        }


        /// <summary>
        /// 执行存储过程，返回DataTable
        /// </summary>
        /// <returns>影响的行数</returns>
        public override DataTable ExecuteProcedureDataTable(Procedure proc)
        {
            //
            SqlParameter[] par = GetProcedureParameters(proc);

            //执行
            DataTable dt = ExecuteDataTable(connectionString, CommandType.StoredProcedure, proc.ProcedureName, par);

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
        /// <param name="proc"></param>
        /// <returns></returns>
        private SqlDataReader ExecuteProcedureDataReader(Procedure proc)
        {
            //
            SqlParameter[] par = GetProcedureParameters(proc);
            SqlDataReader dr = ExecuteReader(connectionString, CommandType.StoredProcedure, proc.ProcedureName, par);
            //输出赋值
            for (int i = 0; i < proc.OutParameters.Count; i++)
            {
                int j = i + proc.InParameters.Count;
                proc.OutParameters[i].Value = par[j].Value;
            }

            //返回值赋值
            proc.ReturnValue = par[proc.InParameters.Count + proc.OutParameters.Count].Value;
            return dr;
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
            try
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
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        private static int ExecuteNonQuery(Transaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            if (trans == null)
            {
                return ExecuteNonQuery(SqlDbHelper.connectionString, cmdType, cmdText, commandParameters);
            }
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();

                    PrepareCommand(cmd, (System.Data.SqlClient.SqlConnection)trans.Connection,
                        (System.Data.SqlClient.SqlTransaction)trans.DbTransaction, cmdType, cmdText, commandParameters);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
                catch (Exception e)
                {
                    throw e;
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
        private static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
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
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        private static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
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
        /// <param name="connection">an existing database connection</param>
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        private static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
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
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        private static DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
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
        private static SqlParameter MakeInputParam(string paramName, SqlDbType dbType, int size, object value)
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
        private static SqlParameter MakeOutputParam(string paramName, SqlDbType dbType, int size)
        {
            SqlParameter par = new SqlParameter(paramName, dbType, size);
            par.Direction = ParameterDirection.Output;            
            return par;
        }

        /// <summary>
        /// 创建输出参数
        /// </summary>
        private static SqlParameter MakeOutputParam(string paramName, SqlDbType dbType, byte precision)
        {
            SqlParameter par = new SqlParameter(paramName, dbType);
            par.Direction = ParameterDirection.Output;
            par.Precision = precision;
            return par;
        }

        /// <summary>
        /// 创建输出参数
        /// </summary>
        private static SqlParameter MakeOutputParam(string paramName, SqlDbType dbType, byte precision, byte scale)
        {
            SqlParameter par = new SqlParameter(paramName, dbType);
            par.Direction = ParameterDirection.Output;
            par.Precision = 18;
            par.Scale = 18;
            par.Size = 18;

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
    }*/
}
