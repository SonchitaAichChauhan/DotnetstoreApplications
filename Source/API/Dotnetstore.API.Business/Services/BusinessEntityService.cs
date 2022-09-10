using Dotnetstore.API.Business.Repositories;
using Dotnetstore.WebAPI.Intranet.Entity.Business;
using Dotnetstore.WebAPI.Nuget.Core.Services;

namespace Dotnetstore.API.Business.Services;

public sealed class BusinessEntityService : GenericService<BusinessEntity>, IBusinessEntityService
{
    private IBusinessEntityRepository? _repository;

    public BusinessEntityService(IBusinessEntityRepository repository) : base(repository)
    {
        _repository = repository;
    }

    async Task<List<BusinessEntity>> IBusinessEntityService.GetAllAsync()
    {
        if (_repository == null)
        {
            return new List<BusinessEntity>();
        }

        return await _repository.GetAllAsync();
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _repository = null;
        }

        base.DisposeManaged();
    }
}