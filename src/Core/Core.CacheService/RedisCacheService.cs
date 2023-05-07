using System.Text.Json;

namespace Core.CacheService
{
    public class RedisCacheService<T> : IDistributedCacheService<T> where T : class
    {
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);
        private readonly RedisService _redisService;

        private string key
        {
            get
            {
                return typeof(T).Name.ToLower();
            }
        }

        public RedisCacheService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<T?> GetById(int id)
        {
            var cachedObject = await _redisService.GetDb().StringGetAsync($"{key}:{id}");
            if (!cachedObject.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<T>(cachedObject);
            }

            return null;
        }

        public async Task<List<T>> GetAll()
        {
            var keys = _redisService.GetServer().Keys(pattern: $"{key}:*");
            var items = new List<T>();

            foreach (var key in keys)
            {
                var cachedObjectJson = await _redisService.GetDb().StringGetAsync(key);
                var item = JsonSerializer.Deserialize<T>(cachedObjectJson);
                items.Add(item);
            }

            return items;
        }

        public async Task Set(T item)
        {
            var property = typeof(T).GetProperties().Where(k => k.Name.Equals("Id")).FirstOrDefault();
            object value = property.GetValue(item, null);

            var serializedObject = JsonSerializer.Serialize(item);
            await _redisService.GetDb().StringSetAsync($"{key}:{value}", serializedObject, _cacheDuration);
        }


    }
}
