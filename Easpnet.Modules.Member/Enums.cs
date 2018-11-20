using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Member
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 普通用户，不具备管理员用户的功能
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 雇员用户，拥有有限的权限，由管理员用户分配
        /// </summary>
        Employee = 1,
        /// <summary>
        /// 管理员用户，拥有所有的权限
        /// </summary>
        SystemAdmin = 10,
        /// <summary>
        /// 网站管理员，具备相关网站配置的功能
        /// </summary>
        WebAdmin = 100
    }


    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    { 
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 已经锁定
        /// </summary>
        Locked = 1
    }
}
