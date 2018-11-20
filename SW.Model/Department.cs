using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Easpnet.Modules;

namespace SW.Model
{
	/// <summary>
 	/// Department
 	/// </summary>
 	[Serializable]
 	[Table(PrimaryKey = "Id", TableName = "Department")]
	public class Department : ModelBase
	{
		public Department()
		{ }

        /// <summary>
        /// 数据操作对象
        /// </summary>
        public static Easpnet.Db Db
        {
            get
            {
                return new Easpnet.Db("Department");
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
		/// Name
        /// </summary>
        [TableField]
        public string Name
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
		/// Info
        /// </summary>
        [TableField]
        public string Info
        {
            get;
            set;
        }
		#endregion Model
	}
}

