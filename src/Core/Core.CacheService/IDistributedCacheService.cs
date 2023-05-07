namespace Core.CacheService
{
    public interface IDistributedCacheService<T> where T : class
    {
        Task<T?> GetById(int id);
        Task<List<T>> GetAll();
        Task Set(T group);
    }
}
