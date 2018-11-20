using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules
{
    /// <summary>
    /// 操作结果
    /// </summary>
    public class OperateResult
    {
        private List<string> _lst_message = new List<string>();

        /// <summary>
        /// 是否操作成功
        /// </summary>
        public bool Success
        {
            get { return _lst_message.Count == 0; }
        }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message 
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (_lst_message.Count > 0)
                {
                    sb.Append("<ul class=\"alert-message\">");
                    foreach (string s in _lst_message)
                    {
                        sb.Append("<li>" + s + "</li>");
                    }
                    sb.Append("</ul>");
                }
                return sb.ToString();
            }
        }

        public void AddMessage(string msg)
        {
            _lst_message.Add(msg);
        }
    }
}
