using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Http;
using System.Xml;

namespace SW
{
    public class SmsHelper
    {
        /// <summary>
        /// 发短信
        /// </summary>
        /// <param name="phoneNumber">发送电话号码</param>
        /// <param name="messages">发送信息</param>
        /// <param name="errorMsg">结果信息</param>
        /// <returns>bool</returns>
        public static bool SendSms(String phoneNumber, String messages)
        {
            string postbody = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><msgRuqestData msgType=\"3\" templetNo=\"M0001\"><mobileNo>" + phoneNumber + "</mobileNo><molifang>" + messages + "</molifang></msgRuqestData>";
            string result = HttpClient.Post("http://sms.mobo360.com/app/SendSMS", postbody, Encoding.UTF8);

            try
            {
                //分析响应的字符串内容
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);  //加载XML文档
                doc.XmlResolver = null;

                //获取响应节点
                XmlNode node = doc.SelectSingleNode("msgRespData/respCode");
                if (node.InnerText == "00")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exc)
            {
                return false;
            }
            
        }
    }
}
