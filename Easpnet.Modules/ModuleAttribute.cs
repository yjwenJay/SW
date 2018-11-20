using System;

namespace Easpnet.Modules
{
    public class ModuleAttribute : Attribute
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 模块说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 依赖项
        /// </summary>
        public string Dependence { get; set; }
    }
}
