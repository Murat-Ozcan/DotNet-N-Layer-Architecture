﻿using System;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using MRTFramework.CrossCuttingConcern.Caching.Abstract;

namespace MRTFramework.CrossCuttingConcern.Caching.Concrete.Microsoft
{
    public class MicrosoftMemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache => MemoryCache.Default;
        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public void Add(string key, object data, int cacheTime)
        {
            if (data == null)
            {
                return;
            }
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime)
            };

            Cache.Add(new CacheItem(key, data), policy);
        }

        public bool IsAdd(string key)
        {
            return Cache.Contains(key);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = Cache.Where(x => regex.IsMatch(x.Key)).Select(x => x.Key).ToList();
            foreach (var key in keysToRemove)
            {
                Remove(key);
            }
        }

        public void Clear()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }
    }
}
