using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Caching;
using System.Reflection;
using Local;

namespace Easpnet.Modules
{
    /// <summary>
    /// 所有实体对象的基类
    /// </summary>
    public class ModelBase : Record, ICloneable
    {
        /// <summary>
        /// 数据库锁定方式
        /// </summary>
        protected DbLock DbLockType
        {
            get;
            set;
        }
        /// <summary>
        /// 数据库事务
        /// </summary>
        protected Transaction DbTransaction
        {
            get;
            set;
        }



        /// <summary>
        /// 获取缓存依赖
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public CacheDependency Dependency
        {
            get
            {
                return null;

                /*
                //如果数据库为SQL Server则只用SQL缓存依赖
                switch (LocalConfig.DatabaseType)
                {
                    case "Sql":
                    case "Sql2K":
                        return new SqlCacheDependency("SqlCacheDependencyDatabase", GetTableAttribute().m_TableName);
                    default:
                        string file = HttpContext.Current.Server.MapPath("~/Cache/Dependency/" + GetTableAttribute().m_TableName + ".log");
                        if (System.IO.File.Exists(file))
                        {
                            return new CacheDependency(file);
                        }
                        else
                        {
                            return null;
                        }
                }       */
            }
        }


        /// <summary>
        /// 本框架对缓存进行文件缓存依赖，当表数据有改变时，需要通过写文件来通知缓存。
        /// 确保Cache文件夹可写
        /// </summary>
        private void NotifyCache(string tablename)
        {
            /*
            switch (LocalConfig.DatabaseType)
            {
                case "Sql":
                case "Sql2K":
                    break;
                default:
                    string logpath = HttpContext.Current.Server.MapPath("~/Cache/Dependency/");
                    if (!System.IO.Directory.Exists(logpath))
                    {
                        System.IO.Directory.CreateDirectory(logpath);
                    }

                    //
                    System.IO.File.WriteAllText(logpath + tablename + ".log", DateTime.Now.ToString());
                    break;
            }*/
        }

        /// <summary>
        /// 设置数据库锁定方式，只针对一次Sql语句有效
        /// </summary>
        /// <returns></returns>
        public ModelBase Lock(Transaction trans, DbLock lockType)
        {
            this.DbTransaction = trans;
            this.DbLockType = lockType;
            return this;
        }

        /// <summary>
        /// 设置数据库锁定方式，只针对一次Sql语句有效
        /// </summary>
        /// <returns></returns>
        public ModelBase Lock(DbLock lockType)
        {
            this.DbTransaction = null;
            this.DbLockType = lockType;
            return this;
        }

        /// <summary>
        /// 设置数据库锁定方式为 Uplock，只针对一次Sql语句有效: 
        /// </summary>
        /// <returns></returns>
        public ModelBase Lock()
        {
            this.DbTransaction = null;
            this.DbLockType = DbLock.UpdLock;
            return this;
        }


        /// <summary>
        /// 新建数据
        /// </summary>
        /// <returns></returns>
        public virtual long Create()
        {
            TableAttribute table = GetTableAttribute();
            if (table != null)
            {
                List<TableColumn> columns = GetTableColumns();
                long id = Database.Insert(table.TableName, columns);
                TableColumn idColumn = columns.Find(s => s.IsIdentifier);
                if (idColumn != null)
                {
                    System.Reflection.PropertyInfo info = this.GetType().GetProperty(idColumn.PropertyName);
                    if (info != null && info.CanWrite)
                    {
                        if (idColumn.DbType == DbType.Int32)
                        {
                            info.SetValue(this, id.ToInt32(), null);
                        }
                        else
                        {
                            info.SetValue(this, id, null);
                        }
                    }
                }
                return id;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 在指定的事务中执行新建数据
        /// </summary>
        /// <returns></returns>
        public virtual long Create(Transaction trans)
        {
            TableAttribute table = GetTableAttribute();
            if (table != null)
            {
                List<TableColumn> columns = GetTableColumns();
                long id = Database.Insert(trans, table.TableName, columns);
                TableColumn idColumn = columns.Find(s => s.IsIdentifier);
                if (idColumn != null)
                {
                    System.Reflection.PropertyInfo info = this.GetType().GetProperty(idColumn.PropertyName);
                    if (info != null && info.CanWrite)
                    {
                        if (idColumn.DbType == DbType.Int32)
                        {
                            info.SetValue(this, id.ToInt32(), null);
                        }
                        else
                        {
                            info.SetValue(this, id, null);
                        }
                    }
                }
                return id;
            }
            else
            {
                return -1;
            }
        }



        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        public virtual bool Update()
        {
            TableAttribute table = GetTableAttribute();
            if (table != null)
            {
                bool succ = Database.Update(table.TableName, GetNotPrimaryKeyColumns(), GetPrimaryKeyColumns());
                //if (succ && table.EnableCacheDependency)
                //{
                //    NotifyCache(table.m_TableName);
                //}

                return succ;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 在指定的事务中执行更新数据
        /// </summary>
        /// <returns></returns>
        public virtual bool Update(Transaction trans)
        {
            TableAttribute table = GetTableAttribute();
            if (table != null)
            {
                bool succ = Database.Update(trans, table.TableName, GetNotPrimaryKeyColumns(), GetPrimaryKeyColumns());
                //if (succ && table.EnableCacheDependency)
                //{
                //    NotifyCache(table.m_TableName);
                //}

                return succ;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 根据条件将所有的数据进行更新数据
        /// </summary>
        /// <param name="fieldName">要更新的字段</param>
        /// <param name="val">更新字段的值</param>
        /// <returns></returns>
        public virtual int Update(string fieldName, object val)
        {
            NameObject[] fields = new NameObject[] { new NameObject(fieldName, val) };
            return Update(null, fields);
        }


        /// <summary>
        /// 在指定的事务中，根据条件将所有的数据进行更新数据
        /// </summary>
        /// <param name="trans">指定事务</param>
        /// <param name="fieldName">要更新的字段</param>
        /// <param name="val">更新字段的值</param>
        /// <returns></returns>
        public virtual int Update(Transaction trans, string fieldName, object val)
        {
            NameObject[] fields = new NameObject[] { new NameObject(fieldName, val) };
            return Update(trans, null, fields);
        }

        /// <summary>
        /// 根据条件更新数据
        /// </summary>
        /// <param name="fieldName">要更新的字段</param>
        /// <param name="val">更新字段的值</param>
        /// <param name="q">更新条件</param>
        /// <returns></returns>
        public virtual int Update(string fieldName, object val, Query q)
        {
            NameObject[] fields = new NameObject[] { new NameObject(fieldName, val) };
            return Update(q, fields);
        }



        /// <summary>
        /// 在指定的事务中，根据条件更新字段值
        /// </summary>
        /// <param name="trans">指定事务</param>
        /// <param name="fieldName">要更新的字段</param>
        /// <param name="val">更新字段的值</param>
        /// <param name="q">更新条件</param>
        /// <returns></returns>
        public virtual int Update(Transaction trans, string fieldName, object val, Query q)
        {
            NameObject[] fields = new NameObject[] { new NameObject(fieldName, val) };
            return Update(trans, q, fields);
        }


        /// <summary>
        /// 根据条件更新数据
        /// </summary>
        /// <returns></returns>
        public virtual int Update(Query q, params NameObject[] fields)
        {
            List<TableColumn> lst = ConvertNameObjectToFieldColumn(fields);
            if (lst.Count == 0)
            {
                throw new Exception("更新的列集合中没有包含任何列，请检查字段是否正确！");
            }

            TableAttribute table = GetTableAttribute();
            if (table != null)
            {
                int cnt = Database.Update(table.TableName, lst, q); ;
                //if (cnt > 0 && table.EnableCacheDependency)
                //{
                //    NotifyCache(table.m_TableName);
                //}

                return cnt;
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 在指定的事务中，根据条件进行更新指定的列
        /// </summary>
        /// <param name="q">要更新的数据的条件</param>
        /// <param name="list">要更新的字段集合</param>
        /// <returns></returns>
        public virtual int Update(Query q, List<NameObject> list)
        {
            return this.Update(q, list.ToArray());
        }



        /// <summary>
        /// 在指定的事务中，根据条件进行更新指定的列
        /// </summary>
        /// <param name="trans">指定事务</param>
        /// <param name="q">要更新的数据的条件</param>
        /// <param name="fields">要更新的字段集合</param>
        /// <returns></returns>
        public virtual int Update(Transaction trans, Query q, params NameObject[] fields)
        {
            List<TableColumn> lst = ConvertNameObjectToFieldColumn(fields);
            if (lst.Count == 0)
            {
                throw new Exception("更新的列集合中没有包含任何列，请检查字段是否正确！");
            }

            TableAttribute table = GetTableAttribute();
            if (table != null)
            {
                int cnt = Database.Update(trans, table.TableName, lst, q); ;
                //if (cnt > 0 && table.EnableCacheDependency)
                //{
                //    NotifyCache(table.m_TableName);
                //}

                return cnt;
            }
            else
            {
                return 0;
            }
        }



        /// <summary>
        /// 在指定的事务中，根据条件进行更新指定的列
        /// </summary>
        /// <param name="trans">指定事务</param>
        /// <param name="q">要更新的数据的条件</param>
        /// <param name="list">要更新的字段集合</param>
        /// <returns></returns>
        public virtual int Update(Transaction trans, Query q, List<NameObject> list)
        {
            return this.Update(trans, q, list.ToArray());
        }



        /// <summary>
        /// 将某个字段进行数字的累加
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <param name="query">条件</param>
        /// <returns></returns>
        public virtual int Cumulative(string propertyName, double num, Query query)
        {
            TableAttribute table = GetTableAttribute();
            if (table != null)
            {
                int afect = Database.Cumulative(table.TableName, ConvertPropertyNameToFieldName(propertyName), num, query);
                //if (afect > 0 && table.EnableCacheDependency)
                //{
                //    NotifyCache(table.m_TableName);
                //}

                return afect;
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// 在指定的事务中，将某个字段进行数字的累加
        /// </summary>
        /// <param name="trans">指定事务</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <param name="query">条件</param>
        /// <returns></returns>
        public virtual int Cumulative(Transaction trans, string propertyName, double num, Query query)
        {
            TableAttribute table = GetTableAttribute();
            if (table != null)
            {
                int afect = Database.Cumulative(trans, table.TableName, ConvertPropertyNameToFieldName(propertyName), num, query);
                //if (afect > 0 && table.EnableCacheDependency)
                //{
                //    NotifyCache(table.m_TableName);
                //}

                return afect;
            }
            else
            {
                return -1;
            }
        }




        /// <summary>
        /// 将某个字段进行数字的累加，条件为主键给定的条件
        /// </summary>
        /// <param name="propertyName">字段名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <returns></returns>
        public virtual int Cumulative(string propertyName, double num)
        {
            TableAttribute table = GetTableAttribute();
            if (table != null)
            {
                int afect = Database.Cumulative(table.TableName, ConvertPropertyNameToFieldName(propertyName), num, GetPrimaryKeyColumns());
                //if (afect > 0 && table.EnableCacheDependency)
                //{
                //    NotifyCache(table.m_TableName);
                //}

                return afect;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 在指定的事务中，将某个字段进行数字的累加，条件为给定的条件
        /// </summary>
        /// <param name="trans">指定事务</param>
        /// <param name="propertyName">字段名称</param>
        /// <param name="num">累加的数字，可为负数</param>
        /// <returns></returns>
        public virtual int Cumulative(Transaction trans, string propertyName, double num)
        {
            TableAttribute table = GetTableAttribute();
            if (table != null)
            {
                int afect = Database.Cumulative(trans, table.TableName, ConvertPropertyNameToFieldName(propertyName), num, GetPrimaryKeyColumns());
                //if (afect > 0 && table.EnableCacheDependency)
                //{
                //    NotifyCache(table.m_TableName);
                //}

                return afect;
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public virtual bool Delete()
        {
            TableAttribute table = GetTableAttribute();
            if (table != null)
            {
                bool succ = Database.Delete(table.TableName, GetPrimaryKeyColumns());
                //if (succ && table.EnableCacheDependency)
                //{
                //    NotifyCache(table.m_TableName);
                //}

                return succ;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 在指定的事务中删除数据
        /// </summary>
        /// <param name="trans">指定事务</param>
        /// <returns></returns>
        public virtual bool Delete(Transaction trans)
        {
            TableAttribute table = GetTableAttribute();
            if (table != null)
            {
                bool succ = Database.Delete(trans, table.TableName, GetPrimaryKeyColumns());
                //if (succ && table.EnableCacheDependency)
                //{
                //    NotifyCache(table.m_TableName);
                //}

                return succ;
            }
            else
            {
                return false;
            }
        }




        /// <summary>
        /// 删除query条件中指定的记录
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual int Delete(Query query)
        {
            TableAttribute table = GetTableAttribute();
            if (table != null)
            {
                int afect = Database.Delete(table.TableName, query);
                //if (afect > 0 && table.EnableCacheDependency)
                //{
                //    NotifyCache(table.m_TableName);
                //}

                return afect;
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// 在指定的事务中,根据query条件删除记录
        /// </summary>
        /// <param name="trans">指定事务</param>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public virtual int Delete(Transaction trans, Query query)
        {
            TableAttribute table = GetTableAttribute();
            if (table != null)
            {
                int afect = Database.Delete(trans, table.TableName, query);
                //if (afect > 0 && table.EnableCacheDependency)
                //{
                //    NotifyCache(table.m_TableName);
                //}

                return afect;
            }
            else
            {
                return -1;
            }
        }




        /// <summary>
        /// 获取一个实体模型
        /// </summary>
        /// <returns></returns>
        public virtual bool GetModel()
        {
            bool finded = false;
            IDataReader dr = Database.GetModelReader(GetTableName(), MakeSelectTableColumns(), GetPrimaryKeyColumns(), this.DbLockType, this.DbTransaction);
            this.DbLockType = DbLock.None;  //锁类型用完即销毁
            this.DbTransaction = null;
            if (dr.Read())
            {
                finded = true;
                MakeSelfFromIDataReader(dr);
            }

            dr.Close();
            return finded;
        }



        /// <summary>
        /// 获取一个实体模型，置顶要选择的列
        /// </summary>
        /// <param name="field">要查询的第一个字段名称</param>
        /// <param name="fields">要查询的多个字段集合</param>
        /// <returns>若查询到数据返回true，否则返回false</returns>
        public virtual bool GetModel(string field, params string[] fields)
        {
            //构造一个Query，来封装要选择的列
            Query q = Query.NewQuery();
            q.Select(field);
            foreach (string item in fields)
            {
                q.Select(item);
            }

            //
            bool finded = false;
            IDataReader dr = Database.GetModelReader(GetTableName(), MakeSelectTableColumns(q), GetPrimaryKeyColumns(), this.DbLockType, this.DbTransaction);
            this.DbLockType = DbLock.None;  //锁类型用完即销毁
            this.DbTransaction = null;
            if (dr.Read())
            {
                finded = true;
                MakeSelfFromIDataReader(dr);
            }

            dr.Close();
            return finded;
        }



        /// <summary>
        /// 根据查询条件获取实体模型
        /// </summary>
        /// <param name="query"></param>
        /// <returns>若查询到数据返回true，否则返回false</returns>
        public virtual bool GetModel(Query query)
        {
            bool finded = false;
            Type t = this.GetType();
            IDataReader dr = Database.SelectReader(GetTableName(), MakeSelectTableColumns(query), query, MakeOrderByString(query), 1, this.DbLockType, this.DbTransaction);
            this.DbLockType = DbLock.None;  //锁类型用完即销毁
            this.DbTransaction = null;
            if (dr.Read())
            {
                finded = true;
                MakeSelfFromIDataReader(dr);
            }

            dr.Close();
            return finded;
        }


        /// <summary>
        /// 获取集合
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList<T>() where T : ModelBase
        {
            Query query = Query.NewQuery();
            return GetList<T>(query);
        }


        /// <summary>
        /// 获取集合，并制定查询的列的列表
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList<T>(string field, params string[] fields) where T : ModelBase
        {
            Query q = Query.NewQuery();
            q.Select(field);
            foreach (string item in fields)
            {
                q.Select(item);
            }
            return GetList<T>(q);
        }


        /// <summary>
        /// 获取集合
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList<T>(Query query) where T : ModelBase
        {
            List<T> lst = new List<T>();
            IDataReader dr = Database.SelectReader(GetTableName(), MakeSelectTableColumns(query), query, MakeOrderByString(query), this.DbLockType, this.DbTransaction);
            this.DbLockType = DbLock.None;  //锁类型用完即销毁
            this.DbTransaction = null;
            while (dr.Read())
            {
                lst.Add(MakeModelFromIDataReader<T>(dr));
            }
            dr.Close();
            return lst;
        }


        /// <summary>
        /// 根据条件获取前cout条记录
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList<T>(Query query, int count) where T : ModelBase
        {
            List<T> lst = new List<T>();
            //查询的列集合构造
            List<TableColumn> selectColumns = MakeSelectTableColumns(query);
            IDataReader dr = Database.SelectReader(GetTableName(), selectColumns, query, MakeOrderByString(query), count, this.DbLockType, this.DbTransaction);
            this.DbLockType = DbLock.None;  //锁类型用完即销毁
            this.DbTransaction = null;
            while (dr.Read())
            {
                lst.Add(MakeModelFromIDataReader<T>(dr));
            }
            dr.Close();
            return lst;
        }


        /// <summary>
        /// 根据查询对象和分页参数获取集合对象
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <param name="page">分页参数</param>
        /// <returns></returns>
        public virtual List<T> GetList<T>(Query query, ref PageParam page) where T : ModelBase
        {
            List<T> lst = new List<T>();
            IDataParameter total;                                                         //输出参数,总记录数
            IDataParameter count;                                                         //输出参数,总页数

            //排序
            string orderby = MakeOrderByString(query);

            //查询的列集合构造
            List<TableColumn> selectColumns = MakeSelectTableColumns(query);


            //进行查询
            IDataReader dr = Database.SelectReader(GetTableName(), selectColumns, GetIdentifierColumnName(), query, orderby, page, out total, out count);
            while (dr.Read())
            {
                lst.Add(MakeModelFromIDataReader<T>(dr));
            }

            dr.Close();

            //输出参数赋值 DataReader 关闭后，才能获取输出参数的值
            page.Count = TypeConvert.ToInt32(count.Value);
            page.Total = TypeConvert.ToInt32(total.Value);

            return lst;
        }

        /// <summary>
        /// 根据条件进行统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual int Count(Query query)
        {
            return Database.SelectCount(GetTableName(), query);
        }


        /// <summary>
        /// 根据条件统计总数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual double Sum(Query query, string propertyName)
        {
            return Database.SelectSum(GetTableName(), ConvertPropertyNameToFieldName(propertyName), query);
        }


        /// <summary>
        /// 根据条件统计最大值
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual double Max(Query query, string propertyName)
        {
            return Database.SelectMax(GetTableName(), ConvertPropertyNameToFieldName(propertyName), query);
        }


        /// <summary>
        /// 根据条件统计最小值
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual double Min(Query query, string propertyName)
        {
            return Database.SelectMin(GetTableName(), ConvertPropertyNameToFieldName(propertyName), query);
        }


        /// <summary>
        /// 分组查询。
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual DataTable SelectTable(Query query, int count)
        {
            //
            return Database.SelectTable(GetTableName(), query, count);
        }

        /// <summary>
        /// 查询DataTable，可用于比较复杂的查询。
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual DataTable SelectTable(Query query)
        {
            //
            DataTable dt = Database.SelectTable(GetTableName(), query, 0, this.DbLockType, this.DbTransaction);
            this.DbLockType = DbLock.None;
            this.DbTransaction = null;
            return dt;
        }


        /// <summary>
        /// 获取该实体对象的列的集合
        /// </summary>
        /// <returns></returns>
        private List<TableColumn> GetTableColumns()
        {
            //
            Type type = this.GetType();
            List<TableColumn> lst = new List<TableColumn>();
            //
            System.Reflection.PropertyInfo[] fields = GetGetProperties(type);
            foreach (System.Reflection.PropertyInfo info in fields)
            {
                TableFieldAttribute att = Attribute.GetCustomAttribute(info, typeof(TableFieldAttribute)) as TableFieldAttribute;
                if (att != null && att.IsTableField)
                {
                    TableColumn column = new TableColumn();
                    //若没有特殊指定字段名称，表字段名和属性名一致
                    column.ColumnName = string.IsNullOrEmpty(att.TableFieldName) ? info.Name : att.TableFieldName;
                    column.Size = att.Size;
                    column.Value = info.GetValue(this, null);
                    column.IsIdentifier = att.IsIdentifier;
                    column.PropertyName = info.Name;

                    //若是枚举类型，则数据类型为Int32
                    if (info.PropertyType.IsEnum)
                    {
                        column.DbType = DbType.Int32;
                    }
                    else
                    {
                        //DbType
                        string fieldtype = info.PropertyType.ToString();
                        switch (fieldtype)
                        {
                            case "System.String":
                                column.DbType = DbType.AnsiString;
                                if (column.Value == null)
                                {
                                    column.Value = string.Empty;
                                }
                                break;
                            case "System.Int32":
                            case "System.Nullable`1[System.Int32]":
                                column.DbType = DbType.Int32;
                                break;
                            case "System.Int64":
                            case "System.Nullable`1[System.Int64]":
                                column.DbType = DbType.Int64;
                                break;
                            case "System.Boolean":
                                column.DbType = DbType.Boolean;
                                break;
                            case "System.Decimal":
                            case "System.Nullable`1[System.Decimal]":
                                column.DbType = DbType.Decimal;
                                break;
                            case "System.DateTime":
                            case "System.Nullable`1[System.DateTime]":
                                column.DbType = DbType.DateTime;
                                if (column.Value != null)
                                {
                                    column.Value = TypeConvert.ToDateTime(column.Value);
                                }

                                break;
                            default:
                                column.DbType = DbType.Binary;
                                break;
                        }
                    }
                    //

                    //
                    lst.Add(column);
                }
            }

            return lst;
        }

        /// <summary>
        /// 获取不含有值的列集合
        /// </summary>
        /// <returns></returns>
        private List<TableColumn> GetTableColumnsWithoutValue()
        {
            //
            Type type = this.GetType();
            string cach_name = "AllTableColumnsOf-" + type.AssemblyQualifiedName;

            //先从缓存中获取，获取不到再去进行反射读取
            List<TableColumn> lst;
            lst = HttpRuntime.Cache.Get(cach_name) as List<TableColumn>;
            if (lst != null)
            {
                return lst;
            }

            //构造对象
            lst = new List<TableColumn>();

            //
            System.Reflection.PropertyInfo[] fields = GetGetProperties(type);
            foreach (System.Reflection.PropertyInfo info in fields)
            {
                TableFieldAttribute att = Attribute.GetCustomAttribute(info, typeof(TableFieldAttribute)) as TableFieldAttribute;
                if (att != null && att.IsTableField)
                {
                    TableColumn column = new TableColumn();
                    column.PropertyName = info.Name;
                    //若没有特殊指定字段名称，表字段名和属性名一致
                    column.ColumnName = string.IsNullOrEmpty(att.TableFieldName) ? info.Name : att.TableFieldName;
                    column.Size = att.Size;
                    column.IsIdentifier = att.IsIdentifier;

                    //若是枚举类型，则数据类型为Int32
                    if (info.PropertyType.IsEnum)
                    {
                        column.DbType = DbType.Int32;
                    }
                    else
                    {
                        //DbType
                        string fieldtype = info.PropertyType.ToString();
                        switch (fieldtype)
                        {
                            case "System.String":
                                column.DbType = DbType.AnsiString;
                                break;
                            case "System.Int32":
                            case "System.Nullable`1[System.Int32]":
                                column.DbType = DbType.Int32;
                                break;
                            case "System.Int64":
                            case "System.Nullable`1[System.Int64]":
                                column.DbType = DbType.Int64;
                                break;
                            case "System.Boolean":
                                column.DbType = DbType.Boolean;
                                break;
                            case "System.Decimal":
                            case "System.Nullable`1[System.Decimal]":
                                column.DbType = DbType.Decimal;
                                break;
                            case "System.DateTime":
                            case "System.Nullable`1[System.DateTime]":
                                column.DbType = DbType.DateTime;
                                if (column.Value != null)
                                {
                                    column.Value = TypeConvert.ToDateTime(column.Value);
                                }

                                break;
                            default:
                                column.DbType = DbType.Binary;
                                break;
                        }
                    }
                    //

                    //
                    lst.Add(column);
                }
            }

            //加入缓存
            HttpRuntime.Cache.Add(cach_name, lst, null, DateTime.Now.AddHours(1.0), TimeSpan.Zero, CacheItemPriority.Normal, null);
            return lst;
        }


        /// <summary>
        ///  获取表名称
        /// </summary>
        /// <returns></returns>
        public virtual string GetTableName()
        {
            TableAttribute att = Attribute.GetCustomAttribute(this.GetType(), typeof(TableAttribute)) as TableAttribute;
            string table_name = att != null && !string.IsNullOrEmpty(att.TableName) ? att.TableName : this.GetType().Name;
            return table_name;
        }


        /// <summary>
        ///  获取表相关信息
        /// </summary>
        /// <returns></returns>
        public virtual TableAttribute GetTableAttribute()
        {
            TableAttribute att = Attribute.GetCustomAttribute(this.GetType(), typeof(TableAttribute)) as TableAttribute;
            if (att != null)
            {
                if (string.IsNullOrEmpty(att.TableName))
                {
                    att.TableName = this.GetType().Name;
                }
            }
            return att;
        }


        /// <summary>
        /// 获取主键列
        /// </summary>
        /// <returns></returns>
        public virtual string GetIdentifierColumnName()
        {
            List<TableColumn> lst = GetPrimaryKeyColumns();
            if (lst.Count == 0)
            {
                throw new Exception("没有指定主键列，不能进行分页查询！");
            }
            else
            {
                string str = "";
                for (int i = 0; i < lst.Count; i++)
                {
                    if (i > 0)
                    {
                        str += ",";
                    }

                    str += lst[i].ColumnName;
                }

                return str;
            }
        }

        /// <summary>
        /// 获取主键列集合
        /// </summary>
        /// <returns></returns>
        public virtual List<TableColumn> GetPrimaryKeyColumns()
        {
            Type type = this.GetType();
            List<TableColumn> lst_ret = new List<TableColumn>();

            //若没有缓存，则查找并存储缓存
            List<TableColumn> lst = GetTableColumns();
            TableAttribute att = Attribute.GetCustomAttribute(type, typeof(TableAttribute)) as TableAttribute;

            //将主键值进行分析“,”号分割
            string[] arr = att.PrimaryKey.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            //依次查找主键列
            foreach (string item in arr)
            {
                TableColumn col = lst.Find(f => f.ColumnName == item);
                if (col != null)
                {
                    lst_ret.Add(col);
                }
            }

            return lst_ret;
        }

        /// <summary>
        /// 获取非主键列集合
        /// </summary>
        /// <returns></returns>
        public virtual List<TableColumn> GetNotPrimaryKeyColumns()
        {
            List<TableColumn> lst = GetTableColumns();
            List<TableColumn> lst_primary = GetPrimaryKeyColumns();
            List<TableColumn> not_primary = new List<TableColumn>();

            int count = lst.Count;
            for (int i = 0; i < count; i++)
            {
                if (!lst[i].IsIdentifier)
                {
                    TableColumn md = lst_primary.Find(s => s.ColumnName == lst[i].ColumnName);
                    if (md == null)
                    {
                        not_primary.Add(lst[i]);
                    }
                }
            }

            return not_primary;
        }

        /// <summary>
        /// 构造实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private T MakeModelFromIDataReader<T>(IDataReader dr) where T : ModelBase
        {
            Type type = this.GetType();

            //
            T md = type.Assembly.CreateInstance(type.FullName) as T;

            if (md != null)
            {
                md.MakeSelfFromIDataReader(dr);
            }

            return md;
        }


        /// <summary>
        /// 通过IDataReader构造实体对象本身
        /// </summary>
        /// <param name="dr"></param>
        public virtual void MakeSelfFromIDataReader(IDataReader dr)
        {
            Type type = this.GetType();

            System.Reflection.PropertyInfo[] fields = GetGetProperties(type);

            //System.Reflection.PropertyInfo[] properties = GetGetProperties(type);
            //for (int i = 0; i < dr.FieldCount; i++)
            //{
            //    string fieldName = dr.GetName(i);
            //    foreach (System.Reflection.PropertyInfo info in properties)
            //    {
            //        TableFieldAttribute att = Attribute.GetCustomAttribute(info, typeof(TableFieldAttribute)) as TableFieldAttribute;
            //        if (att != null && att.IsTableField)
            //        {
            //            string fname = string.IsNullOrEmpty(att.TableFieldName) ? info.Name : att.TableFieldName;
            //            if (fname == fieldName)
            //            {
            //                object val = dr.GetValue(i);
            //                if (val != DBNull.Value)
            //                {
            //                    info.SetValue(this, val, null);
            //                }
            //            }
            //        }
            //    }
            //}

            foreach (System.Reflection.PropertyInfo info in fields)
            {
                TableFieldAttribute att = Attribute.GetCustomAttribute(info, typeof(TableFieldAttribute)) as TableFieldAttribute;
                if (att != null && att.IsTableField)
                {
                    if (info.CanWrite)
                    {
                        string field_name = string.IsNullOrEmpty(att.TableFieldName) ? info.Name : att.TableFieldName;  //若没有单独设置数据库的列名，则直接用属性名做为数据库的列名

                        try
                        {
                            if (dr[field_name] != DBNull.Value)
                            {
                                try
                                {
                                    info.SetValue(this, dr[field_name], null);
                                }
                                catch
                                {
                                    info.SetValue(this, Convert.ChangeType(dr[field_name], info.PropertyType.GetGenericArguments()[0]), null);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 构造排序的Sql字符串
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private string MakeOrderByString(Query query)
        {
            string orderby = "";
            Type type = this.GetType();
            System.Reflection.PropertyInfo[] properties = GetGetProperties(type);
            if (query.OrderByType != null)
            {
                foreach (QueryOrderField field in query.OrderByType.Fields)
                {
                    string fieldName = ConvertPropertyNameToFieldName(field.FieldName);
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        string orderType = field.OrderType == OrderType.ASC ? "asc" : "desc";
                        if (string.IsNullOrEmpty(orderby))
                        {
                            orderby += "[" + fieldName + "] " + orderType;
                        }
                        else
                        {
                            orderby += ",[" + fieldName + "] " + orderType;
                        }
                    }
                }
            }

            return orderby;
        }


        /// <summary>
        /// 将属性字段名称转换为数据库的字段名称
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private string ConvertPropertyNameToFieldName(string propertyName)
        {
            List<TableColumn> lst = GetTableColumnsWithoutValue();
            TableColumn c = lst.Find(s => s.PropertyName == propertyName);
            return c == null ? "" : c.ColumnName;
        }


        /// <summary>
        /// 转化为表格列集合
        /// </summary>
        /// <param name="nj"></param>
        /// <returns></returns>
        private List<TableColumn> ConvertNameObjectToFieldColumn(NameObject[] nj)
        {
            List<TableColumn> lst = GetTableColumnsWithoutValue();
            List<TableColumn> lst_return = new List<TableColumn>();
            foreach (NameObject item in nj)
            {
                TableColumn column = lst.Find(s => s.PropertyName == item.Name);
                if (column != null)
                {
                    column.Value = item.Value;

                    //日期类型特殊处理
                    if (column.DbType == DbType.DateTime)
                    {
                        column.Value = TypeConvert.ToDateTime(item.Value);
                    }
                    else if (column.DbType == DbType.Int32)
                    {
                        column.Value = TypeConvert.ToInt32(item.Value);
                    }

                    lst_return.Add(column);
                }
            }

            return lst_return;
        }



        /// <summary>
        /// 从对象中将数据填充到给定的TableColumn集合中
        /// </summary>
        /// <param name="lst"></param>
        private void FillDataFromObject(List<TableColumn> lst)
        {

        }


        /// <summary>
        /// 根据查询对象构造查询的列集合对象
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        private List<TableColumn> MakeSelectTableColumns(Query query)
        {
            List<TableColumn> columns = GetTableColumnsWithoutValue();
            if (query.SelectColumns == null || query.SelectColumns.Count == 0)
            {
                return columns;
            }
            else
            {
                List<TableColumn> lst = new List<TableColumn>();
                foreach (QuerySelectColumn item in query.SelectColumns)
                {
                    TableColumn tc = columns.Find(s => s.PropertyName == item.ColumnName);
                    if (tc != null)
                    {
                        lst.Add(tc);
                    }
                }

                if (lst.Count > 0)
                {
                    return lst;
                }
                else
                {
                    return columns;
                }
            }
        }

        /// <summary>
        /// 根据查询对象构造查询的列集合对象(返回所有的列)
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        private List<TableColumn> MakeSelectTableColumns()
        {
            return GetTableColumnsWithoutValue();
        }

        /// <summary>
        /// 获取所有的TableFieldAttribute
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<TableFieldAttribute> GetAllTableFieldList(Type type)
        {
            System.Reflection.PropertyInfo[] properties = GetGetProperties(type);
            List<TableFieldAttribute> lst = new List<TableFieldAttribute>();
            foreach (PropertyInfo item in properties)
            {
                TableFieldAttribute att = Attribute.GetCustomAttribute(item, typeof(TableFieldAttribute)) as TableFieldAttribute;
                if (att != null && att.IsTableField)
                {
                    lst.Add(att);
                }
            }

            return lst;
        }


        /// <summary>
        /// 获取树形数组
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private System.Reflection.PropertyInfo[] GetGetProperties(Type type)
        {
            System.Reflection.PropertyInfo[] properties = type.GetProperties();
            //List<PropertyInfo> lst = new List<PropertyInfo>();
            //foreach (PropertyInfo item in properties)
            //{
            //    if (item.DeclaringType == type)
            //    {
            //        lst.Add(item);
            //    }
            //}

            return properties;
        }

        #region ICloneable 成员

        /// <summary>
        /// 克隆一个对象的副本
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
