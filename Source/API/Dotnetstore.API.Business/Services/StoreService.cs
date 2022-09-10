using Dotnetstore.API.Business.Repositories;
using Dotnetstore.WebAPI.Intranet.Entity.Business;
using Dotnetstore.WebAPI.Nuget.Core.Services;

namespace Dotnetstore.API.Business.Services;

public sealed class StoreService : GenericService<Store>, IStoreService
{
    private IStoreRepository? _repository;

    public StoreService(IStoreRepository repository) : base(repository)
    {
        _repository = repository;
    }

    async Task<List<Store>> IStoreService.GetAllActiveAsync(bool isOwnStore)
    {
        if (_repository == null)
        {
            return new List<Store>();
        }

        return await _repository.GetAllActiveAsync(isOwnStore);
    }

    async Task<List<Store>> IStoreService.GetAllAsync(bool isOwnStore)
    {
        if (_repository == null)
        {
            return new List<Store>();
        }

        return await _repository.GetAllAsync(isOwnStore);
    }

    async Task<int> IStoreService.GetQuantityAllActiveAsync(bool isOwnStore)
    {
        if (_repository == null)
        {
            return 0;
        }

        return await _repository.GetQuantityAllActiveAsync(isOwnStore);
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