using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Intranet.Services;
using Dotnetstore.WPF.Intranet.ViewModels.Containers;
using Dotnetstore.WPF.Intranet.Views.Containers;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.WPF.Intranet.IoC;

internal static class ServiceCollectionBootStrap
{
    internal static void Build(ref IServiceCollection serviceCollection, string httpDotnetstoreWebAPIClientName, string httpDotnetstoreWebAPIClientBaseAddress)
    {
        //UnitOfWorks.IoC.ServiceCollectionBootStrap.Build(ref serviceCollection, httpDotnetstoreWebAPIClientName, httpDotnetstoreWebAPIClientBaseAddress);

        serviceCollection.AddSingleton<IApplicationService, ApplicationService>();
        serviceCollection.AddSingleton<IEventService, EventService>();

        serviceCollection.AddSingleton<IMainContainerViewModel, MainContainerViewModel>();
        serviceCollection.AddSingleton<ITopContainerViewModel, TopContainerViewModel>();

        serviceCollection.AddSingleton<MainContainerView>();
        serviceCollection.AddSingleton<TopContainerView>();
    }
}