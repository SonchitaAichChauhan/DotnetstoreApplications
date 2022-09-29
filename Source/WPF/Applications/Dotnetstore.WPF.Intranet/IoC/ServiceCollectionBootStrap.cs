using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Intranet.Services;
using Dotnetstore.WPF.Intranet.ViewModels.Containers;
using Dotnetstore.WPF.Intranet.Views.Containers;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.WPF.Intranet.IoC;

internal static class ServiceCollectionBootStrap
{
    internal static void Build(ref IServiceCollection serviceCollection)
    {
        Dotnetstore.WPF.Nuget.Core.IoC.ServiceCollectionBootStrap.Build(ref serviceCollection);
        Dotnetstore.WPF.API.Settings.IoC.ServiceCollectionBootStrap.Build(ref serviceCollection);

        RegisterInternalObjects(ref serviceCollection);
    }

    internal static void Build(ref IServiceCollection serviceCollection, string httpDotnetstoreWebAPIClientName, string httpDotnetstoreWebAPIClientBaseAddress)
    {
        Dotnetstore.WPF.Nuget.Core.IoC.ServiceCollectionBootStrap.Build(ref serviceCollection);
        Dotnetstore.WPF.API.Settings.IoC.ServiceCollectionBootStrap.Build(ref serviceCollection);
        //UnitOfWorks.IoC.ServiceCollectionBootStrap.Build(ref serviceCollection, httpDotnetstoreWebAPIClientName, httpDotnetstoreWebAPIClientBaseAddress);

        RegisterInternalObjects(ref serviceCollection);
    }

    private static void RegisterInternalObjects(ref IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IApplicationFileService, ApplicationFileService>();
        serviceCollection.AddSingleton<IApplicationService, ApplicationService>();
        serviceCollection.AddSingleton<IEventService, EventService>();
        serviceCollection.AddSingleton<ISetupService, SetupService>();

        serviceCollection.AddSingleton<IBottomContainerViewModel, BottomContainerViewModel>();
        serviceCollection.AddSingleton<IMainContainerViewModel, MainContainerViewModel>();
        serviceCollection.AddSingleton<ITopContainerViewModel, TopContainerViewModel>();

        serviceCollection.AddSingleton<BottomContainerView>();
        serviceCollection.AddSingleton<MainContainerView>();
        serviceCollection.AddSingleton<TopContainerView>();
    }
}