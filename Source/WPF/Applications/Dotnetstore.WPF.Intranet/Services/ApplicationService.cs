using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Intranet.ViewModels.Containers;
using Dotnetstore.WPF.Intranet.Views.Containers;
using Dotnetstore.WPF.Nuget.Core.Abstracts;
using Dotnetstore.WPF.Nuget.Core.Interfaces;
using Dotnetstore.WPF.Nuget.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.WPF.Intranet.Services;

public class ApplicationService : Disposable, IApplicationService
{
    private string? _httpDotnetstoreWebAPIClientBaseAddress;
    private string? _httpDotnetstoreWebAPIClientName;
    private IJsonSettingFileReaderService? _jsonSettingFileReaderService;
    private MainContainerView? _mainContainerView;
    private IMainContainerViewModel? _mainContainerViewModel;

    public ApplicationService()
    {
    }

    public ApplicationService(
        IMainContainerViewModel mainContainerViewModel,
        MainContainerView mainContainerView)
    {
        _mainContainerViewModel = mainContainerViewModel;
        _mainContainerView = mainContainerView;
    }

    IApplicationService? IApplicationService.LoadSettings()
    {
        LoadObjects();
        LoadSettingParameters();
        return LoadIoC();
    }

    void IApplicationService.Run()
    {
        if (_mainContainerView == null)
        {
            return;
        }

        _mainContainerView.DataContext = _mainContainerViewModel;
        _mainContainerView.Show();
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _mainContainerViewModel = null;
            _mainContainerView = null;
            _jsonSettingFileReaderService = null;

            _httpDotnetstoreWebAPIClientName = null;
            _httpDotnetstoreWebAPIClientBaseAddress = null;
        }

        base.DisposeManaged();
    }

    private IApplicationService? LoadIoC()
    {
        if (string.IsNullOrWhiteSpace(_httpDotnetstoreWebAPIClientName) ||
            string.IsNullOrWhiteSpace(_httpDotnetstoreWebAPIClientBaseAddress))
            return new ApplicationService();

        IServiceCollection serviceCollection = new ServiceCollection();
        IoC.ServiceCollectionBootStrap.Build(ref serviceCollection, _httpDotnetstoreWebAPIClientName, _httpDotnetstoreWebAPIClientBaseAddress);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var applicationService = serviceProvider.GetService<IApplicationService>();
        return applicationService;
    }

    private void LoadObjects()
    {
        IConfigurationBuilderSingletonService configurationBuilderSingletonService =
            new ConfigurationBuilderSingletonService();
        _jsonSettingFileReaderService = new JsonSettingFileReaderService(configurationBuilderSingletonService);
    }

    private void LoadSettingParameters()
    {
        if (_jsonSettingFileReaderService == null)
        {
            return;
        }

        _httpDotnetstoreWebAPIClientName = _jsonSettingFileReaderService.GetString("API:DotnetstoreIntranet:HttpDotnetstoreWebAPIClientName");
        _httpDotnetstoreWebAPIClientBaseAddress = _jsonSettingFileReaderService.GetString("API:DotnetstoreIntranet:HttpDotnetstoreWebAPIClientBaseAddress");
    }
}