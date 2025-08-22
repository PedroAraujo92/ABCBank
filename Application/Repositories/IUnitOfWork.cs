using Domain.Contracts;

namespace Application.Repositories;

public interface IUnitOfWork<TId> : IDisposable
{
    Task<int> CommitAsync(CancellationToken cancellationToken);
    IWriteRepositoryAsync<T, TId> WriteRepositoryFor<T>()
        where T : BaseEntity<TId>;
    IReadRepositoryAsync<T, TId> ReadRepository<T>()
        where T : BaseEntity<TId>;
}
