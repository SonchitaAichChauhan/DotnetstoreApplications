using CommunityToolkit.Mvvm.ComponentModel;
using Dotnetstore.WPF.Nuget.Core.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnetstore.WPF.Intranet.ViewModels.Containers;

public partial class BottomContainerViewModel : BaseViewModel, IBottomContainerViewModel
{
    [ObservableProperty]
    private string? _currentTime;

    private readonly PeriodicTimer? _periodicTimer = new(TimeSpan.FromSeconds(1));

    private Task? _timerTask;

    private CancellationTokenSource? _cts;

    public void Cancel() => _cts?.Cancel();

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
        _cts = new CancellationTokenSource();
        _timerTask = HandleTimerAsync(_cts.Token);
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _periodicTimer?.Dispose();
            _timerTask?.ConfigureAwait(false);
            CurrentTime = null;
        }

        base.DisposeManaged();
    }
}