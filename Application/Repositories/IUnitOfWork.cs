using Domain.Contracts;

namespace Application.Repositories;

internal interface IUnitOfWork<TId> : IDisposable
    where TId : notnull
{
    Task<int> CommitAsync();
    Task<int> CommitAsync(CancellationToken cancellationToken);
    IWriteRepositoryAsync<T, TId> GetWriteRepositoryFor<T>()
        where T : BaseEntity<TId>;
    IReadRepositoryAsync<T, TId> GetReadRepository<T>()
        where T : BaseEntity<TId>;
}
