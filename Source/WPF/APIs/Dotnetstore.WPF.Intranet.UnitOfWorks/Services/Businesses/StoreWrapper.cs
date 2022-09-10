using Dotnetstore.WebAPI.Nuget.Intranet.Dto.Businesses.Stores;
using Dotnetstore.WPF.Intranet.UnitOfWorks.Interfaces.Businesses;
using Dotnetstore.WPF.Nuget.Core.Abstracts;
using Dotnetstore.WPF.Nuget.Core.Interfaces;
using Dotnetstore.WPF.Nuget.Core.Services;
using System.Net;

namespace Dotnetstore.WPF.Intranet.UnitOfWorks.Services.Businesses;

public sealed class StoreWrapper : Disposable, IStoreWrapper
{
    private string? _httpClientName;
    private IHttpService? _httpService;

    public StoreWrapper(
        IHttpService? httpService,
        string httpClientName)
    {
        _httpService = httpService;
        _httpClientName = httpClientName;
    }

    async Task<HttpResponseWrapper<QuantityAllActiveStoresDto?>> IStoreWrapper.GetQuantityAllActiveAsync(RequestQuantityAllActiveStoresDto requestQuantityAllActiveStoresDto)
    {
        try
        {
            if (_httpService == null ||
                string.IsNullOrWhiteSpace(_httpClientName))
            {
                return new HttpResponseWrapper<QuantityAllActiveStoresDto?>(
                    false,
                    new QuantityAllActiveStoresDto { Quantity = -1 },
                    new HttpResponseMessage(HttpStatusCode.InternalServerError),
                    new Exception("HttpService or HttpClientName is null"));
            }

            return await _httpService.PostAsync<RequestQuantityAllActiveStoresDto, QuantityAllActiveStoresDto>(
                _httpClientName, "api/Store/GetQuantityAllActive", requestQuantityAllActiveStoresDto);
        }
        catch (Exception exception)
        {
            return new HttpResponseWrapper<QuantityAllActiveStoresDto?>(
                false,
                new QuantityAllActiveStoresDto { Quantity = -1 },
                new HttpResponseMessage(HttpStatusCode.InternalServerError),
                exception);
        }
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _httpService = null;
            _httpClientName = null;
        }

        base.DisposeManaged();
    }
}