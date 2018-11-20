using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Member
{
    /// <summary>
    /// 用户权限
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public long PermissionId { get; set; }
        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; set; }
        /// <summary>
        /// 页面名称
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// 操作名称
        /// </summary>
        public string Act { get; set; }
    }
}
