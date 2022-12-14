using Dotnetstore.WebAPI.Nuget.Core.Services;
using Microsoft.Extensions.Configuration;

namespace Dotnetstore.WebAPI.Nuget.Core.Interfaces;

public interface IConfigurationBuilderSingletonService
{
    ConfigurationBuilderSingletonService? Instance { get; }

    IConfigurationRoot? ConfigurationRoot { get; }
}