using Domain.Contracts;

namespace Application.Repositories;

public interface IWriteRepositoryAsync<T, in TId> : IReadRepositoryAsync<T, TId>
    where T : class, IEntity<TId>
    where TId : notnull
{
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}