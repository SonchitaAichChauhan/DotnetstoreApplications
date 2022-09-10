namespace Dotnetstore.WebAPI.Nuget.Core.Interfaces;

public interface IGenericRepository<T> where T : class
{
    ValueTask<(bool success, Exception? exception, T entity)> AddAsync(T entity);

    ValueTask<(bool success, Exception? exception, T entity)> DeleteAsync(T entity);

    ValueTask<(bool success, Exception? exception, T entity)> SaveAsync(T entity);

    Task<T?> FindAsync(Guid id);
}