using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Easpnet.Modules;

namespace SW.Model
{
	/// <summary>
 	/// SysUser_Employee_Deparment_Company
 	/// </summary>
 	[Serializable]
 	[Table(PrimaryKey = "id", TableName = "SysUser_Employee_Deparment_Company")]
	public class SysUser_Employee_Deparment_Company : ModelBase
	{
		public SysUser_Employee_Deparment_Company()
		{ }

        /// <summary>
        /// 数据操作对象
        /// </summary>
        public static Easpnet.Db Db
        {
            get
            {
                return new Easpnet.Db("SysUser_Employee_Deparment_Company");
            }
        }
        
		#region 扩展方法
		
        #endregion 扩展方法
        
		#region Model
      	#endregion Model
	}
}

