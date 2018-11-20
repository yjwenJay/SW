using Easpnet.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Easpnet
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public delegate void SelectCallback<T>(T recd);

    /// <summary>
    /// 连接类型
    /// </summary>
    public enum JoinType
    {
        /// <summary>
        /// 内连接
        /// </summary>
        Inner,
        /// <summary>
        /// 左连接
        /// </summary>
        Left,
        /// <summary>
        /// 右连接
        /// </summary>
        Right
    }
    /// <summary>
    /// 关联
    /// </summary>
    public class Join
    {
        /// <summary>
        /// 连接方式
        /// </summary>
        public JoinType JoinType { get; set; }
        /// <summary>
        /// 连接到的表格
        /// </summary>
        public string Table { get; set; }
        /// <summary>
        /// 表别名
        /// </summary>
        public string TableAs { get; set; }
        /// <summary>
        /// 连接条件
        /// </summary>
        public string On { get; set; }
    }

    /// <summary>
    /// 查询条件
    /// </summary>
    public class Qy
    {
        private Qy()
        {

        }

        /// <summary>
        /// 构造Qy对象，比较符号默认为 = 
        /// </summary>
        /// <param name="field"></param>
        public Qy(string field)
        {
            this.Field = field;
            this.Symbol = "";
        }

        /// <summary>
        /// 构造Qy对象，比较符号默认为 = 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public Qy(string field, object value)
        {
            this.Field = field;
            this.Symbol = "=";
            this.value = value;
        }

        /// <summary>
        /// 构造Qy对象，比较符号默认为 = 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="symbol"></param>
        /// <param name="value"></param>
        public Qy(string field,string symbol, object value)
        {
            this.Field = field;
            this.Symbol = symbol;
            this.value = value;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 比较符号
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object value { get; set; }
    }

    /// <summary>
    /// 查询条件列表
    /// </summary>
    public class QyList : List<Qy>
    {
        /// <summary>
        /// 插入一条查询条件
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public QyList Add(string field, object value)
        {
            this.Add(new Qy(field, value));
            return this;
        }
        /// <summary>
        /// 插入一条查询条件
        /// </summary>
        /// <param name="field"></param>
        /// <param name="symbol"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public QyList Add(string field, string symbol, object value)
        {
            this.Add(new Qy(field, symbol, value));
            return this;
        }


    }


    /// <summary>
    /// 数据库模型
    /// </summary>
    public class Db
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
        /// 表名
        /// </summary>
        internal string m_TableName { get; set; }
        /// <summary>
        /// 表别名
        /// </summary>
        internal string TableAs { get; set; }
        /// <summary>
        /// 查询前N调数据
        /// </summary>
        internal int SelectLimit { get; set; }
        /// <summary>
        /// 分页参数
        /// </summary>
        internal PageParam PageParam { get; set; }

        
        /// <summary>
        /// 查询加锁方式
        /// </summary>
        internal DbLock LockType { get; set; }
        /// <summary>
        /// 表连接
        /// </summary>
        internal List<Join> Joins { get; set; }
        /// <summary>
        /// 查询的字段列表
        /// </summary>
        internal string SelectFields { get; set; }


        private string pri_WhereSql;
        /// <summary>
        /// 查询条件-sql语句
        /// </summary>
        internal string pWhereSql
        {
            get
            {
                if (pri_QyList != null && pri_QyList.Count() > 0)
                {
                    string where = "";
                    object[] par = new object[pri_QyList.Count()];
                    int i = 0;
                    foreach (var item in pri_QyList)
                    {
                        if (!string.IsNullOrEmpty(where))
                        {
                            where += " and ";
                        }

                        //符号不为空才增加占位 ?
                        if (!string.IsNullOrEmpty(item.Symbol))
                        {
                            //若字段名称是正常的单次：^[A-Za-z0-9_]+$  则自动增加 []
                            string wenhao = item.Symbol.ToLower().Contains("in") ? "(?)" : " ? ";
                            if (Regex.Match(item.Field, "^[A-Za-z0-9_]+$").Success)
                            {
                                where += "[" + item.Field + "] " + item.Symbol + wenhao;
                            }
                            else
                            {
                                where += item.Field + " " + item.Symbol + wenhao;
                            }
                        }
                        else
                        {
                            where += item.Field;
                        }
                        

                        par[i] = item.value;
                        i++;
                    }

                    pri_WhereSql = where;
                    pri_WhereParams = par;
                }

                return pri_WhereSql;
            }
            set
            {
                pri_WhereSql = value;
            }
        }

        private object[] pri_WhereParams;

        /// <summary>
        /// 查询条件-参数
        /// </summary>
        internal object[] WhereParams
        {
            get
            {
                return pri_WhereParams;
            }
            set
            {
                pri_WhereParams = value;
            }
        }

        private QyList pri_QyList;
        

        /// <summary>
        /// 排序字段
        /// </summary>
        internal string OrderByField { get; set; }

        /// <summary>
        /// 分组字段
        /// </summary>
        internal string GroupByField { get; set; }


        /// <summary>
        /// 最终执行的原生SQL
        /// </summary>
        public string RawSql { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Db()
        {

        }
        /// <summary>
        /// 分割表名称
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private string[] splitTableName(string table)
        {
            table = table.Trim();
            string[] arr = new Regex(@"(\s+as\s+)|(\s+)").Split(table);     //支持 as 或者 空格
            return arr;
        }



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName"></param>
        public Db(string tableName)
        {
            string[] arr = splitTableName(tableName);

            this.m_TableName = (arr[0]).Trim();
            if (arr.Length > 2 && !string.IsNullOrEmpty((arr[2]).Trim()))
            {
                this.TableAs = (arr[2]).Trim();
            }

            this.Joins = new List<Join>();
            LockType = DbLock.None;
        }



        /// <summary>
        /// 重置各种条件
        /// </summary>
        public void Reset()
        {
            this.pri_QyList = null;
            this.pri_WhereParams = null;
            this.pri_WhereSql = null;
            this.OrderByField = null;
            this.Joins = new List<Join>();
            this.PageParam = null;
            this.SelectFields = null;
            this.SelectLimit = 0;
            LockType = DbLock.None;
        }


        /// <summary>
        /// 新建一个对象
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Db Table(string tableName)
        {
            return new Db(tableName);
        }

        /// <summary>
        /// 根据连接字符串创建对象
        /// </summary>
        /// <param name="connstr">连接字符串</param>
        /// <returns></returns>
        public static Db Connect(string connstr)
        {
            Db db = new Db();
            db.DbConnectionString = connstr;
            db.Joins = new List<Join>();
            db.LockType = DbLock.None;
            return db;
        }

        /// <summary>
        /// 根据连接字符串创建对象
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <returns></returns>
        public static Db Connect(System.Data.Common.DbConnection conn)
        {
            Db db = new Db();
            db.DbConnection = conn;
            db.Joins = new List<Join>();
            db.LockType = DbLock.None;
            return db;
        }


        /// <summary>
        /// 设置表格名称
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public Db TableName(string tableName)
        {
            this.m_TableName = tableName;
            return this;
        }

        /// <summary>
        /// 新增一个表连接
        /// </summary>
        /// <param name="joinType"></param>
        /// <param name="table"></param>
        /// <param name="on"></param>
        /// <returns></returns>
        public Db Join(JoinType joinType, string table, string on)
        {
            string[] arr = splitTableName(table);

            Join j = new Easpnet.Join();
            j.JoinType = joinType;
            j.Table = (arr[0]).Trim();
            if (arr.Length > 2 && !string.IsNullOrEmpty((arr[2]).Trim()))
            {
                j.TableAs = (arr[2]).Trim();
            }
            j.On = on;
            Joins.Add(j);
            return this;
        }


        /// <summary>
        /// 新增一个表连接
        /// </summary>
        /// <param name="joinType"></param>
        /// <param name="table"></param>
        /// <param name="tableAs"></param>
        /// <param name="on"></param>
        /// <returns></returns>
        public Db Join(JoinType joinType, string table,string tableAs, string on)
        {
            string[] arr = splitTableName(table);

            Join j = new Easpnet.Join();
            j.JoinType = joinType;
            j.Table = table.Trim();
            j.TableAs = tableAs.Trim();
            j.On = on;
            Joins.Add(j);
            return this;
        }


        /// <summary>
        /// 新增一个表连接 - Inner Join
        /// </summary>
        /// <param name="table"></param>
        /// <param name="on"></param>
        /// <returns></returns>
        public Db InnerJoin(string table, string on)
        {
            return Join(JoinType.Inner, table, on);
        }

        /// <summary>
        /// 新增一个表连接 - Inner Join
        /// </summary>
        /// <param name="table"></param>
        /// <param name="tableAs"></param>
        /// <param name="on"></param>
        /// <returns></returns>
        public Db InnerJoin(string table, string tableAs, string on)
        {
            return Join(JoinType.Inner, table, tableAs, on);
        }

        /// <summary>
        /// 新增一个表连接 - Left Join
        /// </summary>
        /// <param name="table"></param>
        /// <param name="on"></param>
        /// <returns></returns>
        public Db LeftJoin(string table, string on)
        {
            return Join(JoinType.Left, table, on);
        }

        /// <summary>
        /// 新增一个表连接 - Left Join
        /// </summary>
        /// <param name="table"></param>
        /// <param name="tableAs"></param>
        /// <param name="on"></param>
        /// <returns></returns>
        public Db LeftJoin(string table, string tableAs, string on)
        {
            return Join(JoinType.Left, table, tableAs, on);
        }

        /// <summary>
        /// 新增一个表连接 - Right Join
        /// </summary>
        /// <param name="table"></param>
        /// <param name="on"></param>
        /// <returns></returns>
        public Db RightJoin(string table, string on)
        {
            return Join(JoinType.Right, table, on);
        }

        /// <summary>
        /// 新增一个表连接 - Right Join
        /// </summary>
        /// <param name="table"></param>
        /// <param name="tableAs"></param>
        /// <param name="on"></param>
        /// <returns></returns>
        public Db RightJoin(string table, string tableAs, string on)
        {
            return Join(JoinType.Right, table, tableAs, on);
        }


        /// <summary>
        /// 设置查询Where条件
        /// </summary>
        /// <param name="whereSql">
        /// Sql语句
        /// 用?号作为参数占位
        /// </param>
        /// <param name="par">参数列表</param>
        /// <returns></returns>
        public Db WhereSql(string whereSql, params object[] par)
        {
            pri_QyList = null;  //将简单查询对象清空
            this.pWhereSql = whereSql;
            this.WhereParams = par;
            return this;
        }

        /// <summary>
        /// 构造简单查询条件
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public Db Where(QyList q)
        {
            pri_QyList = q;
            return this;     
        }

        /// <summary>
        /// 新增Where条件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="symbol"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public Db Where(string key, string symbol, object val)
        {
            if (pri_QyList == null)
            {
                pri_QyList = new QyList();
            }
            pri_QyList.Add(key, symbol, val);
            return this;
        }

        /// <summary>
        /// 新增Where条件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public Db Where(string key)
        {
            if (pri_QyList == null)
            {
                pri_QyList = new QyList();
            }
            pri_QyList.Add(new Qy(key));
            return this;
        }

        /// <summary>
        /// 新增Where条件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public Db Where(string key, object val)
        {
            return Where(key, "=", val);
        }

        /// <summary>
        /// 增加In查询
        /// </summary>
        /// <param name="key">字段名称</param>
        /// <param name="values">字段值列表</param>
        /// <returns></returns>
        public Db WhereIn(string key, params object[] values)
        {
            return Where(key, "in", values);
        }

        /// <summary>
        /// 增加Not In查询
        /// </summary>
        /// <param name="key">字段名称</param>
        /// <param name="values">字段值列表</param>
        /// <returns></returns>
        public Db WhereNotIn(string key, params object[] values)
        {
            return Where(key, "not in", values);
        }

        /*
        /// <summary>
        /// 增加Not In查询
        /// </summary>
        /// <param name="key">字段名称</param>
        /// <param name="value1">字段值1</param>
        /// <param name="value2">字段值2</param>
        /// <returns></returns>
        public Db WhereBetween(string key, object value1,object value2)
        {
            object[] values = new object[2];
            values[0] = value1;
            values[1] = value2;
            return Where(key, "between", values);
        }*/

        /// <summary>
        /// 设置排序方式
        /// </summary>
        /// <param name="orderBY"></param>
        /// <returns></returns>
        public Db OrderBy(string orderBY)
        {
            this.OrderByField = orderBY.Trim();
            return this;
        }

        /// <summary>
        /// 设置分组字段
        /// </summary>
        /// <param name="groupBy"></param>
        /// <returns></returns>
        public Db GroupBy(string groupBy)
        {
            this.GroupByField = groupBy.Trim();
            return this;
        }

        /// <summary>
        /// 设置Lock方式
        /// </summary>
        /// <param name="lockType"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public Db Lock(DbLock lockType, Transaction trans = null)
        {
            this.LockType = lockType;
            this.DbTransaction = trans;
            return this;
        }

        /// <summary>
        /// 设置事务
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public Db Transaction(Transaction trans)
        {
            this.DbTransaction = trans;
            return this;
        }

        /// <summary>
        /// 设置查询的条数
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public Db Limit(int limit)
        {
            this.SelectLimit = limit;
            return this;
        }


        /// <summary>
        /// 查找一条记录
        /// </summary>
        /// <param name="fields">查询的列，默认为 * </param>
        /// <param name="callback">查询成功后对记录的回调处理函数</param>
        /// <returns></returns>
        public Record SelectFirst(string fields = "*", SelectCallback<Record> callback = null)
        {
            this.SelectFields = fields;
            this.SelectLimit = 1;
            IDataReader dr = Database.db.Select(this);
            Record rec = null;
            if (dr.Read())
            {
                rec = MakeRecordFromDataReader(dr);
                callback?.Invoke(rec);      //调用委托
            }
            dr.Close();
            return rec;
        }

        /// <summary>
        /// 查询第一条记录的指定字段值
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public string SelectFirstField(string field)
        {
            Record r = SelectFirst(field);
            return r != null ? r.GetStr(field) : "";
        }



        /// <summary>
        /// 查找一条记录
        /// </summary>
        /// <param name="fields">查询的列，默认为 * </param>
        /// <param name="callback">查询成功后对记录的回调处理函数</param>
        /// <returns></returns>
        public T SelectFirst<T>(string fields = "*", SelectCallback<T> callback = null) where T : ModelBase
        {
            this.SelectFields = fields;
            IDataReader dr = Database.db.Select(this);
            T rec = null;
            if (dr.Read())
            {
                rec = MakeRecordFromDataReader<T>(dr);
                callback?.Invoke(rec);      //调用委托              
            }
            dr.Close();
            return rec;
        }


        /// <summary>
        /// 查询结果集合（多条记录）
        /// </summary>
        /// <param name="fields">查询的列，默认为 * </param>
        /// <param name="callback">查询成功后对记录的回调处理</param>
        /// <returns></returns>
        public RecordList<Record> Select(string fields = "*", SelectCallback<Record> callback = null)
        {
            this.SelectFields = fields;
            RecordList<Record> coll = new RecordList<Record>();
            IDataReader dr = Database.db.Select(this);
            while (dr.Read())
            {
                Record rec = MakeRecordFromDataReader(dr);
                callback?.Invoke(rec);      //调用委托
                coll.Add(rec);
            }
            dr.Close();
            return coll;
        }

        /// <summary>
        /// 指定的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fields">要查询的字段</param>
        /// <param name="callback">查询成功后对记录的回调处理</param>
        /// <returns></returns>
        public RecordList<T> Select<T>(string fields = "*", SelectCallback<T> callback = null) where T : ModelBase
        {
            this.SelectFields = fields;
            RecordList<T> coll = new RecordList<T>();
            IDataReader dr = Database.db.Select(this);
            while (dr.Read())
            {
                T rec = MakeRecordFromDataReader<T>(dr);
                callback?.Invoke(rec);      //调用委托
                coll.Add(rec);
            }
            dr.Close();
            coll.PageParam = this.PageParam;
            return coll;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="fields"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public RecordList<T> SelectPage<T>(PageParam page, string fields = "*", SelectCallback<T> callback = null) where T : ModelBase
        {
            this.PageParam = page;
            return Select<T>(fields, callback);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="fields"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public RecordList<Record> SelectPage(PageParam page, string fields = "*", SelectCallback<Record> callback = null)
        {
            this.PageParam = page;
            return Select(fields, callback);
        }

        /// <summary>
        /// 简易分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="fields"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public RecordList<T> SelectPage<T>(int pageIndex, int pageSize, string fields = "*", SelectCallback<T> callback = null) where T : ModelBase
        {
            PageParam page = new PageParam(pageIndex, pageSize);
            page.IsSimplePage = true;
            this.PageParam = page;
            return Select<T>(fields, callback);
        }

        /// <summary>
        /// 简易分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="fields"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public RecordList<Record> SelectPage(int pageIndex, int pageSize, string fields = "*", SelectCallback<Record> callback = null)
        {
            PageParam page = new PageParam(pageIndex, pageSize);
            page.IsSimplePage = true;
            this.PageParam = page;
            return Select(fields, callback);
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="trans">关联的事务</param>
        /// <param name="data">更新的数据</param>
        /// <returns></returns>
        public int Update(Transaction trans, params NameObject[] data)
        {
            if (data == null || data.Length == 0)
            {
                throw new Exception("未指定任何更新的列");
            }
            if (trans != null)
            {
                this.DbTransaction = trans;
            }

            if (string.IsNullOrEmpty(this.pWhereSql))
            {
                throw new Exception("更新操作未指定Where条件：若要更新整个表，可设置更新条件为 1=1");
            }

            return Database.db.Update(this, data);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="trans">关联的事务</param>
        /// <param name="data">更新的数据</param>
        /// <returns></returns>
        public int Update(Transaction trans, List<NameObject> data)
        {
            return Update(trans, data.ToArray());
        }

        /// <summary>
        /// 根据条件更新指定的字段
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Update(params NameObject[] data)
        {
            return Update(null, data);
        }

        /// <summary>
        /// 根据条件更新指定的字段
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Update(List<NameObject> data)
        {
            return Update(null, data.ToArray());
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="data">插入的数据</param>
        /// <returns>返回影响的行数</returns>
        public int Insert(Transaction trans, params NameObject[] data)
        {
            if (string.IsNullOrEmpty(this.m_TableName))
            {
                throw new Exception("未指定表名称");
            }

            if (data == null || data.Length == 0)
            {
                throw new Exception("至少指定一个插入的数据列");
            }
            if (trans != null)
            {
                this.DbTransaction = trans;
            }

            return Database.db.Insert(this, data, false).ToInt32();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="data">插入的数据</param>
        /// <returns>返回影响的行数</returns>
        public int Insert(params NameObject[] data)
        {
            return Insert(null, data);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="data">插入的数据</param>
        /// <returns>返回影响的行数</returns>
        public long InsertGetId(Transaction trans, params NameObject[] data)
        {
            if (string.IsNullOrEmpty(this.m_TableName))
            {
                throw new Exception("未指定表名称");
            }

            if (data == null || data.Length == 0)
            {
                throw new Exception("至少指定一个插入的数据列");
            }
            if (trans != null)
            {
                this.DbTransaction = trans;
            }

            return Database.db.Insert(this, data, true);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="trans">事务</param>
        /// <param name="data">插入的数据</param>
        /// <returns>返回影响的行数</returns>
        public long InsertGetId(params NameObject[] data)
        {
            return InsertGetId(null, data);
        }



        /// <summary>
        /// 执行自定义Sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>返回影响的行数</returns>
        public int Execute(string sql, params object[] parameters)
        {
            return Database.db.Execute(this.DbTransaction, this.DbConnection, this.DbConnectionString, sql, parameters);
        }

        /*
        /// <summary>
        /// 执行自定义Sql语句
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>返回影响的行数</returns>
        public int Execute(Transaction trans, string sql, params object[] parameters)
        {
            return Database.db.Execute(trans,null,null, sql, parameters);
        }
        */

        /// <summary>
        /// 执行自定义Sql语句
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="callback">查询成功后执行的回调-若无则传null即可</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回影响的行数</returns>
        public RecordList<Record> ExecuteSelect(string sql, SelectCallback<Record> callback, params object[] parameters)
        {
            RecordList<Record> coll = new RecordList<Record>();
            IDataReader dr = Database.db.ExecuteSelect(this.DbTransaction, this.DbConnection, this.DbConnectionString, sql, parameters);
            while (dr.Read())
            {
                Record rec = MakeRecordFromDataReader(dr);
                callback?.Invoke(rec);      //调用委托
                coll.Add(rec);
            }
            dr.Close();
            return coll;
        }

        /// <summary>
        /// 执行自定义Sql查询语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public RecordList<Record> ExecuteSelect(string sql, params object[] parameters)
        {
            return ExecuteSelect(sql, null, parameters);
        }


        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 获取树形数组
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private System.Reflection.PropertyInfo[] GetGetProperties(Type type)
        {
            System.Reflection.PropertyInfo[] properties = type.GetProperties();
            return properties;
        }
        /// <summary>
        /// 从DataReader构造Record
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        internal static Record MakeRecordFromDataReader(IDataReader dr)
        {
            Record r = new Record();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                string name = dr.GetName(i);
                object value = dr.GetValue(i);
                r.AddField(name, value);
            }
            return r;
        }

        /// <summary>
        /// 构造实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private T MakeRecordFromDataReader<T>(IDataReader dr) where T : ModelBase
        {
            //创建对象
            Type type = typeof(T);
            T md = type.Assembly.CreateInstance(type.FullName) as T;
            if (md != null)
            {
                //设置附加属性的值
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    string name = dr.GetName(i);
                    object value = dr.GetValue(i);
                    md.AddField(name, value);
                }

                md.MakeSelfFromIDataReader(dr);
            }

            return md;
        }


        

    }
}
