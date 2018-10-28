using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DevelopersNotebook.Converters
{
  /// <summary>
  /// Makes the bottom panel of the NoteVM highlighted if the task is running.
  /// </summary>
  public class NoteStatusConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      bool isRunning = (bool) value;
      if (isRunning)
      {
        return Application.Current.Resources["RunningNoteBackground"] as Brush;
      }

      return Application.Current.Resources["NoteBackground"] as Brush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}