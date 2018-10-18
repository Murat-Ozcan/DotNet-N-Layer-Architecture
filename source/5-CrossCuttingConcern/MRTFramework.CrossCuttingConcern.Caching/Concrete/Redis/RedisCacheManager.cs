using System;
using MRTFramework.CrossCuttingConcern.Caching.Abstract;
using StackExchange.Redis.Extensions.Core;

namespace MRTFramework.CrossCuttingConcern.Caching.Concrete.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly ICacheClient _database;

        public RedisCacheManager()
        {
            var redisConfig = new RedisConfigurationManager();
            _database = redisConfig.GetConnection();
        }

        public T Get<T>(string key)
        {
            return _database.Get<T>(key);
        }

        public void Add(string key, object data, int cacheTime)
        {
            if (data == null)
            {
                return;
            }

            _database.Add(key, data, DateTimeOffset.Now.AddMinutes(cacheTime));

        }

        public bool IsAdd(string key)
        {
            return _database.Exists(key);
        }

        public void Remove(string key)
        {
            _database.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var keys = _database.SearchKeys(pattern);
            _database.RemoveAll(keys);
        }

        public void Clear()
        {
            _database.FlushDb();
        }
    }
}
