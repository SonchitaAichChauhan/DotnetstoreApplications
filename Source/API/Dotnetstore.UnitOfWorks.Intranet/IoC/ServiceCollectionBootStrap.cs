using Dotnetstore.UnitOfWorks.Intranet.Interfaces;
using Dotnetstore.UnitOfWorks.Intranet.Interfaces.Businesses;
using Dotnetstore.UnitOfWorks.Intranet.Services;
using Dotnetstore.UnitOfWorks.Intranet.Services.Businesses;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.UnitOfWorks.Intranet.IoC;

public static class ServiceCollectionBootStrap
{
    public static void Build(ref IServiceCollection serviceCollection, bool useSqlServer, string connectionString)
    {
        API.Business.IoC.ServiceCollectionBootStrap.Build(ref serviceCollection, useSqlServer, connectionString);

        serviceCollection.AddSingleton<IStoreWrapper, StoreWrapper>();

        serviceCollection.AddSingleton<IUnitOfWork, UnitOfWork>();
    }
}