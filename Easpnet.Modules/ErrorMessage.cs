using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules
{
    /// <summary>
    /// 错误消息
    /// </summary>
    public class ErrorMessage
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
        /// 跳转到的Url
        /// </summary>
        public string RedirectUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get
            {
                if (_lst_message.Count > 0)
                {
                    string str = "";
                    foreach (string s in _lst_message)
                    {
                        str += TemplateHelper.ReadTemplateContent("error_message/error_message_item.template",
                            new NameObject("{message}", s));
                    }

                    return TemplateHelper.ReadTemplateContent("error_message/error_message.template",
                        new NameObject("{message}", str),
                        new NameObject("{url}", RedirectUrl));
                }
                else
                {
                    return "";
                }
            }
        }

        public void AddMessage(string msg)
        {
            _lst_message.Add(msg);
        }
    }

    /// <summary>
    /// 成功消息
    /// </summary>
    public class SuccessMessage
    {
        private List<string> _lst_message = new List<string>();

        /// <summary>
        /// 跳转到的Url
        /// </summary>
        public string RedirectUrl
        {
            get;
            set;
        }

        
        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get
            {                
                if (_lst_message.Count > 0)
                {
                    string str = "";
                    foreach (string s in _lst_message)
                    {
                        str += TemplateHelper.ReadTemplateContent("success_message/success_message_item.template",
                            new NameObject("{message}", s));
                    }

                    return TemplateHelper.ReadTemplateContent("success_message/success_message.template",
                        new NameObject("{message}", str),
                        new NameObject("{url}", RedirectUrl));
                }
                else
                {
                    return "";
                }
            }
        }

        public void AddMessage(string msg)
        {
            _lst_message.Add(msg);
        }
    }
}
