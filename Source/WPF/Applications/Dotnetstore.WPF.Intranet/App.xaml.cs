using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Intranet.Services;
using System.Threading.Tasks;
using System.Windows;

namespace Dotnetstore.WPF.Intranet;

public partial class App
{
    private IApplicationService? _applicationService;
    private ISetupService? _setupService;

    private void AppOnStartup(object sender, StartupEventArgs e)
    {
        LoadApplication();
        RunSetup();
        SetCulture();
        RunApplicationAsync().GetAwaiter().GetResult();
    }

    private void LoadApplication()
    {
        _applicationService = new ApplicationService();
        var (setupService, applicationService) = _applicationService.Load();
        _applicationService = applicationService;
        _setupService = setupService;
    }

    private async Task RunApplicationAsync()
    {
        if (_applicationService is null)
        {
            return;
        }

        await _applicationService.RunAsync();
    }

    private void RunSetup()
    {
        _setupService?.Run();
    }

    private void SetCulture()
    {
        _applicationService?.LoadCulture();
    }
}