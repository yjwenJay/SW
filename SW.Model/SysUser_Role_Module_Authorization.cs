using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Easpnet.Modules;

namespace SW.Model
{
	/// <summary>
 	/// SysUser_Role_Module_Authorization
 	/// </summary>
 	[Serializable]
 	[Table(PrimaryKey = "id", TableName = "SysUser_Role_Module_Authorization")]
	public class SysUser_Role_Module_Authorization : ModelBase
	{
		public SysUser_Role_Module_Authorization()
		{ }

        /// <summary>
        /// 数据操作对象
        /// </summary>
        public static Easpnet.Db Db
        {
            get
            {
                return new Easpnet.Db("SysUser_Role_Module_Authorization");
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
        /// RoleId
        /// </summary>
        [TableField]
        public int? RoleId
        {
            get;
            set;
        }
        /// <summary>
		/// Name
        /// </summary>
        [TableField]
        public string RoleName
        {
            get;
            set;
        }
        /// <summary>
        /// Info
        /// </summary>
        [TableField]
        public string RoleInfo
        {
            get;
            set;
        }
        /// <summary>
        /// Level
        /// </summary>
        [TableField]
        public int RoleLevel
        {
            get;
            set;
        }
        /// <summary>
        /// Id
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
        /// FatherModule
        /// </summary>
        [TableField]
        public int? ModuleFather
        {
            get;
            set;
        }
        /// <summary>
        /// Level
        /// </summary>
        [TableField]
        public int? ModuleLevel
        {
            get;
            set;
        }
        /// <summary>
        /// Image
        /// </summary>
        [TableField]
        public string ModuleImg
        {
            get;
            set;
        }
        /// <summary>
        /// Link
        /// </summary>
        [TableField]
        public string ModuleLink
        {
            get;
            set;
        }
        /// <summary>
        /// Link
        /// </summary>
        [TableField]
        public string AuthorId
        {
            get;
            set;
        }
        /// <summary>
        /// Link
        /// </summary>
        [TableField]
        public string Author
        {
            get;
            set;
        }
        #endregion Model
    }
}

