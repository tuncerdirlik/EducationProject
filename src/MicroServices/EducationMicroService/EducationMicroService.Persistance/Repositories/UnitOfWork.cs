using EducationMicroService.Application.Contracts;

namespace EducationMicroService.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IEducationRepository _educationRepository;

        public IEducationRepository EducationRepository => _educationRepository ??= new EducationRepository(_dbContext);

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
