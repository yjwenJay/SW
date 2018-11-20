using System.Data.Common;

namespace Easpnet
{
    /// <summary>
    /// Easpnet封装的事务类
    /// </summary>
    public class Transaction
    {
        private DbTransaction myTransaction;
        private DbConnection myConnection;

        /// <summary>
        /// 获取数据库事务
        /// </summary>
        public DbTransaction DbTransaction
        {
            get { return myTransaction; }
        }

        /// <summary>
        /// 获取数据库链接
        /// </summary>
        public DbConnection Connection
        {
            get
            {
                return myConnection;
            }
            set
            {
                myConnection = value;
            }
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public Transaction()
        {
            myConnection = Database.db.Connection;
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
            myTransaction = myConnection.BeginTransaction();
        }

        /// <summary>
        /// 在指定的连接中开启事务
        /// </summary>
        /// <param name="conn"></param>
        public Transaction(DbConnection conn)
        {
            myConnection = conn;
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
            myTransaction = myConnection.BeginTransaction();
        }

        /// <summary>
        /// 用指定的连接字符串开启事务
        /// </summary>
        /// <param name="connstr">连接字符串</param>
        public Transaction(string connstr)
        {
            myConnection = Database.db.MakeConnection(connstr);
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
            myTransaction = myConnection.BeginTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            myTransaction.Commit();
            if (myConnection.State == System.Data.ConnectionState.Open)
            {
                myConnection.Close();
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            myTransaction.Rollback();
            if (myConnection.State == System.Data.ConnectionState.Open)
            {
                myConnection.Close();
            }
        }
        


    }
}
