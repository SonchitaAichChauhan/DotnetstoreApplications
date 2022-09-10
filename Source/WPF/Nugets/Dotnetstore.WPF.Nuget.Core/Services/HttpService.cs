using Dotnetstore.WPF.Nuget.Core.Abstracts;
using Dotnetstore.WPF.Nuget.Core.Interfaces;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Dotnetstore.WPF.Nuget.Core.Services;

public sealed class HttpService : Disposable, IHttpService
{
    private IHttpClientFactory? _httpClientFactory;

    public HttpService(
        IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private static JsonSerializerOptions JsonSerializerOptions => new()
    {
        PropertyNameCaseInsensitive = true
    };

    async Task<HttpResponseWrapper<object?>> IHttpService.DeleteAsync<T>(string httpClientName, string url)
    {
        if (_httpClientFactory == null)
        {
            return new HttpResponseWrapper<object?>(false, null, new HttpResponseMessage(HttpStatusCode.InternalServerError), new Exception("HttpClientFactory is null"));
        }

        try
        {
            using var httpClient = _httpClientFactory.CreateClient(httpClientName);
            var responseHttp = await httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object?>(responseHttp.IsSuccessStatusCode, null, responseHttp, null);
        }
        catch (Exception exception)
        {
            return new HttpResponseWrapper<object?>(false, null, new HttpResponseMessage(HttpStatusCode.InternalServerError), exception);
        }
    }

    async Task<HttpResponseWrapper<T?>> IHttpService.GetAsync<T>(string httpClientName, string url) where T : default
    {
        if (_httpClientFactory == null)
        {
            return new HttpResponseWrapper<T?>(false, default, new HttpResponseMessage(HttpStatusCode.InternalServerError), new Exception("HttpClientFactory is null"));
        }

        try
        {
            using var httpClient = _httpClientFactory.CreateClient(httpClientName);
            var responseHttp = await httpClient.GetAsync(url);

            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await Deserialize<T>(responseHttp, JsonSerializerOptions);
                return new HttpResponseWrapper<T?>(responseHttp.IsSuccessStatusCode, response, responseHttp, null);
            }

            return new HttpResponseWrapper<T?>(responseHttp.IsSuccessStatusCode, default, new HttpResponseMessage(HttpStatusCode.InternalServerError), null);
        }
        catch (Exception exception)
        {
            return new HttpResponseWrapper<T?>(false, default, new HttpResponseMessage(HttpStatusCode.InternalServerError), exception);
        }
    }

    async Task<HttpResponseWrapper<object?>> IHttpService.PostAsync<T>(string httpClientName, string url, T data)
    {
        if (_httpClientFactory == null)
        {
            return new HttpResponseWrapper<object?>(false, null, new HttpResponseMessage(HttpStatusCode.InternalServerError), new Exception("HttpClientFactory is null"));
        }

        try
        {
            using var httpClient = _httpClientFactory.CreateClient(httpClientName);
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);

            return new HttpResponseWrapper<object?>(response.IsSuccessStatusCode, null, response, null);
        }
        catch (Exception exception)
        {
            return new HttpResponseWrapper<object?>(false, default, new HttpResponseMessage(HttpStatusCode.InternalServerError), exception);
        }
    }

    async Task<HttpResponseWrapper<TResponse?>> IHttpService.PostAsync<T, TResponse>(string httpClientName, string url, T data) where TResponse : default
    {
        if (_httpClientFactory == null)
        {
            return new HttpResponseWrapper<TResponse?>(false, default, new HttpResponseMessage(HttpStatusCode.InternalServerError), new Exception("HttpClientFactory is null"));
        }

        try
        {
            using var httpClient = _httpClientFactory.CreateClient(httpClientName);
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);

            if (response.IsSuccessStatusCode)
            {
                var responseDeserialized = await Deserialize<TResponse>(response, JsonSerializerOptions);
                return new HttpResponseWrapper<TResponse?>(response.IsSuccessStatusCode, responseDeserialized, response, null);
            }

            return new HttpResponseWrapper<TResponse?>(response.IsSuccessStatusCode, default, response, null);
        }
        catch (Exception exception)
        {
            return new HttpResponseWrapper<TResponse?>(false, default, new HttpResponseMessage(HttpStatusCode.InternalServerError), exception);
        }
    }

    async Task<HttpResponseWrapper<object?>> IHttpService.PutAsync<T>(string httpClientName, string url, T data)
    {
        if (_httpClientFactory == null)
        {
            return new HttpResponseWrapper<object?>(false, null, new HttpResponseMessage(HttpStatusCode.InternalServerError), new Exception("HttpClientFactory is null"));
        }

        try
        {
            using var httpClient = _httpClientFactory.CreateClient(httpClientName);
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(url, stringContent);

            return new HttpResponseWrapper<object?>(response.IsSuccessStatusCode, null, response, null);
        }
        catch (Exception exception)
        {
            return new HttpResponseWrapper<object?>(false, null, new HttpResponseMessage(HttpStatusCode.InternalServerError), exception);
        }
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _httpClientFactory = null;
        }

        base.DisposeManaged();
    }

    private static async Task<T?> Deserialize<T>(HttpResponseMessage httpResponseMessage,
                        JsonSerializerOptions jsonSerializerOptions)
    {
        var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseString, jsonSerializerOptions);
    }
}