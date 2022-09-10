using Dotnetstore.WebAPI.Nuget.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Dotnetstore.WebAPI.Nuget.Core.Services;

public sealed class JsonSettingFileReaderService : IJsonSettingFileReaderService
{
    private readonly IConfiguration? _configuration;

    public JsonSettingFileReaderService(
        IConfigurationBuilderSingletonService configurationBuilderSingletonService)
    {
        _configuration = configurationBuilderSingletonService.ConfigurationRoot;
    }

    bool IJsonSettingFileReaderService.GetBoolean(string key)
    {
        if (string.IsNullOrWhiteSpace(key) ||
            _configuration == null)
        {
            return false;
        }

        var success = bool.TryParse(_configuration.GetSection(key).Value, out var result);
        return success && result;
    }

    int IJsonSettingFileReaderService.GetInt(string key)
    {
        if (string.IsNullOrWhiteSpace(key) ||
            _configuration == null)
        {
            return -1;
        }

        var success = int.TryParse(_configuration.GetSection(key).Value, out var result);
        return success ? result : -1;
    }

    short IJsonSettingFileReaderService.GetInt16(string key)
    {
        if (string.IsNullOrWhiteSpace(key) ||
            _configuration == null)
        {
            return -1;
        }

        var success = short.TryParse(_configuration.GetSection(key).Value, out var result);
        return success ? result : Convert.ToInt16(-1);
    }

    string IJsonSettingFileReaderService.GetString(string key)
    {
        if (string.IsNullOrWhiteSpace(key) ||
            _configuration == null)
        {
            return string.Empty;
        }

        var result = _configuration.GetSection(key).Value;
        return string.IsNullOrWhiteSpace(result) ? string.Empty : result;
    }
}