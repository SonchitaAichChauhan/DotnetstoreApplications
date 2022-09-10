namespace Dotnetstore.WPF.Nuget.Core.Interfaces;

public interface IJsonSettingFileReaderService
{
    bool GetBoolean(string key);

    int GetInt(string key);

    short GetInt16(string key);

    string GetString(string key);
}