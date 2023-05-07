namespace EducationGroupMicroService.Application.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IEducationGroupRepository EducationGroupRepository { get; }
        Task SaveAsync();
    }
}
