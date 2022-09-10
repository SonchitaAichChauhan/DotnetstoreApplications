using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dotnetstore.WPF.Nuget.Core.ViewModels;
using System.Windows;

namespace Dotnetstore.WPF.Intranet.ViewModels.Containers;

public partial class MainContainerViewModel : BaseViewModel, IMainContainerViewModel
{
    [ObservableProperty]
    private bool? _isOpen;

    [ObservableProperty]
    private string? _test;

    public IRelayCommand MenuToggleCommand { get; set; }

    public MainContainerViewModel()
    {
        MenuToggleCommand = new RelayCommand(ExecuteToggleMenu);

        IsOpen = false;
        Test = IsOpen.ToString();
    }

    private void ExecuteToggleMenu()
    {
        IsOpen = !IsOpen;
        Test = IsOpen.ToString();
        MessageBox.Show(Test);
    }
}