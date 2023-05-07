using EducationGroupMicroService.Application.Contracts;

namespace EducationGroupMicroService.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IEducationGroupRepository _educationGroupRepository;

        public IEducationGroupRepository EducationGroupRepository => _educationGroupRepository ??= new EducationGroupRepository(_dbContext);

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
