using Dotnetstore.WPF.API.Settings.Interfaces;
using Dotnetstore.WPF.API.Settings.Models;
using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Nuget.Core.Abstracts;
using Dotnetstore.WPF.Nuget.Core.Interfaces;

namespace Dotnetstore.WPF.Intranet.Services;

public sealed class SetupService : Disposable, ISetupService
{
    private IApplicationFileService? _applicationFileService;
    private IAppSettingService? _appSettingService;
    private IFilePathService? _filePathService;
    private IPersonalSettingService? _personalSettingService;

    public SetupService(
        IApplicationFileService applicationFileService,
        IAppSettingService appSettingService,
        IFilePathService filePathService,
        IPersonalSettingService personalSettingService)
    {
        _applicationFileService = applicationFileService;
        _appSettingService = appSettingService;
        _filePathService = filePathService;
        _personalSettingService = personalSettingService;
    }

    void ISetupService.Run()
    {
        InstallFolders();
        SetupFiles();
        SetAppSettingsDefault();
        SetPersonalSettingsDefault();
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _applicationFileService = null;
            _appSettingService = null;
            _filePathService = null;
            _personalSettingService = null;
        }

        base.DisposeManaged();
    }

    private void InstallFolders()
    {
        if (_applicationFileService is null ||
            _filePathService is null)
        {
            return;
        }

        _filePathService.AddDirectory(_applicationFileService.ApplicationLocalFolderPath);
        _filePathService.AddDirectory(_applicationFileService.SettingsFolderPath);
        _filePathService.AddDirectory(_applicationFileService.DatabaseFolderPath);
        _filePathService.AddDirectory(_applicationFileService.LoggingFolderPath);
    }

    private void SetAppSettingsDefault()
    {
        if (_applicationFileService is null ||
            _appSettingService is null)
        {
            return;
        }

        var file = _applicationFileService?.AppSettingFile;

        if (string.IsNullOrWhiteSpace(file))
        {
            return;
        }

        var result = _appSettingService.Get(file);

        if (result == null)
        {
            var appSetting = new AppSetting
            {
                CurrentVersion = "1.0.0"
            };
            _appSettingService.Save(file, appSetting);
        }
    }

    private void SetPersonalSettingsDefault()
    {
        if (_applicationFileService is null ||
            _personalSettingService is null)
        {
            return;
        }

        var file = _applicationFileService?.PersonalSettingFile;

        if (string.IsNullOrWhiteSpace(file))
        {
            return;
        }

        var result = _personalSettingService.Get(file);

        if (result == null)
        {
            var personalSetting = new PersonalSetting
            {
                Culture = "sv-SE"
            };
            _personalSettingService.Save(file, personalSetting);
        }
    }

    private void SetupFiles()
    {
        if (_filePathService is null ||
            _applicationFileService is null)
        {
            return;
        }

        _filePathService.AddFile(_applicationFileService.AppSettingFile);
        _filePathService.AddFile(_applicationFileService.PersonalSettingFile);
    }
}