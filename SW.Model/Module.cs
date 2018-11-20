using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Easpnet.Modules;

namespace SW.Model
{
	/// <summary>
 	/// Module
 	/// </summary>
 	[Serializable]
 	[Table(PrimaryKey = "Id", TableName = "Module")]
	public class Module : ModelBase
	{
		public Module()
		{ }

        /// <summary>
        /// 数据操作对象
        /// </summary>
        public static Easpnet.Db Db
        {
            get
            {
                return new Easpnet.Db("Module");
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
		/// FatherModule
        /// </summary>
        [TableField]
        public int? FatherModule
        {
            get;
            set;
        }
		/// <summary>
		/// Level
        /// </summary>
        [TableField]
        public int? Level
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
		#endregion Model
	}
}

