using Dotnetstore.WebAPI.Nuget.Core.Interfaces;
using Dotnetstore.WebAPI.Nuget.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.WebAPI.Nuget.Core.IoC;

public static class ServiceCollectionBootStrap
{
    public static void Build(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IConfigurationBuilderSingletonService, ConfigurationBuilderSingletonService>();
        serviceCollection.AddSingleton<IJsonSettingFileReaderService, JsonSettingFileReaderService>();
        serviceCollection.AddSingleton<ISqlServerService, SqlServerService>();
    }
}