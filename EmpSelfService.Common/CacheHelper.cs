using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EmpSelfService.Common
{
    public class CacheHelper
    {
        private static int cachetime = 3600;

        private static readonly object Locked = new object();

        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Insert(string key, object value)
        {
            lock (Locked)
            {
                if (HttpRuntime.Cache.Get(key) != null) HttpRuntime.Cache.Remove(key);

                HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddSeconds(cachetime), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            }
        }

        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        public static void Insert(string key, object value, int cacheTime)
        {
            lock (Locked)
            {
                if (HttpRuntime.Cache.Get(key) != null) HttpRuntime.Cache.Remove(key);
                HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddSeconds(cacheTime), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            }
        }

        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        public static void Insert(string key, object value, DateTime cacheTime)
        {
            lock (Locked)
            {
                if (HttpRuntime.Cache.Get(key) != null) HttpRuntime.Cache.Remove(key);
                HttpRuntime.Cache.Insert(key, value, null, cacheTime, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            }
        }

        /// <summary>
        /// 得到缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            lock (Locked)
            {
                if (HttpRuntime.Cache.Get(key) != null)
                    HttpRuntime.Cache.Remove(key);
            }
        }

        /// <summary>
        /// 清空所有的缓存
        /// </summary>
        public static void RemoveAll()
        {
            IList<string> keyList = GetKeyList();
            foreach (string key in keyList)
            {
                Remove(key);
            }
        }

        /// <summary>
        /// 得到所有的缓存
        /// </summary>
        /// <returns></returns>
        public static IList<string> GetKeyList()
        {
            IList<string> keyList = new List<string>();
            IDictionaryEnumerator cacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                keyList.Add(cacheEnum.Key.ToString());
            }
            return keyList;
        }
    }
}
