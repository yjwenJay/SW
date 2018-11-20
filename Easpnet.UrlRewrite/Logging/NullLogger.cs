namespace Easpnet.UrlRewrite.Logging
{
    using System;

    public class NullLogger : IRewriteLogger
    {
        public void Debug(object message)
        {
        }

        public void Error(object message)
        {
        }

        public void Error(object message, Exception exception)
        {
        }

        public void Fatal(object message, Exception exception)
        {
        }

        public void Info(object message)
        {
        }

        public void Warn(object message)
        {
        }
    }
}
