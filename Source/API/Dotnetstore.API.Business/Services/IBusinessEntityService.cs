using Dotnetstore.WebAPI.Intranet.Entity.Business;
using Dotnetstore.WebAPI.Nuget.Core.Interfaces;

namespace Dotnetstore.API.Business.Services;

public interface IBusinessEntityService : IGenericService<BusinessEntity>
{
    Task<List<BusinessEntity>> GetAllAsync();
}