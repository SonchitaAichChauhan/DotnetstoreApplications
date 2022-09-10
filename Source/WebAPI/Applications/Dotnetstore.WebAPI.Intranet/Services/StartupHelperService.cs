using Dotnetstore.WebAPI.Intranet.Interfaces;
using Dotnetstore.WebAPI.Nuget.Core.Interfaces;
using Dotnetstore.WebAPI.Nuget.Core.Services;

namespace Dotnetstore.WebAPI.Intranet.Services;

public class StartupHelperService : IStartupHelperService
{
    void IStartupHelperService.BuildServices(ref IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllers();
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();

        var (useSqlServer, connectionString) = BuildConnectionString();
        IoC.ServiceCollectionBootStrap.Build(ref serviceCollection, useSqlServer, connectionString);
    }

    (WebApplicationBuilder builder, IServiceCollection serviceCollection) IStartupHelperService.GetServices(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        return (builder, services);
    }

    private static (bool useSqlServer, string connectionString) BuildConnectionString()
    {
        var (configuration, sqlServerService, jsonSettingFileReaderService) = GetConfiguration();

        if (configuration == null ||
            sqlServerService == null ||
            jsonSettingFileReaderService == null)
            return (true, string.Empty);

        var useSqlServer = jsonSettingFileReaderService.GetBoolean("DataBases:UseSqlServer");

        if (useSqlServer)
        {
            return (true, sqlServerService.BuildConnectionString(
                jsonSettingFileReaderService.GetString("DataBases:SqlServer:DataSource"),
                jsonSettingFileReaderService.GetString("DataBases:SqlServer:InitialCatalog"),
                jsonSettingFileReaderService.GetString("DataBases:SqlServer:ApplicationName"),
                jsonSettingFileReaderService.GetBoolean("DataBases:IntegratedSecurity"),
                jsonSettingFileReaderService.GetBoolean("DataBases:MultipleActiveResultSets"),
                jsonSettingFileReaderService.GetString("DataBases:SqlServer:UserID"),
                jsonSettingFileReaderService.GetString("DataBases:SqlServer:Password")));
        }

        return (false, $"Data Source = {jsonSettingFileReaderService.GetString("DataBases:SQLite:FileName")}");
    }

    private static (IConfiguration? configuration, ISqlServerService? sqlServerService, IJsonSettingFileReaderService? jsonSettingFileReaderService) GetConfiguration()
    {
        IConfigurationBuilderSingletonService configurationBuilderSingletonService =
            new ConfigurationBuilderSingletonService();
        ISqlServerService sqlServerService = new SqlServerService();
        IJsonSettingFileReaderService jsonSettingFileReaderService =
            new JsonSettingFileReaderService(configurationBuilderSingletonService);
        return (configurationBuilderSingletonService.ConfigurationRoot, sqlServerService, jsonSettingFileReaderService);
    }

    void IStartupHelperService.SetRuntime(ref WebApplication application)
    {
        if (application.Environment.IsDevelopment())
        {
            application.UseSwagger();
            application.UseSwaggerUI();
        }

        application.UseHttpsRedirection();
        application.UseAuthorization();
        application.MapControllers();
        application.Run();
    }
}