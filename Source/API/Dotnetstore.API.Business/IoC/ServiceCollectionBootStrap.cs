using Dotnetstore.API.Business.Contexts;
using Dotnetstore.API.Business.Repositories;
using Dotnetstore.API.Business.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.API.Business.IoC;

public static class ServiceCollectionBootStrap
{
    public static void Build(ref IServiceCollection serviceCollection, bool useSqlServer, string connectionString)
    {
        serviceCollection.AddSingleton<IBusinessEntityRepository, BusinessEntityRepository>();
        serviceCollection.AddSingleton<IBusinessEntityService, BusinessEntityService>();

        serviceCollection.AddSingleton<IStoreRepository, StoreRepository>();
        serviceCollection.AddSingleton<IStoreService, StoreService>();

        serviceCollection.AddDbContextFactory<BusinessContext>(q =>
        {
            if (useSqlServer)
                q.UseSqlServer(connectionString);
            else
                q.UseSqlite(connectionString);
        });
    }
}