using CommunityToolkit.Mvvm.Input;
using Dotnetstore.WPF.API.Settings.Interfaces;
using Dotnetstore.WPF.Intranet.Interfaces;
using Dotnetstore.WPF.Nuget.Core.Interfaces;
using Dotnetstore.WPF.Nuget.Core.ViewModels;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Dotnetstore.WPF.Intranet.ViewModels.Containers;

public class TopContainerViewModel : BaseViewModel, ITopContainerViewModel
{
    private IApplicationFileService? _applicationFileService;
    private ICultureService? _cultureService;
    private IEventService? _eventService;
    private IPersonalSettingService? _personalSettingService;
    private CultureInfo? _selectedCultureInfo;

    public TopContainerViewModel(
        IApplicationFileService applicationFileService,
        ICultureService cultureService,
        IEventService eventService,
        IPersonalSettingService personalSettingService)
    {
        _applicationFileService = applicationFileService;
        _cultureService = cultureService;
        _eventService = eventService;
        _personalSettingService = personalSettingService;

        CloseApplicationCommand = new RelayCommand(ExecuteCloseApplication);
        SetWindowMinimizeCommand = new RelayCommand(ExecuteSetWindowMinimize);
        SetWindowRestoreCommand = new RelayCommand(ExecuteSetWindowRestore);
    }

    public IRelayCommand? CloseApplicationCommand { get; set; }

    public CultureInfo[]? CultureInfoArray { get; set; }

    public ObservableCollection<CultureInfo>? CultureInfos { get; set; }

    public CultureInfo? SelectedCultureInfo
    {
        get => _selectedCultureInfo;
        set
        {
            SetProperty(ref _selectedCultureInfo, value);
            SaveCultureToSettingFile();
        }
    }

    public IRelayCommand? SetWindowMinimizeCommand { get; set; }

    public IRelayCommand? SetWindowRestoreCommand { get; set; }

    void ITopContainerViewModel.Load()
    {
        LoadCultureInfos();
        SetCurrentCultureInfo();
    }

    protected override void DisposeManaged()
    {
        if (!IsDisposed)
        {
            _applicationFileService = null;
            _cultureService = null;
            _eventService = null;
            _personalSettingService = null;

            CloseApplicationCommand = null;
            SetWindowMinimizeCommand = null;
            SetWindowRestoreCommand = null;

            CultureInfos = null;
            SelectedCultureInfo = null;
            CultureInfoArray = null;
        }

        base.DisposeManaged();
    }

    private void ExecuteCloseApplication()
    {
        _eventService?.RunCloseApplication();
    }

    private void ExecuteSetWindowMinimize()
    {
        _eventService?.RunSetWindowMinimize();
    }

    private void ExecuteSetWindowRestore()
    {
        _eventService?.RunSetWindowRestore();
    }

    private void LoadCultureInfos()
    {
        if (_cultureService is null)
        {
            return;
        }

        CultureInfoArray = _cultureService.GetCultures();
        CultureInfos = new ObservableCollection<CultureInfo>(CultureInfoArray);
    }

    private void SaveCultureToSettingFile()
    {
        if (_personalSettingService is null ||
            _applicationFileService is null ||
            _cultureService is null ||
            SelectedCultureInfo == null)
        {
            return;
        }

        var file = _applicationFileService.PersonalSettingFile;

        if (string.IsNullOrWhiteSpace(file))
        {
            return;
        }

        var personalSettings = _personalSettingService.Get(file);

        if (personalSettings == null)
        {
            return;
        }

        personalSettings.Culture = SelectedCultureInfo.Name;
        _personalSettingService.SaveAsync(file, personalSettings);
        _cultureService.SetCulture(SelectedCultureInfo.Name);
    }

    private void SetCurrentCultureInfo()
    {
        if (_personalSettingService is null ||
            _applicationFileService is null ||
            CultureInfoArray == null)
        {
            return;
        }

        var file = _applicationFileService.PersonalSettingFile;

        if (string.IsNullOrWhiteSpace(file))
        {
            return;
        }

        var personalSettings = _personalSettingService.Get(file);

        if (personalSettings == null)
        {
            return;
        }

        SelectedCultureInfo = CultureInfoArray.FirstOrDefault(q => q.Name == personalSettings.Culture);
    }
}