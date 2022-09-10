namespace Dotnetstore.WebAPI.Intranet.IoC;

public static class ServiceCollectionBootStrap
{
    public static void Build(ref IServiceCollection serviceCollection, bool useSqlServer, string connectionString)
    {
        UnitOfWorks.Intranet.IoC.ServiceCollectionBootStrap.Build(ref serviceCollection, useSqlServer, connectionString);
    }
}