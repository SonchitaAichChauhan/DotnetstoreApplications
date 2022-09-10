using System.Threading.Tasks;

namespace Dotnetstore.WPF.Intranet.Interfaces;

public interface IApplicationService
{
    IApplicationService? LoadSettings();

    Task RunAsync();
}