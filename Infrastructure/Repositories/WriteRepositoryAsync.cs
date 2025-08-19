using Application.Repositories;
using Domain.Contracts;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class WriteRepositoryAsync<T, Tid> : IWriteRepositoryAsync<T, Tid>
    where T : BaseEntity<Tid>
{
    private readonly ApplicationDbContext _context;

    public WriteRepositoryAsync(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var existingEntity = await _context.Set<T>().FindAsync(entity.Id);
        _context.Entry(existingEntity).CurrentValues.SetValues(entity);

        return entity;
    }
}
