using System.Globalization;

namespace Dotnetstore.WPF.Nuget.Core.Interfaces;

public interface ICultureService
{
    void SetCulture(string cultureName);

    CultureInfo[] GetCultures();
}