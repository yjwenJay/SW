
namespace SW
{
    using System;
    using System.Web;
    using System.Web.Caching;

    public class DataCache
    {
        /// <summary>
        /// 移除当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        public static void DelCache(string CacheKey)
        {
            HttpRuntime.Cache.Remove(CacheKey);
        }

        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            return HttpRuntime.Cache[CacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        #region 重载有默认时间方式

        /// <summary>
        /// 清除指定数据缓存
        /// </summary>
        /// <param name="name"></param>
        public static void Remove(string name)
        {
            DelCache(name);
        }
        /// <summary>
        /// 读指定数据缓存
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object Get(string name)
        {
            return GetCache(name);
        }

        /// <summary>
        /// 设置数据缓存,默认保存一天
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void Set(string name, object value)
        {
            Set(name, value, null, DateTime.Now.AddDays(1.0), TimeSpan.Zero);
        }

        /// <summary>
        /// 设置数据缓存,缓存将在指定时间过期
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dateTime"></param>
        public static void Set(string name, object value, DateTime dateTime)
        {
            Set(name, value, null, dateTime, TimeSpan.Zero);
        }

        public static void Set(string name, object value, DateTime dateTime, TimeSpan timeSpan)
        {
            Set(name, value, null, dateTime, timeSpan);
        }


        public static void Set(string name, object value, CacheDependency cacheDependency)
        {
            Set(name, value, cacheDependency, DateTime.Now.AddSeconds(20.0), TimeSpan.Zero);
        }

        public static void Set(string name, object value, CacheDependency cacheDependency, DateTime dt)
        {
            Set(name, value, cacheDependency, dt, TimeSpan.Zero);
        }

        public static void Set(string name, object value, CacheDependency cacheDependency, TimeSpan ts)
        {
            Set(name, value, cacheDependency, System.Web.Caching.Cache.NoAbsoluteExpiration, ts);
        }

        public static void Set(string name, object value, CacheDependency cacheDependency, DateTime dt, TimeSpan ts)
        {
            if (value != null)
                HttpRuntime.Cache.Insert(name, value, cacheDependency, dt, ts);
        }
        #endregion
    }
}

