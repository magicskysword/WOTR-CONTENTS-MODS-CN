using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ModFinder.Localization
{
    /// <summary>
    /// Converter to format author name with localized prefix
    /// </summary>
    public class AuthorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string author && !string.IsNullOrEmpty(author))
            {
                var format = Application.Current.Resources["AuthorBy"]?.ToString() ?? "by {0}";
                return string.Format(format, author);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
