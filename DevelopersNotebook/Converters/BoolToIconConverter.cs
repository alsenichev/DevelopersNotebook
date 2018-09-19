using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevelopersNotebook.UserControls;

namespace DevelopersNotebook.Converters
{
  class BoolToIconConverter:IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if ((bool) value)
      {
        return (SvgIcon) Application.Current.FindResource("PauseIcon");
      }
      else
      {
        return (SvgIcon)Application.Current.FindResource("PlayIcon");
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
