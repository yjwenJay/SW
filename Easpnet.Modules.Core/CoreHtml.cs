using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using Easpnet.Modules.Models;

namespace Easpnet.Modules.Core
{
    /// <summary>
    /// 相关Html构造方法
    /// </summary>
    public class CoreHtml
    {
        /// <summary>
        /// 获取模块的选择框
        /// </summary>
        /// <returns></returns>
        public static string ModuleSelect(string id, string name, string value, params string[] options)
        {
            List<Module> lst = new Module().GetAllModules();
            NameValueCollection nvc = new NameValueCollection();
            foreach (Module md in lst)
            {
                nvc.Add(md.ModuleName, md.ModuleName);
            }

            return Html.Select(id, name, value, nvc, options);
        }
    }
}
