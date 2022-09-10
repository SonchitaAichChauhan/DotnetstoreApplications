namespace Dotnetstore.WPF.Intranet.Interfaces;

public interface IApplicationService
{
    IApplicationService? LoadSettings();

    void Run();
}