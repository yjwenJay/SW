using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW
{
    [Serializable]
    public class RowNumParam
    {

        public RowNumParam()
        {

        }

        public RowNumParam(int start, int end)
        {
            Start = start;
            End = end;
        }
        /// <summary>
        /// 开始行号
        /// </summary>
        public int Start;

        /// <summary>
        /// 结束行号
        /// </summary>
        public int End;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total;
    }
}
