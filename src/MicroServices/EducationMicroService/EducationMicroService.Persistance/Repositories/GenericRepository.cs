using EducationMicroService.Application.Contracts;
using EducationMicroService.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EducationMicroService.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseDomainEntity
    {
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(k => k.Id == id && !k.IsDeleted);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().Where(k => !k.IsDeleted).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
