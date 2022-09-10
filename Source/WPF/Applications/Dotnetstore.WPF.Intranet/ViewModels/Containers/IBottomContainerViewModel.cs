using Dotnetstore.WPF.Nuget.Core.ViewModels;
using System.Threading.Tasks;

namespace Dotnetstore.WPF.Intranet.ViewModels.Containers;

public interface IBottomContainerViewModel : IBaseViewModel
{
    Task LoadAsync();
}