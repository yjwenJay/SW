using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easpnet.Modules
{
    /// <summary>
    /// 执行结束后要执行的操作类型枚举
    /// </summary>
    [Serializable]
    public enum AjaxJsonAction
    {
        /// <summary>
        /// 无操作
        /// </summary>
        none = 0,
        /// <summary>
        /// 跳转
        /// </summary>
        redirect = 1,
        /// <summary>
        /// 重新加载页面
        /// </summary>
        reload = 2
    }

    /// <summary>
    /// 通用的Ajax请求Json返回数据
    /// </summary>
    [Serializable]
    public class AjaxJsonData
    {
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 返回的文本消息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 执行结束后要执行的操作
        /// </summary>
        public AjaxJsonAction action { get; set; }
        /// <summary>
        /// 跳转到的页面Url
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 附加数据
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 转化为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);            
        }

    }
}
