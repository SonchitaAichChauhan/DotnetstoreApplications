using Dotnetstore.WPF.API.Settings.Interfaces;
using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Intranet.ViewModels.Containers;
using Dotnetstore.WPF.Intranet.Views.Containers;
using Dotnetstore.WPF.Nuget.Core.Abstracts;
using Dotnetstore.WPF.Nuget.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Dotnetstore.WPF.Intranet.Services;

public sealed class ApplicationService : Disposable, IApplicationService
{
    private IApplicationFileService? _applicationFileService;
    private IConfiguration? _configuration;
    private ICultureService? _cultureService;
    private MainContainerView? _mainContainerView;
    private IMainContainerViewModel? _mainContainerViewModel;
    private IPersonalSettingService? _personalSettingService;

    public ApplicationService()
    {
    }

    public ApplicationService(
        IApplicationFileService applicationFileService,
        IConfiguration configuration,
        IMainContainerViewModel mainContainerViewModel,
        IPersonalSettingService personalSettingService,
        MainContainerView mainContainerView)
    {
        _applicationFileService = applicationFileService;
        _configuration = configuration;
        _mainContainerViewModel = mainContainerViewModel;
        _personalSettingService = personalSettingService;
        _mainContainerView = mainContainerView;
    }

    (ISetupService? setupService, IApplicationService? applicationService) IApplicationService.Load()
    {
        LoadConfig();
        return LoadIoC();
    }

    void IApplicationService.LoadCulture()
    {
        if (_cultureService is null ||
            _personalSettingService is null ||
            _applicationFileService is null)
        {
            return;
        }

        var personalSettingFile = _applicationFileService.PersonalSettingFile;

        if (string.IsNullOrWhiteSpace(personalSettingFile))
        {
            return;
        }

        var personalSetting = _personalSettingService.Get(personalSettingFile);
        var culture = personalSetting?.Culture;
        _cultureService.SetCulture(string.IsNullOrWhiteSpace(culture) ? "en-US" : culture);
    }

    async Task IApplicationService.RunAsync()
    {
        if (_mainContainerView is null ||
            _mainContainerViewModel is null)
        {
            return;
        }

        await _mainContainerViewModel.LoadAsync();
        _mainContainerView.DataContext = _mainContainerViewModel;
        _mainContainerView.Show();
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _applicationFileService = null;
            _mainContainerViewModel = null;
            _mainContainerView = null;
            _cultureService = null;
            _personalSettingService = null;
        }

        base.DisposeManaged();
    }

    private void LoadConfig()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    private (ISetupService? setupService, IApplicationService? applicationService) LoadIoC()
    {
        if (_configuration is null)
        {
            return (null, null);
        }

        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(_configuration);

        var httpDotnetstoreWebAPIClientName = _configuration.GetSection("API:DotnetstoreIntranet:HttpDotnetstoreWebAPIClientName").Value;
        var httpDotnetstoreWebAPIClientBaseAddress = _configuration.GetSection("API:DotnetstoreIntranet:HttpDotnetstoreWebAPIClientBaseAddress").Value;

        if (string.IsNullOrWhiteSpace(httpDotnetstoreWebAPIClientName) ||
            string.IsNullOrWhiteSpace(httpDotnetstoreWebAPIClientBaseAddress))
        {
            return (null, null);
        }

        IoC.ServiceCollectionBootStrap.Build(ref serviceCollection, httpDotnetstoreWebAPIClientName, httpDotnetstoreWebAPIClientBaseAddress);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        return (serviceProvider.GetService<ISetupService>(), serviceProvider.GetService<IApplicationService>());
    }
}