using CommunityToolkit.Mvvm.Input;
using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Nuget.Core.Abstracts;

namespace Dotnetstore.WPF.Intranet.ViewModels.Containers;

public class TopContainerViewModel : Disposable, ITopContainerViewModel
{
    private IEventService? _eventService;

    public TopContainerViewModel(
        IEventService eventService)
    {
        _eventService = eventService;

        CloseApplicationCommand = new RelayCommand(ExecuteCloseApplication);
        SetWindowMinimizeCommand = new RelayCommand(ExecuteSetWindowMinimize);
        SetWindowRestoreCommand = new RelayCommand(ExecuteSetWindowRestore);
    }

    public IRelayCommand? CloseApplicationCommand { get; set; }

    public IRelayCommand? SetWindowMinimizeCommand { get; set; }

    public IRelayCommand? SetWindowRestoreCommand { get; set; }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _eventService = null;

            CloseApplicationCommand = null;
            SetWindowMinimizeCommand = null;
            SetWindowRestoreCommand = null;
        }

        base.DisposeManaged();
    }

    private void ExecuteCloseApplication()
    {
        _eventService?.RunCloseApplication();
    }

    private void ExecuteSetWindowMinimize()
    {
        _eventService?.RunSetWindowMinimize();
    }

    private void ExecuteSetWindowRestore()
    {
        _eventService?.RunSetWindowRestore();
    }
}