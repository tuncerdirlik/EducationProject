using EducationGroupMicroService.Domain;

namespace EducationGroupMicroService.Application.Contracts
{
    public interface IEducationGroupRepository : IGenericRepository<EducationGroup>
    {
        Task<IReadOnlyList<EducationGroup>> GetAllActiveAsync();
    }
}
