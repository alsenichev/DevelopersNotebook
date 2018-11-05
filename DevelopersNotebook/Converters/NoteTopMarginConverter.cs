using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevelopersNotebook.Converters
{
  public class NoteTopMarginConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      bool isFirstToday = (bool) value;
      if (isFirstToday)
      {
        return new Thickness(0, 22, 0, 0);
      }

      return new Thickness(0, 0, 0, 0);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}