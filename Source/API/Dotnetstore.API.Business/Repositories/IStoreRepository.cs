using Dotnetstore.WebAPI.Intranet.Entity.Business;
using Dotnetstore.WebAPI.Nuget.Core.Interfaces;

namespace Dotnetstore.API.Business.Repositories;

public interface IStoreRepository : IGenericRepository<Store>
{
    Task<List<Store>> GetAllActiveAsync(bool isOwnStore);

    Task<List<Store>> GetAllAsync(bool isOwnStore);

    Task<int> GetQuantityAllActiveAsync(bool isOwnStore);
}