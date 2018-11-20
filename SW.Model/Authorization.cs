using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Easpnet.Modules;

namespace SW.Model
{
	/// <summary>
 	/// Authorization
 	/// </summary>
 	[Serializable]
 	[Table(PrimaryKey = "Id", TableName = "Authorization")]
	public class Authorization : ModelBase
	{
		public Authorization()
		{ }

        /// <summary>
        /// 数据操作对象
        /// </summary>
        public static Easpnet.Db Db
        {
            get
            {
                return new Easpnet.Db("Authorization");
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
		/// Author
        /// </summary>
        [TableField]
        public string Author
        {
            get;
            set;
        }
		/// <summary>
		/// RoleId
        /// </summary>
        [TableField]
        public int RoleId
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
		#endregion Model
	}
}

