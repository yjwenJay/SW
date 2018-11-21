using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW.Cache;

namespace SW
{
    public class CacheHelper
    {
        public static string SetCache(string key)
        {
            var cache = System.Web.HttpRuntime.Cache.Get(key);
            if (cache != null)
            {
                RemoveCache(key);
            }
            string value = DateTime.Now.ToString("yyyyMMddhhHHmmssfff");
            System.Web.HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            return value;
        }

        public static string GetCache(string key)
        {
            var cache = System.Web.HttpRuntime.Cache.Get(key);
            if (cache == null)//如果没有该缓存  
            {
                return "";
            }

            return cache.ToString();
        }

        public static void RemoveCache(string key)
        {
            System.Web.HttpRuntime.Cache.Remove(key);
        }





        private static object cacheLocker = new object();//缓存锁对象
        private static ICache cache = null;//缓存接口

        static CacheHelper()
        {
            Load();
        }

        /// <summary>
        /// 加载缓存
        /// </summary>
        /// <exception cref=""></exception>
        private static void Load()
        {
            try
            {
                cache = new RedisCache();
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message);
            }
        }

        public static ICache GetCache()
        {
            return cache;
        }


        /// <summary>
        /// 缓存过期时间
        /// </summary>
        public static int TimeOut
        {
            get
            {
                return cache.TimeOut;
            }
            set
            {
                lock (cacheLocker)
                {
                    cache.TimeOut = value;
                }
            }
        }


        //////////////////////////////////Redis缓存开始///////////////////////////////////

        /// <summary>
        /// 获得指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>缓存值</returns>
        public static object Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return null;
            return cache.Get(key);
        }

        /// <summary>
        /// 获得指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>缓存值</returns>
        public static T Get<T>(string key)
        {
            return cache.Get<T>(key);
        }

        /// <summary>
        /// 获取字符串内容
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public static string GetStr(string key)
        {
            try
            {
                return Get<string>(key);
            }
            catch (Exception)
            {
                return "";
            }
        }
        

        /// <summary>
        /// 将指定键的对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        public static void Set(string key, object data)
        {
            if (string.IsNullOrWhiteSpace(key) || data == null)
                return;
            try
            {
                cache.Insert(key, data);
            }
            catch (Exception e)
            {
                Log4net.Error("插入缓存异常", e);
            }
            
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        public static void Set<T>(string key, T data)
        {
            if (string.IsNullOrWhiteSpace(key) || data == null)
                return;
            try
            {
                cache.Insert<T>(key, data);
            }
            catch (Exception e)
            {
                Log4net.Error("插入缓存异常", e);
            }
            
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间(分钟)</param>
        public static void Set(string key, object data, int cacheTime)
        {
            if (!string.IsNullOrWhiteSpace(key) && data != null)
            {
                try
                {
                    cache.Insert(key, data, cacheTime);
                }
                catch (Exception e)
                {
                    Log4net.Error("插入缓存异常", e);
                }
                
            }
        }

        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间(分钟)</param>
        public static void Set<T>(string key, T data, int cacheTime)
        {
            if (!string.IsNullOrWhiteSpace(key) && data != null)
            {
                try
                {
                    cache.Insert<T>(key, data, cacheTime);
                }
                catch (Exception e)
                {
                    Log4net.Error("插入缓存异常", e);
                }
                
            }
        }

        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间</param>
        public static void Set(string key, object data, DateTime cacheTime)
        {
            if (!string.IsNullOrWhiteSpace(key) && data != null)
            {
                try
                {
                    cache.Insert(key, data, cacheTime);
                }
                catch (Exception e)
                {
                    Log4net.Error("插入缓存异常", e);
                }

                
            }
        }

        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间</param>
        public static void Set<T>(string key, T data, DateTime cacheTime)
        {
            if (!string.IsNullOrWhiteSpace(key) && data != null)
            {
                try
                {
                    cache.Insert<T>(key, data, cacheTime);
                }
                catch (Exception e)
                {
                    Log4net.Error("插入缓存异常", e);
                }
            }
        }

        /// <summary>
        /// 从缓存中移除指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        public static void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;
            cache.Remove(key);
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        public static bool Exists(string key)
        {
            return cache.Exists(key);
        }



    }
}
