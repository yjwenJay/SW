
using System;
using System.Web;
namespace SW.Commons.Http
{

    public class SessionState
    {
        public static object Get(string name)
        {
            string str = ConfigHelper.GetValue("AppPrefix");
            string sessionName = ((str == null) ? string.Empty : str) + name;
            if (HttpContext.Current!=null && HttpContext.Current.Session!=null && HttpContext.Current.Session[sessionName] != null)
                return HttpContext.Current.Session[sessionName];
            else
                return null;
        }

        public static object GetAndRemove(string name)
        {
            string str = ConfigHelper.GetValue("AppPrefix");
            string sessionName = ((str == null) ? string.Empty : str) + name;
            object obj2 = HttpContext.Current.Session[sessionName];
            if (HttpContext.Current.Session[sessionName] != null)
            {
                HttpContext.Current.Session.Remove(sessionName);
            }
            return obj2;
        }

        public static void Remove(string name)
        {
            string str = ConfigHelper.GetValue("AppPrefix");
            string sessionName = ((str == null) ? string.Empty : str) + name;
            if (HttpContext.Current.Session[sessionName] != null)
            {
                HttpContext.Current.Session.Remove(sessionName);
            }
        }

        public static void RemoveAll()
        {
            HttpContext.Current.Session.RemoveAll();
        }

        public static void Set(string name, object value)
        {
            string str = ConfigHelper.GetValue("AppPrefix");
            string sessionName = ((str == null) ? string.Empty : str) + name;
            HttpContext.Current.Session.Add(sessionName, value);
        }
    }
}

