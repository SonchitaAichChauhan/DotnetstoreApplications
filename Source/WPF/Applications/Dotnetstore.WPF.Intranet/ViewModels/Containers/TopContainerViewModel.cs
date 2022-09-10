using CommunityToolkit.Mvvm.Input;
using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Nuget.Core.Abstracts;

namespace Dotnetstore.WPF.Intranet.ViewModels.Containers;

public class TopContainerViewModel : Disposable, ITopContainerViewModel
{
    private IEventService? _eventService;

    public IRelayCommand? CloseApplicationCommand { get; set; }

    public TopContainerViewModel(
        IEventService eventService)
    {
        _eventService = eventService;
        CloseApplicationCommand = new RelayCommand(ExecuteCloseApplication);
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
        }

        base.DisposeManaged();
    }
}