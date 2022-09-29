using Dotnetstore.WPF.API.Settings.Models;

namespace Dotnetstore.WPF.API.Settings.Interfaces;

public interface IPersonalSettingService
{
    Task SaveAsync(string file, PersonalSetting personalSetting);

    void Save(string file, PersonalSetting personalSetting);

    Task<PersonalSetting?> GetAsync(string file);

    PersonalSetting? Get(string file);
}