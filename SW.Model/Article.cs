using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Easpnet.Modules;

namespace SW.Model
{
	/// <summary>
 	/// Article
 	/// </summary>
 	[Serializable]
 	[Table(PrimaryKey = "Guid", TableName = "Article")]
	public class Article : ModelBase
	{
		public Article()
		{ }

        /// <summary>
        /// 数据操作对象
        /// </summary>
        public static Easpnet.Db Db
        {
            get
            {
                return new Easpnet.Db("Article");
            }
        }

        #region 扩展方法

        #endregion 扩展方法

        #region Model
        /// <summary>
        /// Guid
        /// </summary>
        [TableField]
        public string Guid
        {
            get;
            set;
        }
        /// <summary>
        /// Id
        /// </summary>
        
        public int Id
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
		/// Owner
        /// </summary>
        [TableField]
        public int Owner
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
		/// ModifyTime
        /// </summary>
        [TableField]
        public DateTime ModifyTime
        {
            get;
            set;
        }
		/// <summary>
		/// Modify
        /// </summary>
        [TableField]
        public int? Modify
        {
            get;
            set;
        }
		/// <summary>
		/// ModifyInfo
        /// </summary>
        [TableField]
        public string ModifyInfo
        {
            get;
            set;
        }
		/// <summary>
		/// Type
        /// </summary>
        [TableField]
        public int Type
        {
            get;
            set;
        }
		/// <summary>
		/// Click
        /// </summary>
        [TableField]
        public int? Click
        {
            get;
            set;
        }
		/// <summary>
		/// Contents
        /// </summary>
        [TableField]
        public string Contents
        {
            get;
            set;
        }
		/// <summary>
		/// Image
        /// </summary>
        [TableField]
        public string Image
        {
            get;
            set;
        }
		/// <summary>
		/// Link
        /// </summary>
        [TableField]
        public string Link
        {
            get;
            set;
        }

        public int Comments
        {
            get;
            set;
        }

        public int ModuleId
        {
            get;
            set;
        }
        #endregion Model
    }
}

