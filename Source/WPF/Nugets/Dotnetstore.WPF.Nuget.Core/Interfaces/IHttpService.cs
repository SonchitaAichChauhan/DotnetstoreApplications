using Dotnetstore.WPF.Nuget.Core.Services;

namespace Dotnetstore.WPF.Nuget.Core.Interfaces;

public interface IHttpService
{
    Task<HttpResponseWrapper<T?>> GetAsync<T>(string httpClientName, string url);

    Task<HttpResponseWrapper<object?>> PostAsync<T>(string httpClientName, string url, T data);

    Task<HttpResponseWrapper<TResponse?>> PostAsync<T, TResponse>(string httpClientName, string url, T data);

    Task<HttpResponseWrapper<object?>> PutAsync<T>(string httpClientName, string url, T data);

    Task<HttpResponseWrapper<object?>> DeleteAsync<T>(string httpClientName, string url);
}