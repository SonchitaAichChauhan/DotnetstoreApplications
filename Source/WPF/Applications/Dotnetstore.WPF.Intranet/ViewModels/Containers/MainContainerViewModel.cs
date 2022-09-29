using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Nuget.Core.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Dotnetstore.WPF.Intranet.ViewModels.Containers;

public partial class MainContainerViewModel : BaseViewModel, IMainContainerViewModel
{
    private IEventService? _eventService;

    private IBottomContainerViewModel? _bottomContainerViewModel;

    [ObservableProperty]
    private IBottomContainerViewModel? _currentBottomContainerViewModel;

    [ObservableProperty]
    private ITopContainerViewModel? _currentTopContainerViewModel;

    [ObservableProperty]
    private ITopContainerViewModel? _topContainerViewModel;

    [ObservableProperty]
    private WindowState _windowState;

    public MainContainerViewModel(
        IBottomContainerViewModel bottomContainerViewModel,
        IEventService eventService,
        ITopContainerViewModel topContainerViewModel)
    {
        _bottomContainerViewModel = bottomContainerViewModel;
        _eventService = eventService;
        _topContainerViewModel = topContainerViewModel;

        WindowState = WindowState.Maximized;
        CloseApplicationCommand = new RelayCommand(ExecuteCloseApplication);

        _eventService.CloseApplication += EventServiceOnCloseApplication;
        _eventService.SetWindowMinimize += EventServiceOnSetWindowMinimize;
        _eventService.SetWindowRestore += EventServiceOnSetWindowRestore;
    }

    public IRelayCommand? CloseApplicationCommand { get; set; }

    async Task IMainContainerViewModel.LoadAsync()
    {
        LoadITopContainerViewModel();
        LoadIBottomContainerViewModel();
    }

    private void LoadITopContainerViewModel()
    {
        if (_topContainerViewModel is null)
        {
            return;
        }

        _topContainerViewModel.Load();
        CurrentTopContainerViewModel = _topContainerViewModel;
    }

    private void LoadIBottomContainerViewModel()
    {
        if (_bottomContainerViewModel is null)
        {
            return;
        }

        _bottomContainerViewModel.Load();
        CurrentBottomContainerViewModel = _bottomContainerViewModel;
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            if (_eventService != null)
            {
                _eventService.CloseApplication -= EventServiceOnCloseApplication;

                _eventService = null;
            }

            _bottomContainerViewModel = null;

            CurrentBottomContainerViewModel = null;
            CloseApplicationCommand = null;
            TopContainerViewModel = null;
        }

        base.DisposeManaged();
    }

    private static void CloseApplication()
    {
        var result = System.Windows.MessageBox.Show(
            "Are you sure you want to close the application?",
            "Close application",
            MessageBoxButton.OKCancel,
            MessageBoxImage.Warning);

        if (result == MessageBoxResult.OK)
        {
            Application.Current.Shutdown();
        }
    }

    private static void EventServiceOnCloseApplication(object? sender, EventArgs e)
    {
        CloseApplication();
    }

    private void EventServiceOnSetWindowMinimize(object? sender, EventArgs e)
    {
        SetMinimizeWindow();
    }

    private void EventServiceOnSetWindowRestore(object? sender, EventArgs e)
    {
        SetRestoreWindow();
    }

    private void ExecuteCloseApplication()
    {
        _eventService?.RunCloseApplication();
    }

    private void SetMinimizeWindow()
    {
        WindowState = WindowState.Minimized;
    }

    private void SetRestoreWindow()
    {
        WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }
}