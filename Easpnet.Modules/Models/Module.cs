using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Easpnet.Modules.Models
{
    /// <summary>
    /// 模块实体
    /// </summary>
    [Table(PrimaryKey = "ModuleId", TableName = "CoreModule", EnableCacheDependency = true)]
    public class Module : ModelBase
    {
        public static readonly Module Instance = new Module();

        /// <summary>
        /// 模块id
        /// </summary>
        [TableField(IsTableField = true, IsIdentifier = true)]
        public long ModuleId { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [TableField(50)]
        public string ModuleName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [TableField(255)]
        public string Description { get; set; }
        /// <summary>
        /// 程序集名称
        /// </summary>
        [TableField(255)]
        public string AssemblyName { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [TableField(25)]
        public string Version { get; set; }
        /// <summary>
        /// 依赖项
        /// </summary>
        [TableField(2000)]
        public string Dependence { get; set; }
        /// <summary>
        /// 模块包含的页面数量
        /// </summary>
        [TableField]
        public int PageCount { get; set; }
        /// <summary>
        /// 模块包含的区块数量
        /// </summary>
        [TableField]
        public int BlockCount { get; set; }
        /// <summary>
        /// Url重写配置文件路径
        /// </summary>
        [TableField(255)]
        public string UrlRewriteFile { get; set; }
        /// <summary>
        /// 是否是核心模块，核心模块不能进行卸载
        /// </summary>
        [TableField]
        public bool IsCore { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        [TableField]
        public string NameSpace { get; set; }

        /// <summary>
        /// 获取所有安装的模块
        /// </summary>
        /// <returns></returns>
        public List<Module> GetAllModules()
        {
            Query q = Query.NewQuery();
            q.OrderBy("ModuleName", OrderType.ASC);
            return GetList<Module>(q);
        }


        /// <summary>
        /// 根据
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <returns></returns>
        public static Module GetModuleByName(string moduleName)
        {
            string cachname = "GetModuleByName" + Instance.GetType().AssemblyQualifiedName + "moduleName_" + moduleName;
            Module module = HttpRuntime.Cache.Get(cachname) as Module;
            if (module == null)
            {
                Query q = Query.NewQuery();
                q.Where("ModuleName", Symbol.EqualTo, Ralation.End, moduleName);
                module = new Module();
                if (module.GetModel(q))
                {
                    HttpRuntime.Cache.Add(cachname, module,
                        Instance.Dependency, DateTime.Now.AddHours(1.0), TimeSpan.Zero, CacheItemPriority.Normal, null);
                }
            }

            return module;                    
        }


    }
}
