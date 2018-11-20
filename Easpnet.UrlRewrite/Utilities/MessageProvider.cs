namespace Easpnet.UrlRewrite.Utilities
{
    using System;
    using System.Collections;
    using System.Resources;
    using System.Reflection;

    internal sealed class MessageProvider
    {
        private static Hashtable _messageCache = new Hashtable();
        private static ResourceManager _resources = new ResourceManager("Easpnet.UrlRewrite.Properties.Resources", Assembly.GetExecutingAssembly());

        private MessageProvider()
        {
        }

        public static string FormatString(Message message, params object[] args)
        {
            string str;
            lock (_messageCache.SyncRoot)
            {
                if (_messageCache.ContainsKey(message))
                {
                    str = (string) _messageCache[message];
                }
                else
                {
                    str = _resources.GetString(message.ToString());
                    _messageCache.Add(message, str);
                }
            }
            return string.Format(str, args);
        }
    }
}
