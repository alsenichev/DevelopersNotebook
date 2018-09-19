using System;
using System.Windows;
using ViewModel.MainWindowVMs;

namespace DevelopersNotebook
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow(MainWindowVM mainWindowVM)
    {
      DataContext = mainWindowVM;
      InitializeComponent();
    }

    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e);
      Application.Current.Shutdown();
    }
  }
}