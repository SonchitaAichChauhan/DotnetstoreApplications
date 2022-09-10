﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Nuget.Core.ViewModels;
using System;
using System.Windows;

namespace Dotnetstore.WPF.Intranet.ViewModels.Containers;

public partial class MainContainerViewModel : BaseViewModel, IMainContainerViewModel
{
    private IEventService? _eventService;

    [ObservableProperty]
    private ITopContainerViewModel _topContainerViewModel;

    public MainContainerViewModel(
        IEventService eventService,
        ITopContainerViewModel topContainerViewModel)
    {
        _eventService = eventService;
        _topContainerViewModel = topContainerViewModel;

        CloseApplicationCommand = new RelayCommand(ExecuteCloseApplication);

        _eventService.CloseApplication += EventServiceOnCloseApplication;
    }

    public IRelayCommand? CloseApplicationCommand { get; set; }

    void IMainContainerViewModel.Load()
    {
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

            CloseApplicationCommand = null;
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

    private void ExecuteCloseApplication()
    {
        _eventService?.RunCloseApplication();
    }
}