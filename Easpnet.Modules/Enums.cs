using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easpnet.Modules
{
    public enum PageLayout
    {
        /// <summary>
        /// 一栏
        /// </summary>
        MiddleOnly = 1,
        /// <summary>
        /// 左栏和中栏两栏
        /// </summary>
        LeftMiddle = 2,
        /// <summary>
        /// 左中右三栏
        /// </summary>
        LeftMiddleRight = 3,
        /// <summary>
        /// 中栏和右栏两栏
        /// </summary>
        MiddleRight = 4
    }
}
