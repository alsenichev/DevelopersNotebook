using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ViewModel.MainWindowVMs;

namespace DevelopersNotebook
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private MainWindowVM mainWindowVM;
    public MainWindow(MainWindowVM mainWindowVM)
    {
      DataContext = mainWindowVM;
      this.mainWindowVM = mainWindowVM;
      InitializeComponent();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      mainWindowVM.BottomPanelVM.Shutdown();
    }

    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e);
      Application.Current.Shutdown();
    }

    private void MainText_OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
      mainWindowVM.BottomPanelVM.OnTextInputPreviewKeyDown(e.Key);
    }
  }
}