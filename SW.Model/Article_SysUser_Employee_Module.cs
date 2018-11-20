using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Easpnet.Modules;

namespace SW.Model
{
	/// <summary>
 	/// Article_SysUser_Employee_Module
 	/// </summary>
 	[Serializable]
 	[Table(PrimaryKey = "id", TableName = "Article_SysUser_Employee_Module")]
	public class Article_SysUser_Employee_Module : ModelBase
	{
		public Article_SysUser_Employee_Module()
		{ }

        /// <summary>
        /// 数据操作对象
        /// </summary>
        public static Easpnet.Db Db
        {
            get
            {
                return new Easpnet.Db("Article_SysUser_Employee_Module");
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
        public string CreateTimes
        {
            get;
            set;
        }
        /// <summary>
        /// ModifyTime
        /// </summary>
        [TableField]
        public string ModifyTime
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
        /// <summary>
        /// UserId
        /// </summary>
        [TableField]
        public int UserId
        {
            get;
            set;
        }
        /// <summary>
        /// Name
        /// </summary>
        [TableField]
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// ModuleId
        /// </summary>
        [TableField]
        public int ModuleId
        {
            get;
            set;
        }
        /// <summary>
        /// Name
        /// </summary>
        [TableField]
        public string ModuleName
        {
            get;
            set;
        }
        /// <summary>
        /// ModuleLink
        /// </summary>
        [TableField]
        public string ModuleLink
        {
            get;
            set;
        }
        #endregion Model
    }
}

