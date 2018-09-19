using System;
using System.Windows;
using ViewModel.Infrastructure.Logging;

namespace DevelopersNotebook.StartUp
{
  public class TimeTrackerInitialization : IDisposable
  {
    private readonly ITimeTrackerLogger logger;
    private Window window;

    public TimeTrackerInitialization(ITimeTrackerLogger logger)
    {
      this.logger = logger;
    }

    public void InitializeBeforeShowingTheWindow()
    {
      // add references of main window to view models that
      // contain dialogs, so they could have parent window
    }

    // ReSharper disable once UnusedMember.Global
    // The property is used by the IoC
    public Window Window
    {
      set => window = value;
    }

    public void ShowWindow()
    {
      window.Show();
    }

    public void InitializeAfterShowingTheWindow()
    {
      //refreshWorkItems.RefreshItems();
      //dataGridVM.UpdateTfsItems();
    }

    public void LogAndDisplayError(string message)
    {
      logger.Error(message);
    }

    public void Dispose()
    {
    }
  }
}