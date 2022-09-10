using CommunityToolkit.Mvvm.ComponentModel;
using Dotnetstore.WPF.Nuget.Core.Interfaces;
using Dotnetstore.WPF.Nuget.Core.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnetstore.WPF.Intranet.ViewModels.Containers;

public partial class BottomContainerViewModel : BaseViewModel, IBottomContainerViewModel
{
    private IJsonSettingFileReaderService? _jsonSettingFileReaderService;

    [ObservableProperty]
    private string? _currentTime;

    [ObservableProperty]
    private string? _currentVersion;

    private readonly PeriodicTimer? _periodicTimer = new(TimeSpan.FromSeconds(1));

    private Task? _timerTask;

    private CancellationTokenSource? _cts;

    public void Cancel() => _cts?.Cancel();

    public BottomContainerViewModel(
        IJsonSettingFileReaderService jsonSettingFileReaderService)
    {
        _jsonSettingFileReaderService = jsonSettingFileReaderService;
    }

    private async Task HandleTimerAsync(CancellationToken cancel = default)
    {
        if (_periodicTimer == null)
        {
            return;
        }

        while (await _periodicTimer.WaitForNextTickAsync(cancel))
        {
            CurrentTime = DateTimeOffset.Now.ToString();
        }
    }

    async Task IBottomContainerViewModel.LoadAsync()
    {
        LoadCurrentTimeText();
        LoadCurrentVersionText();
    }

    private void LoadCurrentTimeText()
    {
        _cts = new CancellationTokenSource();
        _timerTask = HandleTimerAsync(_cts.Token);
    }

    private void LoadCurrentVersionText()
    {
        if (_jsonSettingFileReaderService == null)
        {
            return;
        }

        var version = _jsonSettingFileReaderService.GetString("CurrentVersion");
        CurrentVersion = $"Version: {version}";
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _periodicTimer?.Dispose();
            _timerTask?.ConfigureAwait(false);

            CurrentTime = null;
            CurrentVersion = null;
        }

        base.DisposeManaged();
    }
}