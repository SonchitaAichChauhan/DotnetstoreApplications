using Dotnetstore.WebAPI.Intranet.Entity.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.WebAPI.Intranet.Entity.IoC;

public static class ServiceCollectionBootStrap
{
    public static void Build(ref IServiceCollection serviceCollection, bool useSqlServer, string connectionString)
    {
        if (useSqlServer)
            serviceCollection.AddDbContextFactory<IntranetContext>(q => q.UseSqlServer(connectionString));
        else
            serviceCollection.AddDbContextFactory<IntranetContext>(q => q.UseSqlite(connectionString));
    }
}