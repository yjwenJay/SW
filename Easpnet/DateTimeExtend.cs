using System;

namespace Easpnet
{
    public static class DateTimeExtend
    {
        public static string FullString(DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
