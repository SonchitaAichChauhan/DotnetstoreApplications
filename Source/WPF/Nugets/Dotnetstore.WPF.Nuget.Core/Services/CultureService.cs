using Dotnetstore.WPF.Nuget.Core.Interfaces;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace Dotnetstore.WPF.Nuget.Core.Services;

public sealed class CultureService : ICultureService
{
    void ICultureService.SetCulture(string cultureName)
    {
        var cultureInfo = new CultureInfo(cultureName);

        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        try
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
        catch
        {
        }
    }

    CultureInfo[] ICultureService.GetCultures()
    {
        return CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
    }
}