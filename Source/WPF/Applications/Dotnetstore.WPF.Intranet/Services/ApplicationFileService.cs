using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Nuget.Core.Abstracts;
using Dotnetstore.WPF.Nuget.Core.Interfaces;
using System.IO;

namespace Dotnetstore.WPF.Intranet.Services;

public class ApplicationFileService : Disposable, IApplicationFileService
{
    private string? _applicationLocalFolderPath;
    private string? _appSettingFile;
    private string? _databaseFolderPath;
    private string? _loggingFolderPath;
    private string? _personalSettingFile;
    private string? _settingsFolderPath;

#if DEBUG
    private string? _applicationName = "Dotnetstore Intranet DEBUG";
#else
    private string? _applicationName = "Dotnetstore Intranet";
#endif

    public ApplicationFileService(
        IFilePathService filePathService)
    {
        LoadFoldersAndFiles(filePathService);
    }

    string? IApplicationFileService.ApplicationLocalFolderPath => _applicationLocalFolderPath;

    string? IApplicationFileService.AppSettingFile => _appSettingFile;

    string? IApplicationFileService.DatabaseFolderPath => _databaseFolderPath;

    string? IApplicationFileService.LoggingFolderPath => _loggingFolderPath;

    string? IApplicationFileService.PersonalSettingFile => _personalSettingFile;

    string? IApplicationFileService.SettingsFolderPath => _settingsFolderPath;

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _applicationLocalFolderPath = null;
            _databaseFolderPath = null;
            _loggingFolderPath = null;
            _settingsFolderPath = null;
            _applicationName = null;
        }

        base.DisposeManaged();
    }

    private void LoadFoldersAndFiles(IFilePathService filePathService)
    {
        var localApplicationDataFolder = filePathService.LocalApplicationDataFolder;

        if (localApplicationDataFolder != null &&
            !string.IsNullOrWhiteSpace(_applicationName))
        {
            _applicationLocalFolderPath = Path.Combine(localApplicationDataFolder, _applicationName);
            _databaseFolderPath = Path.Combine(_applicationLocalFolderPath, "Database");
            _loggingFolderPath = Path.Combine(_applicationLocalFolderPath, "Log");
            _settingsFolderPath = Path.Combine(_applicationLocalFolderPath, "Settings");
            _appSettingFile = Path.Combine(_settingsFolderPath, "AppSettings.json");
            _personalSettingFile = Path.Combine(_settingsFolderPath, "PersonalSettings.json");
        }
    }
}