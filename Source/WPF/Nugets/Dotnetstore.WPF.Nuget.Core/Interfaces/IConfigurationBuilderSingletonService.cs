using Dotnetstore.WPF.Nuget.Core.Services;
using Microsoft.Extensions.Configuration;

namespace Dotnetstore.WPF.Nuget.Core.Interfaces;

public interface IConfigurationBuilderSingletonService
{
    ConfigurationBuilderSingletonService? Instance { get; }

    IConfigurationRoot? ConfigurationRoot { get; }
}