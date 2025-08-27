using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace ModFinder.Util
{
  public class Settings
  {
    public string AutoWrathPath { get; set; }
    public string WrathPath { get; set; }
    public byte[] NexusApiKeyBytes { get; set; }
    public string Language { get; set; } = "en-US";
    public bool IsFirstRun { get; set; } = true;
    public string MaybeGetNexusKey()
    {
      if (NexusApiKeyBytes == null)
      {
        return null;
      }
      var plain = ProtectedData.Unprotect(NexusApiKeyBytes, null, DataProtectionScope.CurrentUser);
      return Encoding.UTF8.GetString(plain);
    }

    private static Settings _Instance;
    public static Settings Load()
    {
      if (_Instance == null)
      {
        if (Main.TryReadFile("Settings.json", out var settingsRaw))
        {
          _Instance = IOTool.FromString<Settings>(settingsRaw);
        }
        else
        {
          // First run - create new settings with auto-detected language
          _Instance = new();
          _Instance.Language = DetectSystemLanguage();
          _Instance.IsFirstRun = false; // Mark as no longer first run
          _Instance.Save(); // Save the initial settings
        }
      }

      return _Instance;
    }

    /// <summary>
    /// Detect system language and return appropriate language code
    /// </summary>
    /// <returns>Language code for localization</returns>
    private static string DetectSystemLanguage()
    {
      try
      {
        var culture = CultureInfo.CurrentUICulture;
        var languageCode = culture.TwoLetterISOLanguageName.ToLower();
        
        // Check if it's Chinese (Simplified)
        if (languageCode == "zh" || culture.Name.StartsWith("zh-CN", System.StringComparison.OrdinalIgnoreCase))
        {
          return "zh-CN";
        }
        
        // Default to English for all other languages
        return "en-US";
      }
      catch
      {
        // Fallback to English if detection fails
        return "en-US";
      }
    }

    public void Save()
    {
      IOTool.SafeRun(() =>
      {
        IOTool.Write(this, Main.AppPath("Settings.json"));
      });
    }
  }
}
