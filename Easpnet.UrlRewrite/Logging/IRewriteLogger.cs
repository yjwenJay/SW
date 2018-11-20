namespace Easpnet.UrlRewrite.Logging
{
    using System;

    public interface IRewriteLogger
    {
        void Debug(object message);
        void Error(object message);
        void Error(object message, Exception exception);
        void Fatal(object message, Exception exception);
        void Info(object message);
        void Warn(object message);
    }
}
