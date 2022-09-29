using CommunityToolkit.Mvvm.ComponentModel;
using Dotnetstore.WPF.API.Settings.Interfaces;
using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Nuget.Core.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnetstore.WPF.Intranet.ViewModels.Containers;

public sealed partial class BottomContainerViewModel : BaseViewModel, IBottomContainerViewModel
{
    private readonly PeriodicTimer? _periodicTimer = new(TimeSpan.FromSeconds(1));
    private IApplicationFileService? _applicationFileService;
    private IAppSettingService? _appSettingService;
    private CancellationTokenSource? _cts;

    [ObservableProperty]
    private string? _currentTime;

    [ObservableProperty]
    private string? _currentVersion;

    private Task? _timerTask;

    public BottomContainerViewModel(
        IApplicationFileService applicationFileService,
        IAppSettingService appSettingService)
    {
        _applicationFileService = applicationFileService;
        _appSettingService = appSettingService;
    }

    public void Cancel() => _cts?.Cancel();

    void IBottomContainerViewModel.Load()
    {
        LoadCurrentTimeText();
        LoadCurrentVersionText();
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _periodicTimer?.Dispose();
            _timerTask?.ConfigureAwait(false);
            _appSettingService = null;
            _applicationFileService = null;

            CurrentTime = null;
            CurrentVersion = null;
        }

        base.DisposeManaged();
    }

    private async Task HandleTimerAsync(CancellationToken cancel = default)
    {
        if (_periodicTimer is null)
        {
            return;
        }

        while (await _periodicTimer.WaitForNextTickAsync(cancel))
        {
            CurrentTime = DateTimeOffset.Now.ToString();
        }
    }

    private void LoadCurrentTimeText()
    {
        _cts = new CancellationTokenSource();
        _timerTask = HandleTimerAsync(_cts.Token);
    }

    private void LoadCurrentVersionText()
    {
        if (_appSettingService is null ||
            _applicationFileService is null)
        {
            return;
        }

        var appSettingFile = _applicationFileService.AppSettingFile;

        if (string.IsNullOrWhiteSpace(appSettingFile))
        {
            return;
        }

        var appSettings = _appSettingService.Get(appSettingFile);
        var version = appSettings?.CurrentVersion;
        CurrentVersion = $"Version: {version}";
    }
}