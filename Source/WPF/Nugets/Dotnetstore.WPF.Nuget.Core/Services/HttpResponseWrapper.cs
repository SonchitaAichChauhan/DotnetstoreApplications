using Dotnetstore.WPF.Nuget.Core.Abstracts;

namespace Dotnetstore.WPF.Nuget.Core.Services;

public sealed class HttpResponseWrapper<T> : Disposable
{
    public bool Success { get; set; }

    public T Response { get; set; }

    public HttpResponseMessage HttpResponseMessage { get; set; }

    public Exception? Exception { get; set; }

    public HttpResponseWrapper(
        bool success, T response, HttpResponseMessage httpResponseMessage, Exception? exception)
    {
        Success = success;
        Response = response;
        HttpResponseMessage = httpResponseMessage;
        Exception = exception;
    }

    public async Task<string> GetBodyAsync()
    {
        return await HttpResponseMessage.Content.ReadAsStringAsync();
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            HttpResponseMessage.Dispose();
        }

        base.DisposeManaged();
    }
}