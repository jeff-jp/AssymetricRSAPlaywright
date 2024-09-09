using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace AssymetricRSACriptPlaywrigth.Utils
{
    public static class CachingTest
    {
        private static MemoryCache _cacheObj;
        private static MemoryCache cacheObj
        {
            get
            {
                if(_cacheObj == null)
                {
                    _cacheObj = new MemoryCache("CacheProj");
                }
                return _cacheObj;
            }
        }

        private static CacheItemPolicy _itemPolicy;
        private static CacheItemPolicy itemPolicy
        {
            get
            {
                if(_itemPolicy == null)
                {
                    _itemPolicy = new CacheItemPolicy();
                    _itemPolicy.Priority = CacheItemPriority.Default;
                }

                return _itemPolicy;
            }
        }

        public static void AddCache(string nameKey, object value)
        {
            //MemoryCache cacheObj = new MemoryCache("TestJJP");

            var _objItemCache = cacheObj.Get(nameKey);

            if(_objItemCache != null)
                cacheObj.Remove(nameKey);

            cacheObj.Add(nameKey, value, itemPolicy);
        }
        
        public static T GetCache<T>(string nameKey)
        {
            //MemoryCache cacheObj = new MemoryCache("TestJJP");

            var _objItemCache = cacheObj.Get(nameKey);

            //if (_objItemCache == null)
            //    return "";
            
            dynamic result = null;

            if (typeof(T).FullName == "System.String")
                result = _objItemCache.ToString();
            else if (typeof(T).FullName == "System.Int")
                result = Convert.ToInt32(_objItemCache);

            return result;
        }

        public static void RemoveCache(string nameKey)
        {
            //MemoryCache cacheObj = new MemoryCache("TestJJP");

            var _objItemCache = cacheObj.Get(nameKey);

            if(_objItemCache != null)
                cacheObj.Remove(nameKey);
        }

    }
}