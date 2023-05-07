namespace EducationMicroService.Application.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IEducationRepository EducationRepository { get; }

        Task SaveAsync();
    }
}
