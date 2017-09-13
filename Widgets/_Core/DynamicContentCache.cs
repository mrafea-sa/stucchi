using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _Core
{
    public class DynamicContentCache
    {
        private const string DYNAMIC_CONTENT_CACHE_KEY = "DYNAMIC_CONTENT_CACHE_KEY";
        private static DynamicContentCache _instance = null;

        public static DynamicContentCache GetInstance()
        {
            if (_instance == null) _instance = new DynamicContentCache();
            if (HttpContext.Current.Cache[DYNAMIC_CONTENT_CACHE_KEY] == null) HttpContext.Current.Cache[DYNAMIC_CONTENT_CACHE_KEY] = new Hashtable();

            return _instance;
        }

        public void Invalidate(string contentType)
        {
            Hashtable cacheItems = HttpContext.Current.Cache[DYNAMIC_CONTENT_CACHE_KEY] as Hashtable;
            if (cacheItems == null) return;

            cacheItems.Remove(contentType);
        }

        public bool ExistsContent(string contentType)
        {
            Hashtable cacheItems = HttpContext.Current.Cache[DYNAMIC_CONTENT_CACHE_KEY] as Hashtable;
            if (cacheItems == null || cacheItems[contentType] == null) return false;

            return true;
        }

        public void AddContent(string contentType, object contentValue)
        {
            Hashtable cacheItems = HttpContext.Current.Cache[DYNAMIC_CONTENT_CACHE_KEY] as Hashtable;
            if (cacheItems == null) return;

            cacheItems.Add(contentType, contentValue);
        }

        public object GetContent(string contentType)
        {
            Hashtable cacheItems = HttpContext.Current.Cache[DYNAMIC_CONTENT_CACHE_KEY] as Hashtable;
            if (cacheItems == null) return null;

            return cacheItems[contentType];
        }
    }
}
