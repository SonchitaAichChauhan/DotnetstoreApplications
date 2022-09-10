using Dotnetstore.API.Business.Contexts;
using Dotnetstore.WebAPI.Intranet.Entity.Business;
using Dotnetstore.WebAPI.Nuget.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dotnetstore.API.Business.Repositories;

public sealed class BusinessEntityRepository : GenericRepository<BusinessEntity, BusinessContext>, IBusinessEntityRepository
{
    public BusinessEntityRepository(IDbContextFactory<BusinessContext> dataContextFactory) : base(dataContextFactory)
    {
    }

    async Task<List<BusinessEntity>> IBusinessEntityRepository.GetAllAsync()
    {
        if (DataContextFactory == null)
        {
            return new List<BusinessEntity>();
        }

        await using var cx = await DataContextFactory.CreateDbContextAsync();

        return await cx.BusinessEntities
            .AsNoTracking()
            .ToListAsync();
    }
}