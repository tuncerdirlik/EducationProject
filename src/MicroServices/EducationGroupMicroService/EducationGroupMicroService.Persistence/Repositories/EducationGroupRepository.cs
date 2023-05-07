using EducationGroupMicroService.Application.Contracts;
using EducationGroupMicroService.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducationGroupMicroService.Persistence.Repositories
{
    public class EducationGroupRepository : GenericRepository<EducationGroup>, IEducationGroupRepository
    {
        private readonly AppDbContext _dbContext;

        public EducationGroupRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<EducationGroup>> GetAllActiveAsync()
        {
            return await _dbContext.EducationGroups.Where(k => k.Status && !k.IsDeleted).ToListAsync();
        }
    }
}
