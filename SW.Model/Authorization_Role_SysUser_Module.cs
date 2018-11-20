using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Easpnet.Modules;

namespace SW.Model
{
    /// <summary>
    /// Authorization_Role_SysUser_Module
    /// </summary>
    [Serializable]
 	[Table(PrimaryKey = "id", TableName = "Authorization_Role_SysUser_Module")]
	public class Authorization_Role_SysUser_Module : ModelBase
	{
		public Authorization_Role_SysUser_Module()
		{ }

        /// <summary>
        /// 数据操作对象
        /// </summary>
        public static Easpnet.Db Db
        {
            get
            {
                return new Easpnet.Db("Authorization_Role_SysUser_Module");
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
        /// PsswordHash
        /// </summary>
        [TableField]
        public string PsswordHash
        {
            get;
            set;
        }
        /// <summary>
        /// Phone
        /// </summary>
        [TableField]
        public string Phone
        {
            get;
            set;
        }
        /// <summary>
        /// Email
        /// </summary>
        [TableField]
        public string Email
        {
            get;
            set;
        }
        /// <summary>
        /// Account
        /// </summary>
        [TableField]
        public string Account
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
        public DateTime? ModifyTime
        {
            get;
            set;
        }
        /// <summary>
        /// NickName
        /// </summary>
        [TableField]
        public string NickName
        {
            get;
            set;
        }
        /// <summary>
        /// CompanyId
        /// </summary>
        [TableField]
        public int CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// DepartmentId
        /// </summary>
        [TableField]
        public int DepartmentId
        {
            get;
            set;
        }
        /// <summary>
        /// EmployeeId
        /// </summary>
        [TableField]
        public int EmployeeId
        {
            get;
            set;
        }
        /// <summary>
        /// AuthorID
        /// </summary>
        [TableField]
        public int AuthorID
        {
            get;
            set;
        }
        /// <summary>
        /// Author
        /// </summary>
        [TableField]
        public string Author
        {
            get;
            set;
        }
        /// <summary>
        /// RoleID
        /// </summary>
        [TableField]
        public int RoleID
        {
            get;
            set;
        }
        /// <summary>
        /// RoleName
        /// </summary>
        [TableField]
        public string RoleName
        {
            get;
            set;
        }
        /// <summary>
        /// RoleInfo
        /// </summary>
        [TableField]
        public string RoleInfo
        {
            get;
            set;
        }
        /// <summary>
        /// RoleLevel
        /// </summary>
        [TableField]
        public int RoleLevel
        {
            get;
            set;
        }
        /// <summary>
        /// ModuleName
        /// </summary>
        [TableField]
        public string ModuleName
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
        /// ModuleFather
        /// </summary>
        [TableField]
        public int ModuleFather
        {
            get;
            set;
        }
        /// <summary>
        /// ModuleLevel
        /// </summary>
        [TableField]
        public int ModuleLevel
        {
            get;
            set;
        }
        /// <summary>
        /// ModuleImg
        /// </summary>
        [TableField]
        public string ModuleImg
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

