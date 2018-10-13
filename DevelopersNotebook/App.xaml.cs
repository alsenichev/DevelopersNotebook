using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using Castle.Windsor;
using Castle.Windsor.Installer;
using DevelopersNotebook.Infrastructure.Logging;
using DevelopersNotebook.StartUp;

namespace DevelopersNotebook
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private WindsorContainer container;
    private ApplicationInitialization appInit;

    public App()
    {
      ArrangeLogger();
    }

    private void ArrangeLogger()
    {
      // Log files are session-wise
      Logger.Setup(Path.Combine(
        "Logs", $"DevelopersNotebook{DateTime.Now:dd-MM-yyyy-HH-mm-ss}LogEvents.txt"));
    }

    protected override void OnStartup(StartupEventArgs e)
    {
      container = new WindsorContainer();
      container.Install(FromAssembly.This());
      appInit = container.Resolve<ApplicationInitialization>();

      // prepare view-models before showing main window
      appInit.InitializeBeforeShowingTheWindow();
      appInit.ShowWindow();

      // to have the ability to show messages, we must have main Window shown
      // hence attach central error handler after the window is displayed
      DispatcherUnhandledException += App_DispatcherUnhandledException;
      appInit.InitializeAfterShowingTheWindow();
    }

    private void App_DispatcherUnhandledException(
      object sender,
      DispatcherUnhandledExceptionEventArgs e)
    {
      appInit.LogAndDisplayError(
        $"Unexpected error: {e.Exception.Message}");
      appInit.Dispose();

      //handle error, otherwise Windows will complain
      e.Handled = true;
    }

    protected override void OnExit(ExitEventArgs e)
    {
      base.OnExit(e);
      if (container == null)
        return;
      container.Release(appInit);
      container.Dispose();
    }
  }
}