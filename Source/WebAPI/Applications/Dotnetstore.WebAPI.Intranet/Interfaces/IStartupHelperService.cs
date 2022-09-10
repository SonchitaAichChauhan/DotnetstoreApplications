namespace Dotnetstore.WebAPI.Intranet.Interfaces;

public interface IStartupHelperService
{
    (WebApplicationBuilder builder, IServiceCollection serviceCollection) GetServices(string[] args);

    void BuildServices(ref IServiceCollection serviceCollection);

    void SetRuntime(ref WebApplication application);
}