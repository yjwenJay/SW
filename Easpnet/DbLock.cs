using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easpnet
{
    /// <summary>
    /// 数据库查询锁类型
    /// </summary>
    public enum DbLock
    {
        /// <summary>
        /// 无锁
        /// </summary>
        None = 0,
        /// <summary>
        /// 更新锁 - 数据读取后，其他线程不能更新
        /// </summary>
        UpdLock = 1,
        /// <summary>
        /// 行锁
        /// </summary>
        RowLock = 2,
        /// <summary>
        /// 不锁
        /// </summary>
        NoLock = 3
    }
}
