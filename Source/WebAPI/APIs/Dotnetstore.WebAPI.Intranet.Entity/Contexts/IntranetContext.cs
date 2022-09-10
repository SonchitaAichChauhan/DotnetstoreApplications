using Dotnetstore.WebAPI.Intranet.Entity.Business;
using Dotnetstore.WebAPI.Intranet.Entity.Interfaces;
using Dotnetstore.WebAPI.Nuget.Core.Interfaces;
using Dotnetstore.WebAPI.Nuget.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Dotnetstore.WebAPI.Intranet.Entity.Contexts;

public sealed class IntranetContext : DbContext, IBusinessEntityContextService
{
    private readonly IJsonSettingFileReaderService _jsonSettingFileReaderService;
    private readonly ISqlServerService _sqlServerService;

    public IntranetContext()
    {
        _sqlServerService = new SqlServerService();
        _jsonSettingFileReaderService = new JsonSettingFileReaderService(new ConfigurationBuilderSingletonService());
    }

    public IntranetContext(
        DbContextOptions<IntranetContext> options,
        IJsonSettingFileReaderService jsonSettingFileReaderService,
        ISqlServerService sqlServerService) : base(options)
    {
        _jsonSettingFileReaderService = jsonSettingFileReaderService;
        _sqlServerService = sqlServerService;
    }

    public DbSet<BusinessEntity> BusinessEntities => Set<BusinessEntity>();

    public DbSet<Store> Stores => Set<Store>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        try
        {
            var useSqlServer = _jsonSettingFileReaderService.GetBoolean("DataBases:UseSqlServer");

            if (useSqlServer)
            {
                optionsBuilder.UseSqlServer(GetSqlServerConnectionString());
            }
            else
            {
                optionsBuilder.UseSqlite($"Data Source={_jsonSettingFileReaderService.GetString("DataBases:SQLite:FileName")}");
            }
        }
        catch
        {
            optionsBuilder.UseSqlServer("Server=DotnetstoreSQL;Database=DotnetstoreIntranet;Trusted_Connection=false;User Id=sa;Password=Xieb80!2275023;MultipleActiveResultSets=true;");
        }
    }

    private string GetSqlServerConnectionString()
    {
        return _sqlServerService.BuildConnectionString(
            _jsonSettingFileReaderService.GetString("DataBases:SqlServer:DataSource"),
            _jsonSettingFileReaderService.GetString("DataBases:SqlServer:InitialCatalog"),
            _jsonSettingFileReaderService.GetString("DataBases:SqlServer:ApplicationName"),
            _jsonSettingFileReaderService.GetBoolean("DataBases:SqlServer:IntegratedSecurity"),
            _jsonSettingFileReaderService.GetBoolean("DataBases:SqlServer:MultipleActiveResultSets"),
            _jsonSettingFileReaderService.GetString("DataBases:SqlServer:UserID"),
            _jsonSettingFileReaderService.GetString("DataBases:SqlServer:Password"));
    }
}