//using EducationGroupMicroService.Application.Contracts;
//using EducationGroupMicroService.Domain;
//using System.Text.Json;

//namespace EducationGroupMicroService.Persistence.Repositories
//{
//    public class RedisCacheService : IDistributedCacheService
//    {
//        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);
//        private readonly RedisService _redisService;

//        public RedisCacheService(RedisService redisService)
//        {
//            _redisService = redisService;
//        }

//        public async Task<EducationGroup?> GetById(int id)
//        {
//            var cachedEducationProgram = await _redisService.GetDb().StringGetAsync($"educationPrograms:{id}");
//            if (!cachedEducationProgram.IsNullOrEmpty)
//            {
//                return JsonSerializer.Deserialize<EducationGroup>(cachedEducationProgram);
//            }

//            return null;    
//        }

//        public async Task<List<EducationGroup>> GetAll()
//        {
//            var keys = _redisService.GetServer().Keys(pattern: "educationPrograms:*");
//            var educationPrograms = new List<EducationGroup>();

//            foreach (var key in keys)
//            {
//                var educationProgramJson = await _redisService.GetDb().StringGetAsync(key);
//                var educationProgram = JsonSerializer.Deserialize<EducationGroup>(educationProgramJson);
//                educationPrograms.Add(educationProgram);
//            }

//            return educationPrograms;
//        }

//        public async Task Set(EducationGroup educationGroup)
//        {
//            var serializedEducationProgram = JsonSerializer.Serialize(educationGroup);
//            await _redisService.GetDb().StringSetAsync($"educationPrograms:{educationGroup.Id}", serializedEducationProgram, _cacheDuration);
//        }

        
//    }
//}
