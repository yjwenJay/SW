using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace SW.Commons
{
    /// <summary>
    /// RabbitMQ辅助类
    /// </summary>
    public class RabbitMQHelper
    {
        //提交到通道延迟时间
        static string MQURL = System.Configuration.ConfigurationManager.AppSettings["MQURL"];

        /// <summary>
        /// 创建队列
        /// </summary>
        /// <param name="queue"></param>
        public static bool CreateQueue(string QueueName)
        {
            ConnectionFactory cf = new ConnectionFactory();
            cf.Uri = MQURL;
            IConnection con;
            IModel mqModel;

            try
            {
                using (con = cf.CreateConnection())
                {
                    using (mqModel = con.CreateModel())
                    {
                        mqModel.QueueDeclare(QueueName, true, false, false, null);
                        mqModel.QueueBind(QueueName, "amq.direct", QueueName, null);
                        return true;
                    }
                }
            }
            catch (Exception exec)
            {
                Log4net.Error("创建队列异常:" + QueueName, exec);
                return false;
            }
        }

        /// <summary>
        /// 消息入队
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="routingKey"></param>
        /// <param name="message"></param>
        public static void SendMessage(string exchange, string routingKey, string message)
        {
            try
            {
                ConnectionFactory cf = new ConnectionFactory();
                cf.Uri = MQURL;

                using (IConnection conn = cf.CreateConnection())
                {
                    using (IModel ch = conn.CreateModel())
                    {
                        //发布属性定义
                        IBasicProperties properties = ch.CreateBasicProperties();
                        //可持久化
                        properties.DeliveryMode = 2;

                        ch.BasicPublish(exchange,
                                        routingKey,
                                        properties,
                                        Encoding.UTF8.GetBytes(message));
                        //关闭
                        ch.Close();
                    }
                    //关闭连接
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Log4net.Error("发送队列异常:"+ routingKey, ex);
            }
        }


        /// <summary>
        /// 获取一条消息
        /// </summary>
        /// <param name="queue"></param>
        public static string GetMessage(string queue)
        {
            try
            {
                ConnectionFactory cf = new ConnectionFactory();
                cf.Uri = MQURL;

                using (IConnection conn = cf.CreateConnection())
                {
                    using (IModel ch = conn.CreateModel())
                    {
                        BasicGetResult res = ch.BasicGet(queue, false);
                        if (res != null)
                        {
                            string content = System.Text.UTF8Encoding.UTF8.GetString(res.Body);
                            ch.BasicAck(res.DeliveryTag, false);
                            return content;
                        }
                        ch.Close();
                    }
                    //关闭连接
                    conn.Close();
                }
            }
            catch (Exception)
            {
                
            }           

            return "";
        }



        /// <summary>
        /// 发送消息队列
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="message"></param>
        public static void SendMessage(string queue, string message)
        {
            SendMessage("amq.direct", queue, message);
        }


        /// <summary>
        /// 发送消息队列
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="message"></param>
        /// <param name="trycount">尝试多少次</param>
        /// <returns></returns>
        public static bool SendMessage(string queue, string message, int trycount)
        {
            int i = 0;
            while (i <= 3)
            {
                try
                {
                    SendMessage(queue, message);
                    return true;
                }
                catch
                {

                }

                i++;
                System.Threading.Thread.Sleep(2000);
            }

            return false;
        }



        /// <summary>
        /// 发送普通通知
        /// </summary>
        /// <param name="noticeCount">通知次数</param>
        /// <param name="content">通知内容</param>
        public static void SendNormalNotice(int noticeCount, string content)
        {
            SendMessage("mb.port.comsume_v2.healthcheck.normalnotice", noticeCount + content);
        }


        /// <summary>
        /// 发送邮件[走消息队列]
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public static bool SendEmail(string to, string subject, string body)
        {
            try
            {
                List<string> lst = new List<string>();
                lst.Add(to);
                lst.Add(subject);
                lst.Add(body);
                string msg = JsonConvert.SerializeObject(lst);
                SendMessage("mb.port.comsume_v2.email", msg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 发送短信【走消息队列】
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool SendSms(string phone, string msg)
        {
            try
            {
                List<string> lst = new List<string>();
                lst.Add(phone);
                lst.Add(msg);
                string content = JsonConvert.SerializeObject(lst);
                SendMessage("mb.port.comsume_v2.sms", content);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
