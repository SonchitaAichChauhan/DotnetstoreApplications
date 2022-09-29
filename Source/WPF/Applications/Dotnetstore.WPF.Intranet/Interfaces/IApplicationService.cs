using System.Threading.Tasks;

namespace Dotnetstore.WPF.Intranet.Interfaces;

public interface IApplicationService
{
    (ISetupService? setupService, IApplicationService? applicationService) Load();

    void LoadCulture();

    Task RunAsync();
}