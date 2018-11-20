using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Easpnet.Modules;

namespace SW.Model
{
	/// <summary>
 	/// Message
 	/// </summary>
 	[Serializable]
 	[Table(PrimaryKey = "Id", TableName = "Message")]
	public class Message : ModelBase
	{
		public Message()
		{ }

        /// <summary>
        /// 数据操作对象
        /// </summary>
        public static Easpnet.Db Db
        {
            get
            {
                return new Easpnet.Db("Message");
            }
        }
        
		#region 扩展方法
		
        #endregion 扩展方法
        
		#region Model
      	/// <summary>
		/// Id
        /// </summary>
        [TableField]
        public int Id
        {
            get;
            set;
        }
		/// <summary>
		/// Owner
        /// </summary>
        [TableField]
        public int? Owner
        {
            get;
            set;
        }
		/// <summary>
		/// Title
        /// </summary>
        [TableField]
        public string Title
        {
            get;
            set;
        }
		/// <summary>
		/// Info
        /// </summary>
        [TableField]
        public string Info
        {
            get;
            set;
        }
		/// <summary>
		/// CreateTime
        /// </summary>
        [TableField]
        public DateTime CreateTime
        {
            get;
            set;
        }
		/// <summary>
		/// Type
        /// </summary>
        [TableField]
        public string Type
        {
            get;
            set;
        }
		/// <summary>
		/// IP
        /// </summary>
        [TableField]
        public string IP
        {
            get;
            set;
        }
		/// <summary>
		/// ArticleId
        /// </summary>
        [TableField]
        public int? ArticleId
        {
            get;
            set;
        }
		#endregion Model
	}
}

