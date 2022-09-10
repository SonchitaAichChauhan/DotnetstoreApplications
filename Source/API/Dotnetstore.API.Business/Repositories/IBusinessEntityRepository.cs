using Dotnetstore.WebAPI.Intranet.Entity.Business;
using Dotnetstore.WebAPI.Nuget.Core.Interfaces;

namespace Dotnetstore.API.Business.Repositories;

public interface IBusinessEntityRepository : IGenericRepository<BusinessEntity>
{
    Task<List<BusinessEntity>> GetAllAsync();
}