using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace SW.MQ.Services
{
    public abstract class ServiceThead
    {
        protected log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);

        protected string TheadId = "0";

        /// <summary>
        /// 工作主线程
        /// </summary>
        Thread thMain;

        /// <summary>
        /// 是否停止服务
        /// </summary>
        public bool IsStop = false;


        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceThead()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceThead(string tid)
        {
            TheadId = tid;
        }


        /// <summary>
        /// 开始运行
        /// </summary>
        public void Start()
        {
            thMain = new Thread(new ThreadStart(MainWork));
            thMain.Start();
        }

        /// <summary>
        /// 停止运行
        /// </summary>
        public virtual void Stop()
        {
            if (thMain != null && thMain.IsAlive)
            {
                //通知订阅者停止订阅，并且等待线程执行结束
                this.IsStop = true;
                thMain.Join();
            }
        }

        /// <summary>
        /// 定时器维护线程方法
        /// </summary>
        protected abstract void MainWork();



    }
}
