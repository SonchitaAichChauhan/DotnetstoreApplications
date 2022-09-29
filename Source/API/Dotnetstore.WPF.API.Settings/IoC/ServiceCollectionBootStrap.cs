using Dotnetstore.WPF.API.Settings.Interfaces;
using Dotnetstore.WPF.API.Settings.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.WPF.API.Settings.IoC;

public static class ServiceCollectionBootStrap
{
    public static void Build(ref IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IAppSettingService, AppSettingService>();
        serviceCollection.AddSingleton<IPersonalSettingService, PersonalSettingService>();
    }
}