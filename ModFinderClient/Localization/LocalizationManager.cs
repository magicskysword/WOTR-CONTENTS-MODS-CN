using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using ModFinder.Util;

namespace ModFinder.Localization
{
    /// <summary>
    /// Manages application localization and language switching
    /// </summary>
    public class LocalizationManager : INotifyPropertyChanged
    {
        private static LocalizationManager _instance;
        public static LocalizationManager Instance => _instance ??= new LocalizationManager();

        private readonly Dictionary<string, ResourceDictionary> _languageResources = new();
        private string _currentLanguage = "en-US";

        public event PropertyChangedEventHandler PropertyChanged;

        private LocalizationManager()
        {
            LoadLanguageResources();
            SetLanguage(_currentLanguage);
        }

        /// <summary>
        /// Current application language code
        /// </summary>
        public string CurrentLanguage
        {
            get => _currentLanguage;
            private set
            {
                if (_currentLanguage != value)
                {
                    _currentLanguage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentLanguage)));
                }
            }
        }

        /// <summary>
        /// Available languages
        /// </summary>
        public List<LanguageInfo> AvailableLanguages { get; } = new()
        {
            new LanguageInfo("en-US", "English"),
            new LanguageInfo("zh-CN", "中文")
        };

        /// <summary>
        /// Load all language resource dictionaries
        /// </summary>
        private void LoadLanguageResources()
        {
            foreach (var language in AvailableLanguages)
            {
                try
                {
                    var resourceDict = new ResourceDictionary
                    {
                        Source = new Uri($"pack://application:,,,/Localization/{language.Code}.xaml")
                    };
                    _languageResources[language.Code] = resourceDict;
                }
                catch (Exception)
                {
                    // Fallback: create empty resource dictionary if file doesn't exist
                    _languageResources[language.Code] = new ResourceDictionary();
                }
            }
        }

        /// <summary>
        /// Set the current application language
        /// </summary>
        /// <param name="languageCode">Language code (e.g., "en-US", "zh-CN")</param>
        public void SetLanguage(string languageCode)
        {
            if (!_languageResources.ContainsKey(languageCode))
            {
                Logger.Log.Warning($"Language '{languageCode}' not found, using default.");
                return;
            }

            Logger.Log.Info($"Setting language to: {languageCode}");

            // Remove existing language resources
            var resourcesToRemove = new List<ResourceDictionary>();
            foreach (var dict in Application.Current.Resources.MergedDictionaries)
            {
                if (dict.Source?.ToString().Contains("/Localization/") == true)
                {
                    resourcesToRemove.Add(dict);
                }
            }

            foreach (var dict in resourcesToRemove)
            {
                Application.Current.Resources.MergedDictionaries.Remove(dict);
            }

            // Add new language resources
            Application.Current.Resources.MergedDictionaries.Add(_languageResources[languageCode]);

            CurrentLanguage = languageCode;
            
            // Set thread culture for proper formatting
            var culture = new CultureInfo(languageCode);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            
            Logger.Log.Info($"Language set successfully to: {languageCode}");
        }

        /// <summary>
        /// Get localized string by key
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <param name="defaultValue">Default value if key not found</param>
        /// <returns>Localized string</returns>
        public string GetString(string key, string defaultValue = null)
        {
            try
            {
                var resource = Application.Current.Resources[key];
                return resource?.ToString() ?? defaultValue ?? key;
            }
            catch
            {
                return defaultValue ?? key;
            }
        }
    }

    /// <summary>
    /// Language information
    /// </summary>
    public class LanguageInfo
    {
        public string Code { get; }
        public string DisplayName { get; }

        public LanguageInfo(string code, string displayName)
        {
            Code = code;
            DisplayName = displayName;
        }
    }
}
