using Dotnetstore.WPF.Intranet.UnitOfWorks.Interfaces.Businesses;
using Dotnetstore.WPF.Intranet.UnitOfWorks.Services.Businesses;
using Dotnetstore.WPF.Nuget.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Dotnetstore.WPF.Intranet.UnitOfWorks.IoC;

public static class ServiceCollectionBootStrap
{
    public static void Build(ref IServiceCollection serviceCollection, string httpDotnetstoreWebAPIClientName, string httpDotnetstoreWebAPIClientBaseAddress)
    {
        Nuget.Core.IoC.ServiceCollectionBootStrap.Build(ref serviceCollection);

        serviceCollection.AddScoped<IStoreWrapper>(q => new StoreWrapper(q.GetService<IHttpService>(), httpDotnetstoreWebAPIClientName));

        serviceCollection.AddHttpClient(httpDotnetstoreWebAPIClientName, httpClient =>
        {
            httpClient.BaseAddress = new Uri(httpDotnetstoreWebAPIClientBaseAddress);
        }).AddPolicyHandler(GetRetryPolicy());
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed)
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            // What to do if any of the above erros occur:
            // Retry 3 times, each time wait 1,2 and 4 seconds before retrying.
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}