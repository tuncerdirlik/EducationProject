using EducationMicroService.Application.Contracts;
using EducationMicroService.Domain;

namespace EducationMicroService.Persistence.Repositories
{
    public class EducationRepository : GenericRepository<Education>, IEducationRepository
    {
        private readonly AppDbContext _dbContext;

        public EducationRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
