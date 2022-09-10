using Dotnetstore.API.Business.Services;
using Dotnetstore.UnitOfWorks.Intranet.Interfaces.Businesses;
using Dotnetstore.WebAPI.Nuget.Core.Abstracts;
using Dotnetstore.WebAPI.Nuget.Intranet.Dto.Businesses.Stores;

namespace Dotnetstore.UnitOfWorks.Intranet.Services.Businesses;

public class StoreWrapper : Disposable, IStoreWrapper
{
    private IStoreService? _storeService;

    public StoreWrapper(
        IStoreService storeService)
    {
        _storeService = storeService;
    }

    async Task<QuantityAllActiveStoresDto> IStoreWrapper.GetQuantityAllActiveAsync(RequestQuantityAllActiveStoresDto requestQuantityAllActiveStoresDto)
    {
        if (_storeService == null)
        {
            return new QuantityAllActiveStoresDto { Quantity = 0 };
        }

        var result = await _storeService.GetQuantityAllActiveAsync(requestQuantityAllActiveStoresDto.IsOwnStore);
        return new QuantityAllActiveStoresDto { Quantity = result };
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _storeService = null;
        }

        base.DisposeManaged();
    }
}