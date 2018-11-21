using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
 
namespace SW.MQ.Services
{
    public class SwServiceBase : ServiceBase
    {
        //线程列表
        public List<ServiceThead> TheadList = new List<ServiceThead>();

        public int ThreadCount = 1;

        public SwServiceBase()
        {
            //获取要启动的线程的数量，默认为1个线程
            int ThreadCount = System.Configuration.ConfigurationManager.AppSettings["ThreadCount"].ToInt32();
            if (ThreadCount == 0)
            {
                ThreadCount = 1;
            }
        }

        /// <summary>
        /// 测试
        /// </summary>
        public void StartTest()
        {
            OnStart(null);
        }

        /*
        protected override void OnStart(string[] args)
        {
            

            //启动线程
            for (int i = 0; i < ThreadCount; i++)
            {
                ServiceThead th = new ServiceThead(i.ToString());
                thList.Add(th);
                th.Start();
            }

        }*/

        protected override void OnStop()
        {
            foreach (ServiceThead th in TheadList)
            {
                th.Stop();
            }
        }


    }
}
