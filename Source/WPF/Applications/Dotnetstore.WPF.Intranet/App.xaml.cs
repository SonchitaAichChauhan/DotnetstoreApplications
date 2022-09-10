using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Intranet.Services;
using System.Windows;

namespace Dotnetstore.WPF.Intranet;

public partial class App
{
    private IApplicationService? _applicationService;

    private void AppOnStartup(object sender, StartupEventArgs e)
    {
        LoadObjects();
        LoadSettings();
        RunApplication();
    }

    private void LoadSettings()
    {
        _applicationService = _applicationService?.LoadSettings();
    }

    private void LoadObjects()
    {
        _applicationService = new ApplicationService();
    }

    private void RunApplication()
    {
        _applicationService?.Run();
    }
}