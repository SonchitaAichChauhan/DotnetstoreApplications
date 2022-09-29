using Dotnetstore.WPF.API.Settings.Interfaces;
using Dotnetstore.WPF.API.Settings.Models;
using Dotnetstore.WPF.Nuget.Core.Abstracts;
using Dotnetstore.WPF.Nuget.Core.Interfaces;

namespace Dotnetstore.WPF.API.Settings.Services;

public sealed class PersonalSettingService : Disposable, IPersonalSettingService
{
    private IJsonService? _jsonService;

    public PersonalSettingService(
        IJsonService? jsonService)
    {
        _jsonService = jsonService;
    }

    async Task<PersonalSetting?> IPersonalSettingService.GetAsync(string file)
    {
        if (_jsonService is null)
        {
            return new PersonalSetting();
        }

        return await _jsonService.GetAsync<PersonalSetting>(file);
    }

    PersonalSetting? IPersonalSettingService.Get(string file)
    {
        return _jsonService is null ? new PersonalSetting() : _jsonService.Get<PersonalSetting>(file);
    }

    async Task IPersonalSettingService.SaveAsync(string file, PersonalSetting personalSetting)
    {
        if (_jsonService is null)
        {
            return;
        }

        await _jsonService.SaveAsync(file, personalSetting);
    }

    void IPersonalSettingService.Save(string file, PersonalSetting personalSetting)
    {
        _jsonService?.Save(file, personalSetting);
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