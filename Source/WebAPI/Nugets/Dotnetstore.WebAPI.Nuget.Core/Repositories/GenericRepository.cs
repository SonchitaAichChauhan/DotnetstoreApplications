using Dotnetstore.WebAPI.Nuget.Core.Abstracts;
using Dotnetstore.WebAPI.Nuget.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dotnetstore.WebAPI.Nuget.Core.Repositories;

public class GenericRepository<T, TContext> : Disposable, IGenericRepository<T> where T : class where TContext : DbContext
{
    public GenericRepository(IDbContextFactory<TContext> dataContextFactory)
    {
        DataContextFactory = dataContextFactory;
    }

    public IDbContextFactory<TContext>? DataContextFactory { get; set; }

    async ValueTask<(bool success, Exception? exception, T entity)> IGenericRepository<T>.AddAsync(T entity)
    {
        try
        {
            if (DataContextFactory == null)
                return (false, new Exception("DataContextFactory is null"), entity);

            await using var cx = await DataContextFactory.CreateDbContextAsync();
            cx.Entry(entity).State = EntityState.Added;
            await cx.SaveChangesAsync();
            return (true, null, entity);
        }
        catch (Exception exception)
        {
            return (false, exception, entity);
        }
    }

    async ValueTask<(bool success, Exception? exception, T entity)> IGenericRepository<T>.DeleteAsync(T entity)
    {
        try
        {
            if (DataContextFactory == null)
                return (false, new Exception("DataContextFactory is null"), entity);

            await using var cx = await DataContextFactory.CreateDbContextAsync();
            cx.Entry(entity).State = EntityState.Deleted;
            await cx.SaveChangesAsync();
            return (true, null, entity);
        }
        catch (Exception exception)
        {
            return (false, exception, entity);
        }
    }

    async Task<T?> IGenericRepository<T>.FindAsync(Guid id)
    {
        if (DataContextFactory == null)
            return default;

        await using var cx = await DataContextFactory.CreateDbContextAsync();
        return await cx.Set<T>().FindAsync(id);
    }

    async ValueTask<(bool success, Exception? exception, T entity)> IGenericRepository<T>.SaveAsync(T entity)
    {
        try
        {
            if (DataContextFactory == null)
                return (false, new Exception("DataContextFactory is null"), entity);

            await using var cx = await DataContextFactory.CreateDbContextAsync();
            cx.Entry(entity).State = EntityState.Modified;
            await cx.SaveChangesAsync();
            return (true, null, entity);
        }
        catch (Exception exception)
        {
            return (false, exception, entity);
        }
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            DataContextFactory = null;
        }

        base.DisposeManaged();
    }
}