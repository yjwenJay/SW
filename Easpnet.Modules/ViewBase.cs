using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.Caching;

namespace Easpnet.Modules
{
    /// <summary>
    /// 所有视图对象的基类
    /// </summary>
    public class ViewBase
    {
        /// <summary>
        /// 获取一个实体模型
        /// </summary>
        /// <returns></returns>
        public virtual bool GetModel()
        {
            bool finded = false;
            IDataReader dr = Database.GetModelReader(GetTableName(), MakeSelectTableColumns(), GetPrimaryKeyColumns());
            if (dr.Read())
            {
                finded = true;
                PopulateSelfFromIDataReader(dr);
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
            IDataReader dr = Database.GetModelReader(GetTableName(), MakeSelectTableColumns(q), GetPrimaryKeyColumns());
            if (dr.Read())
            {
                finded = true;
                PopulateSelfFromIDataReader(dr);
            }

            dr.Close();
            return finded;
        }

        /// <summary>
        /// 根据查询条件获取实体模型
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual bool GetModel(Query query)
        {
            bool finded = false;
            IDataReader dr = Database.SelectReader(GetTableName(), MakeSelectTableColumns(query), query, MakeOrderByString(query), 1);
            if (dr.Read())
            {
                finded = true;
                PopulateSelfFromIDataReader(dr);
            }

            dr.Close();
            return finded;
        }


        /// <summary>
        /// 获取集合
        /// </summary>
        /// <returns></returns>
        public virtual List<ViewBase> GetList(Query query)
        {
            List<ViewBase> lst = new List<ViewBase>();
            IDataReader dr = Database.SelectReader(GetTableName(), MakeSelectTableColumns(query), query, MakeOrderByString(query));
            while (dr.Read())
            {
                lst.Add(MakeModelFromIDataReader(dr));
            }
            dr.Close();
            return lst;
        }


        /// <summary>
        /// 根据条件获取前cout条记录
        /// </summary>
        /// <returns></returns>
        public virtual List<ViewBase> GetList(Query query, int count)
        {
            List<ViewBase> lst = new List<ViewBase>();
            IDataReader dr = Database.SelectReader(GetTableName(), MakeSelectTableColumns(query), query, MakeOrderByString(query), count);
            while (dr.Read())
            {
                lst.Add(MakeModelFromIDataReader(dr));
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
        public virtual List<ViewBase> GetList(Query query, ref PageParam page)
        {
            List<ViewBase> lst = new List<ViewBase>();
            IDataParameter total;                                                         //输出参数,总记录数
            IDataParameter count;                                                         //输出参数,总页数

            //排序
            string orderby = MakeOrderByString(query);

            //
            IDataReader dr = Database.SelectReader(GetTableName(), GetIdentifierColumnName(), query, orderby, page, out total, out count);
            while (dr.Read())
            {
                lst.Add(MakeModelFromIDataReader(dr));
            }

            
            dr.Close();

            //输出参数赋值
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
        public virtual double Sum(Query query, string field)
        {
            return Database.SelectSum(GetTableName(), field, query);
        }

        /// <summary>
        /// 获取该实体对象的列的集合
        /// </summary>
        /// <returns></returns>
        public virtual List<TableColumn> GetTableColumns()
        {
            List<TableColumn> lst = new List<TableColumn>();
            //
            Type type = this.GetType();
            System.Reflection.PropertyInfo[] fields = type.GetProperties();
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


                    //DbType
                    string fieldtype = info.PropertyType.ToString();
                    switch (fieldtype)
                    {
                        case "System.String":
                            column.DbType = DbType.AnsiString;
                            break;
                        case "System.Int32":
                            column.DbType = DbType.Int32;
                            break;
                        case "System.Int64":
                            column.DbType = DbType.Int64;
                            break;
                        case "System.Boolean":
                            column.DbType = DbType.Boolean;
                            break;
                        case "System.Decimal":
                            column.DbType = DbType.Decimal;
                            break;
                        case "System.DateTime":
                            column.DbType = DbType.DateTime;
                            column.Value = TypeConvert.ToDateTime(column.Value);
                            break;
                        default:
                            column.DbType = DbType.Binary;
                            break;
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
            System.Reflection.PropertyInfo[] fields = type.GetProperties();
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
                                column.DbType = DbType.Int32;
                                break;
                            case "System.Int64":
                                column.DbType = DbType.Int64;
                                break;
                            case "System.Boolean":
                                column.DbType = DbType.Boolean;
                                break;
                            case "System.Decimal":
                                column.DbType = DbType.Decimal;
                                break;
                            case "System.DateTime":
                                column.DbType = DbType.DateTime;
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
        /// 获取标志列
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
            List<TableColumn> lst = GetTableColumns();
            TableAttribute att = Attribute.GetCustomAttribute(this.GetType(), typeof(TableAttribute)) as TableAttribute;

            //将主键值进行分析“,”号分割
            string[] arr = att.PrimaryKey.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<TableColumn> lst_ret = new List<TableColumn>();

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

            int count = lst.Count;
            for (int i = 0; i < count; i++)
            {
                if (lst[0].IsIdentifier)
                {
                    lst.RemoveAt(i);
                }
                else
                {
                    TableColumn md = lst_primary.Find(s => s.ColumnName == lst[0].ColumnName);
                    if (md != null)
                    {
                        lst.RemoveAt(i);
                    }
                }

            }

            return lst;
        }

        /// <summary>
        /// 构造实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public virtual ViewBase MakeModelFromIDataReader(IDataReader dr)
        {
            Type type = this.GetType();

            //
            ViewBase md = type.Assembly.CreateInstance(type.FullName) as ViewBase;

            if (md != null)
            {

                System.Reflection.PropertyInfo[] fields = type.GetProperties();
                foreach (System.Reflection.PropertyInfo info in fields)
                {
                    TableFieldAttribute att = Attribute.GetCustomAttribute(info, typeof(TableFieldAttribute)) as TableFieldAttribute;
                    if (att != null && att.IsTableField)
                    {
                        if (info.CanWrite)
                        {
                            string field_name = string.IsNullOrEmpty(att.TableFieldName) ? info.Name : att.TableFieldName;  //若没有单独设置数据库的列名，则直接用属性名做为数据库的列名
                            if (dr[field_name] != DBNull.Value)
                            {
                                try
                                {
                                    info.SetValue(md, dr[field_name], null);
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }

            return md;
        }


        /// <summary>
        /// 通过IDataReader构造实体对象本身
        /// </summary>
        /// <param name="dr"></param>
        public virtual void PopulateSelfFromIDataReader(IDataReader dr)
        {
            Type type = this.GetType();


            System.Reflection.PropertyInfo[] fields = type.GetProperties();
            foreach (System.Reflection.PropertyInfo info in fields)
            {
                TableFieldAttribute att = Attribute.GetCustomAttribute(info, typeof(TableFieldAttribute)) as TableFieldAttribute;
                if (att != null && att.IsTableField)
                {
                    if (info.CanWrite)
                    {
                        string field_name = string.IsNullOrEmpty(att.TableFieldName) ? info.Name : att.TableFieldName;  //若没有单独设置数据库的列名，则直接用属性名做为数据库的列名
                        if (dr[field_name] != DBNull.Value)
                        {
                            try
                            {
                                info.SetValue(this, dr[field_name], null);
                            }
                            catch
                            {
                            }
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
            System.Reflection.PropertyInfo[] properties = type.GetProperties();
            if (query.OrderByType != null)
            {
                foreach (QueryOrderField field in query.OrderByType.Fields)
                {
                    //System.Reflection.PropertyInfo info = properties.First(w => w.Name == field.FieldName);

                    //查找第一个满足条件的
                    System.Reflection.PropertyInfo info = null;
                    foreach (System.Reflection.PropertyInfo item in properties)
                    {
                        if (item.Name == field.FieldName)
                        {
                            info = item;
                        }
                    }

                    //
                    if (info != null)
                    {
                        TableFieldAttribute att = Attribute.GetCustomAttribute(info, typeof(TableFieldAttribute)) as TableFieldAttribute;
                        if (att != null && att.IsTableField)
                        {
                            string ColumnName = string.IsNullOrEmpty(att.TableFieldName) ? info.Name : att.TableFieldName;
                            string orderType = field.OrderType == OrderType.ASC ? "asc" : "desc";
                            if (string.IsNullOrEmpty(orderby))
                            {
                                orderby += ColumnName + " " + orderType;
                            }
                            else
                            {
                                orderby += "," + ColumnName + " " + orderType;
                            }
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
            Type type = this.GetType();
            System.Reflection.PropertyInfo info = type.GetProperty(propertyName);
            if (info == null)
            {
                return "";
            }
            else
            {
                TableFieldAttribute att = Attribute.GetCustomAttribute(info, typeof(TableFieldAttribute)) as TableFieldAttribute;
                if (att != null && att.IsTableField)
                {
                    string tmp = string.IsNullOrEmpty(att.TableFieldName) ? info.Name : att.TableFieldName;
                    return tmp;
                }
                else
                {
                    return "";
                }
            }

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
            System.Reflection.PropertyInfo[] properties = type.GetProperties();
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



    }
}
