using Domain.Contracts;

namespace Application.Repositories;

public interface IUnitOfWork<TId> : IDisposable
{
    Task<int> CommitAsync(CancellationToken cancellationToken);
    IWriteRepositoryAsync<T, TId> GetWriteRepositoryFor<T>()
        where T : BaseEntity<TId>;
    IReadRepositoryAsync<T, TId> GetReadRepository<T>()
        where T : BaseEntity<TId>;
}
