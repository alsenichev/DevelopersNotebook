using System;
using System.Reflection;
using System.Windows;
using log4net;

namespace DevelopersNotebook.StartUp
{
  /// <summary>
  /// Performs necessary actions during the start of the application.  
  /// </summary>
  public class ApplicationInitialization : IDisposable
  {
    private static readonly ILog logger =
      LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    private Window window;

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