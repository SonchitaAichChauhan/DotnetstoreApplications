using Dotnetstore.WebAPI.Nuget.Intranet.Dto.Businesses.Stores;
using Dotnetstore.WPF.Nuget.Core.Services;

namespace Dotnetstore.WPF.Intranet.UnitOfWorks.Interfaces.Businesses;

public interface IStoreWrapper
{
    Task<HttpResponseWrapper<QuantityAllActiveStoresDto?>> GetQuantityAllActiveAsync(
        RequestQuantityAllActiveStoresDto requestQuantityAllActiveStoresDto);
}