using Dotnetstore.WebAPI.Nuget.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Dotnetstore.WebAPI.Nuget.Core.Services;

public sealed class ConfigurationBuilderSingletonService : IConfigurationBuilderSingletonService
{
    private readonly IConfigurationRoot? _configuration;
    private readonly object _instanceLock = new();
    private ConfigurationBuilderSingletonService? _instance;

    public ConfigurationBuilderSingletonService()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        _configuration = builder.Build();
    }

    IConfigurationRoot? IConfigurationBuilderSingletonService.ConfigurationRoot
    {
        get
        {
            if (_configuration == null) { var x = Instance; }
            return _configuration;
        }
    }

    ConfigurationBuilderSingletonService? IConfigurationBuilderSingletonService.Instance => Instance;

    private ConfigurationBuilderSingletonService? Instance
    {
        get
        {
            lock (_instanceLock)
            {
                return _instance ??= new ConfigurationBuilderSingletonService();
            }
        }
    }
}