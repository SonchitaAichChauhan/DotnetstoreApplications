using Dotnetstore.WPF.Nuget.Core.Interfaces;
using Dotnetstore.WPF.Nuget.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.WPF.Nuget.Core.IoC;

public static class ServiceCollectionBootStrap
{
    public static void Build(ref IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IConfigurationBuilderSingletonService, ConfigurationBuilderSingletonService>();
        serviceCollection.AddScoped<IHttpService, HttpService>();
        serviceCollection.AddScoped<IJsonSettingFileReaderService, JsonSettingFileReaderService>();
    }
}