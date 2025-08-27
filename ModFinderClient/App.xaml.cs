using System.Windows;
using ModFinder.Localization;
using ModFinder.Util;

namespace ModFinder
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
      
      // Initialize localization with saved language preference
      var settings = Settings.Load();
      LocalizationManager.Instance.SetLanguage(settings.Language);
    }
  }
}
