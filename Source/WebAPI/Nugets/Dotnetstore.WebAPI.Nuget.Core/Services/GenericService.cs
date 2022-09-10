using Dotnetstore.WebAPI.Nuget.Core.Abstracts;
using Dotnetstore.WebAPI.Nuget.Core.Interfaces;
using Dotnetstore.WebAPI.Nuget.Core.Models;

namespace Dotnetstore.WebAPI.Nuget.Core.Services;

public class GenericService<T> : Disposable, IGenericService<T> where T : BaseModel
{
    private const string RepositoryIsNullErrorMessage = "Repository is null";
    private IGenericRepository<T>? _repository;

    public GenericService(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    async ValueTask<(bool success, Exception? exception, T entity)> IGenericService<T>.AddAsync(T entity, Guid? userID)
    {
        if (_repository == null)
        {
            return (false, new Exception(RepositoryIsNullErrorMessage), entity);
        }

        entity.CreatedByID = userID;
        entity.CreatedTime = DateTimeOffset.Now;

        return await _repository.AddAsync(entity);
    }

    async ValueTask<(bool success, Exception? exception, T entity)> IGenericService<T>.DeleteAsync(T entity, bool softDelete, Guid? userID)
    {
        if (_repository == null)
        {
            return (false, new Exception(RepositoryIsNullErrorMessage), entity);
        }

        if (softDelete)
        {
            entity.DeletedByID = userID;
            entity.DeletedTime = DateTimeOffset.Now;
            entity.IsDeleted = true;

            return await SaveAsync(entity, userID);
        }

        return await _repository.DeleteAsync(entity);
    }

    async Task<T?> IGenericService<T>.FindAsync(Guid id)
    {
        if (_repository == null)
            return default;

        return await _repository.FindAsync(id);
    }

    async ValueTask<(bool success, Exception? exception, T entity)> IGenericService<T>.SaveAsync(T entity, Guid? userID)
    {
        return await SaveAsync(entity, userID);
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _repository = null;
        }

        base.DisposeManaged();
    }

    private async ValueTask<(bool success, Exception? exception, T entity)> SaveAsync(T entity, Guid? userID)
    {
        if (_repository == null)
        {
            return (false, new Exception(RepositoryIsNullErrorMessage), entity);
        }

        entity.LastUpdatedByID = userID;
        entity.LastUpdatedTime = DateTimeOffset.Now;
        entity.IsUpdated = true;

        return await _repository.SaveAsync(entity);
    }
}