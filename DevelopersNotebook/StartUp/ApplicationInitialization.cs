using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using Domain.BusinessRules.Services;
using Domain.Interfaces;
using log4net;
using ViewModel.CentralPanelVMs;
using ViewModel.TotalCounterVMs;

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
    private readonly ICentralPanelVM centralPanelVM;
    private readonly ITotalCounterVM totalCounterVM;
    private readonly IMainRepository mainRepository;
    private readonly DailyTimeCalculation dailyTimeCalculation;

    public ApplicationInitialization(ICentralPanelVM centralPanelVM, IMainRepository mainRepository, DailyTimeCalculation dailyTimeCalculation, ITotalCounterVM totalCounterVM)
    {
      this.centralPanelVM = centralPanelVM;
      this.mainRepository = mainRepository;
      this.dailyTimeCalculation = dailyTimeCalculation;
      this.totalCounterVM = totalCounterVM;
    }

    public void InitializeBeforeShowingTheWindow()
    {
      // add references of main window to view models that
      // contain dialogs, so they could have parent window
      var notes = mainRepository.LoadNotes().Select(n => n.FromDisk()).ToList();
      centralPanelVM.InitializeNotes(notes);
      var counterValue = dailyTimeCalculation.CalculateTimeForToday(notes);
      totalCounterVM.InitCounter(counterValue);
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
      // some tasks that may show dialogs
    }

    public void LogAndDisplayError(string message)
    {
      // TODO need to notify the user somehow
      logger.Error(message);
    }

    public void Dispose()
    {
    }
  }
}