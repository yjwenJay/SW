using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Easpnet.Modules;

namespace SW.Model
{
	/// <summary>
 	/// Company
 	/// </summary>
 	[Serializable]
 	[Table(PrimaryKey = "Id", TableName = "Company")]
	public class Company : ModelBase
	{
		public Company()
		{ }

        /// <summary>
        /// 数据操作对象
        /// </summary>
        public static Easpnet.Db Db
        {
            get
            {
                return new Easpnet.Db("Company");
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

