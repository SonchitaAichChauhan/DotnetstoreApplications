using Dotnetstore.WPF.API.Settings.Interfaces;
using Dotnetstore.WPF.API.Settings.Models;
using Dotnetstore.WPF.Nuget.Core.Abstracts;
using Dotnetstore.WPF.Nuget.Core.Interfaces;

namespace Dotnetstore.WPF.API.Settings.Services;

public sealed class AppSettingService : Disposable, IAppSettingService
{
    private IJsonService? _jsonService;

    public AppSettingService(
        IJsonService? jsonService)
    {
        _jsonService = jsonService;
    }

    AppSetting? IAppSettingService.Get(string file)
    {
        return _jsonService is null ? new AppSetting() : _jsonService.Get<AppSetting>(file);
    }

    async Task<AppSetting?> IAppSettingService.GetAsync(string file)
    {
        if (_jsonService is null)
        {
            return new AppSetting();
        }

        return await _jsonService.GetAsync<AppSetting>(file);
    }

    async Task IAppSettingService.SaveAsync(string file, AppSetting appSetting)
    {
        if (_jsonService is null)
        {
            return;
        }

        await _jsonService.SaveAsync(file, appSetting);
    }

    void IAppSettingService.Save(string file, AppSetting appSetting)
    {
        _jsonService?.Save(file, appSetting);
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _jsonService = null;
        }

        base.DisposeManaged();
    }
}