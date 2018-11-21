using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SW
{
    public class Log4net
    {

        static Log4net()
        {
            string defaultPath = AppDomain.CurrentDomain.BaseDirectory + @"log4net.config";
            string webAppPath = AppDomain.CurrentDomain.BaseDirectory + @"bin\log4net.config";
            if (File.Exists(defaultPath))
            {
                log4net.Config.XmlConfigurator.Configure(new Uri(defaultPath));
            }
            else if (File.Exists(webAppPath))
            {
                log4net.Config.XmlConfigurator.Configure(new Uri(webAppPath));
            }
            else
            {

            }
        }

        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");

        private static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");

        public static void Info(string info)
        {

            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }
        public static void Info(string info, params object[] pars)
        {

            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(string.Format(info, pars));
            }
        }

        public static void Error(string info, Exception se)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, se);
            }
        }
        public static void Error(Exception se, string info)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, se);
            }
        }

        public static void Error(string info)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info);
            }
        }
    }
}
