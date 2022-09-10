using CommunityToolkit.Mvvm.Input;
using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Nuget.Core.Abstracts;

namespace Dotnetstore.WPF.Intranet.ViewModels.Containers;

public class TopContainerViewModel : Disposable, ITopContainerViewModel
{
    private IEventService? _eventService;

    public IRelayCommand? CloseApplicationCommand { get; set; }

    public IRelayCommand? SetWindowRestoreCommand { get; set; }

    public TopContainerViewModel(
        IEventService eventService)
    {
        _eventService = eventService;
        CloseApplicationCommand = new RelayCommand(ExecuteCloseApplication);
        SetWindowRestoreCommand = new RelayCommand(ExecuteSetWindowRestore);
    }

    private void ExecuteSetWindowRestore()
    {
        _eventService?.RunSetWindowRestore();
    }

    private void ExecuteCloseApplication()
    {
        _eventService?.RunCloseApplication();
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _eventService = null;

            CloseApplicationCommand = null;
            SetWindowRestoreCommand = null;
        }

        base.DisposeManaged();
    }
}