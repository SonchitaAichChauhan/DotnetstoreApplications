using Dotnetstore.WebAPI.Nuget.Core.Models;

namespace Dotnetstore.WebAPI.Nuget.Core.Interfaces;

public interface IGenericService<T> where T : BaseModel
{
    ValueTask<(bool success, Exception? exception, T entity)> AddAsync(T entity, Guid? userID);

    ValueTask<(bool success, Exception? exception, T entity)> DeleteAsync(T entity, bool softDelete, Guid? userID);

    ValueTask<(bool success, Exception? exception, T entity)> SaveAsync(T entity, Guid? userID);

    Task<T?> FindAsync(Guid id);
}