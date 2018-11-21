using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using System.Threading;


namespace SW.MQ.Services
{
    public class SubscribMqServiceThead : ServiceThead
    {
        public event SubscribMqEventHandler OnSubscribMq;

        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName = System.Configuration.ConfigurationManager.AppSettings["MQ_QUEURE_NAME"];

        /// <summary>
        /// 是否定义延时队列
        /// </summary>
        public bool IsDelayQueue = false;

        /// <summary>
        /// 延时队列名称
        /// </summary>
        public string DelayQueueName = "";

        /// <summary>
        /// 延时时间-秒
        /// </summary>
        public int DelayQueueTimeSecond = 0;


        /// <summary>
        /// 消息队列连接
        /// </summary>
        public string MqUrl = System.Configuration.ConfigurationManager.AppSettings["MQURL"];

        /// <summary>
        /// 连接心跳时间
        /// </summary>
        public ushort RequestedHeartbeat = 60;
        
        /// <summary>
        /// 队列是否不用通知
        /// </summary>
        public AckMethod AckMethod = AckMethod.AckWhenFinish;

        protected override void MainWork()
        {
            ConnectionFactory cf = new ConnectionFactory();
            cf.Uri = MqUrl;
            cf.RequestedHeartbeat = RequestedHeartbeat;     //连接心跳时间
            IConnection con;
            IModel mqModel;
            QueueingBasicConsumer consumer;

            //Thread.Sleep(10000);

            //LogHelper.LogPushServer("服务启动成功，等待消息队列！");

            try
            {
                using (con = cf.CreateConnection())
                {
                    using (mqModel = con.CreateModel())
                    {
                        //申明延时队列
                        if (IsDelayQueue && !string.IsNullOrEmpty(DelayQueueName))
                        {
                            System.Collections.Generic.Dictionary<String, object> args = new Dictionary<string, object>();
                            args.Add("x-message-ttl", DelayQueueTimeSecond * 1000);
                            args.Add("x-dead-letter-exchange", "amq.direct");
                            args.Add("x-dead-letter-routing-key", QueueName);
                            mqModel.QueueDeclare(DelayQueueName, true, false, false, args);
                            mqModel.QueueBind(DelayQueueName, "amq.direct", DelayQueueName, null);
                        }

                        //申明队列
                        mqModel.QueueDeclare(QueueName, true, false, false, null);
                        mqModel.QueueBind(QueueName, "amq.direct", QueueName, null);
                        consumer = new QueueingBasicConsumer(mqModel);
                        mqModel.BasicConsume(QueueName, this.AckMethod == AckMethod.NoAck, consumer);
                        while (!IsStop)
                        {
                            try
                            {
                                //超时时间5秒，若5秒仍然没有消息，则进行下一次循环
                                object outres;
                                if (!consumer.Queue.Dequeue(5000, out outres))
                                {
                                    continue;
                                }

                                //获取内容并立即应答
                                RabbitMQ.Client.Events.BasicDeliverEventArgs e = outres as RabbitMQ.Client.Events.BasicDeliverEventArgs;
                                string content = Encoding.UTF8.GetString(e.Body);

                                //如果接收到后马上通知
                                if (this.AckMethod == AckMethod.AckWhenRecive)
                                {
                                    mqModel.BasicAck(e.DeliveryTag, false);
                                }
                                
                                //执行回调函数
                                if (OnSubscribMq != null)
                                {
                                    bool succ = OnSubscribMq(null, content);

                                    //
                                    if (this.AckMethod == AckMethod.AckWhenSuccess)
                                    {
                                        if (succ)
                                        {
                                            mqModel.BasicAck(e.DeliveryTag, false);
                                        }
                                        else
                                        {
                                            //返回队列
                                            mqModel.BasicNack(e.DeliveryTag, false, true);
                                        }
                                    }
                                    else if (this.AckMethod == AckMethod.AckWhenFinish)
                                    {
                                        mqModel.BasicAck(e.DeliveryTag, false);
                                    }
                                }
                                else
                                {
                                    throw new Exception("没有指定接收到队列数据后的回调函数");
                                }
                            }
                            catch (Exception ex)
                            {
                                //Log4net.Error(ex, "服务异常！消息队列URL：" + MqUrl);
                                //跳出循环保证可以重新调用连接队列方法
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception exec)
            {
                //日志
                //Log4net.Error(exec, "服务异常！五秒后将重新尝试，消息队列URL：" + MqUrl);
            }

            //若未收到停止指令，才重新尝试连接队列
            if (!IsStop)
            {
                //隔5秒后重新尝试连接队列
                Thread.Sleep(5000);
                MainWork();
            }
        }


    }



    public delegate bool SubscribMqEventHandler(object sender, string e);

    /// <summary>
    /// 通知方式
    /// </summary>
    public enum AckMethod
    { 
        /// <summary>
        /// 不用通知
        /// </summary>
        NoAck = 1,
        /// <summary>
        /// 接收到后马上通知
        /// </summary>
        AckWhenRecive = 2,
        /// <summary>
        /// 当回调执行完成后
        /// </summary>
        AckWhenFinish = 3,
        /// <summary>
        /// 当回调返回成功后通知
        /// </summary>
        AckWhenSuccess = 4,
        /// <summary>
        /// 自定义通知
        /// </summary>
        CustomeAck = 5
    }
}

