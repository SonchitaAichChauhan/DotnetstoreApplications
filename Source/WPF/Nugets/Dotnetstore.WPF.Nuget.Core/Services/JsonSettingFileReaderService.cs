using Dotnetstore.WPF.Nuget.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Dotnetstore.WPF.Nuget.Core.Services;

public sealed class JsonSettingFileReaderService : IJsonSettingFileReaderService
{
    private readonly IConfiguration? _configuration;

    public JsonSettingFileReaderService(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    bool IJsonSettingFileReaderService.GetBoolean(string key)
    {
        if (string.IsNullOrWhiteSpace(key) ||
            _configuration is null)
        {
            return false;
        }

        var success = bool.TryParse(_configuration.GetSection(key).Value, out var result);
        return success && result;
    }

    int IJsonSettingFileReaderService.GetInt(string key)
    {
        if (string.IsNullOrWhiteSpace(key) ||
            _configuration is null)
        {
            return -1;
        }

        var success = int.TryParse(_configuration.GetSection(key).Value, out var result);
        return success ? result : -1;
    }

    short IJsonSettingFileReaderService.GetInt16(string key)
    {
        if (string.IsNullOrWhiteSpace(key) ||
            _configuration is null)
        {
            return -1;
        }

        var success = short.TryParse(_configuration.GetSection(key).Value, out var result);
        return success ? result : Convert.ToInt16(-1);
    }

    string IJsonSettingFileReaderService.GetString(string key)
    {
        if (string.IsNullOrWhiteSpace(key) ||
            _configuration is null)
        {
            return string.Empty;
        }

        var result = _configuration.GetSection(key).Value;
        return string.IsNullOrWhiteSpace(result) ? string.Empty : result;
    }
}