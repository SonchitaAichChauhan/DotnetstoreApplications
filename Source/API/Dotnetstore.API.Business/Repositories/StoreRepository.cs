using Dotnetstore.API.Business.Contexts;
using Dotnetstore.WebAPI.Intranet.Entity.Business;
using Dotnetstore.WebAPI.Nuget.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dotnetstore.API.Business.Repositories;

public sealed class StoreRepository : GenericRepository<Store, BusinessContext>, IStoreRepository
{
    public StoreRepository(IDbContextFactory<BusinessContext> dataContextFactory) : base(dataContextFactory)
    {
    }

    async Task<List<Store>> IStoreRepository.GetAllActiveAsync(bool isOwnStore)
    {
        return await GetAllActiveAsync(isOwnStore);
    }

    async Task<List<Store>> IStoreRepository.GetAllAsync(bool isOwnStore)
    {
        if (DataContextFactory == null)
        {
            return new List<Store>();
        }

        await using var cx = await DataContextFactory.CreateDbContextAsync();

        return await cx.Stores
            .Where(q => q.IsOwnStore == isOwnStore)
            .OrderBy(q => q.Name)
            .ThenBy(q => q.CorporateID)
            .ThenBy(q => q.Description)
            .AsNoTracking()
            .ToListAsync();
    }

    async Task<int> IStoreRepository.GetQuantityAllActiveAsync(bool isOwnStore)
    {
        var result = await GetAllActiveAsync(isOwnStore);
        return result.Count;
    }

    private async Task<List<Store>> GetAllActiveAsync(bool isOwnStore)
    {
        if (DataContextFactory == null)
        {
            return new List<Store>();
        }

        await using var cx = await DataContextFactory.CreateDbContextAsync();

        return await cx.Stores
            .Where(q => q.IsOwnStore == isOwnStore &&
                        (!q.IsDeleted.HasValue ||
                        (q.IsDeleted.HasValue && !q.IsDeleted.Value)))
            .OrderBy(q => q.Name)
            .ThenBy(q => q.CorporateID)
            .ThenBy(q => q.Description)
            .AsNoTracking()
            .ToListAsync();
    }
}