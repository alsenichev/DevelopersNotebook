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
    private IMainWindowVM mainWindowVM;
    public MainWindow(IMainWindowVM mainWindowVM)
    {
      DataContext = mainWindowVM;
      this.mainWindowVM = mainWindowVM;
      this.mainWindowVM.ScrollDownRequested += OnMainWindowVMScrollDownRequested;
      InitializeComponent();
    }

    private void OnMainWindowVMScrollDownRequested(object sender, EventArgs e)
    {
      mainListBox.Items.MoveCurrentToLast();
      mainListBox.ScrollIntoView(mainListBox.Items.CurrentItem);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      mainWindowVM.PrepareToShutdownApplication();
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