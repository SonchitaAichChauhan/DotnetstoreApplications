using Dotnetstore.WPF.API.Settings.Models;

namespace Dotnetstore.WPF.API.Settings.Interfaces;

public interface IAppSettingService
{
    Task SaveAsync(string file, AppSetting appSetting);

    void Save(string file, AppSetting appSetting);

    Task<AppSetting?> GetAsync(string file);

    AppSetting? Get(string file);
}