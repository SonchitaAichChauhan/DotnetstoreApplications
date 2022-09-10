using Dotnetstore.WebAPI.Nuget.Intranet.Dto.Businesses.Stores;

namespace Dotnetstore.UnitOfWorks.Intranet.Interfaces.Businesses;

public interface IStoreWrapper
{
    Task<QuantityAllActiveStoresDto> GetQuantityAllActiveAsync(RequestQuantityAllActiveStoresDto requestQuantityAllActiveStoresDto);
}