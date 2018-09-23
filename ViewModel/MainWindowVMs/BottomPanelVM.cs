using System;
using System.Windows.Input;
using System.Windows.Threading;
using CookbookMVVM;
using Domain.Interfaces;
using Domain.Services;

namespace ViewModel.MainWindowVMs
{
  public class BottomPanelVM : ViewModelBase, IBottomPanelVM
  {
    #region private fields

    private readonly DispatcherTimer dispatcherTimer;
    private readonly TimeSpan timerStep = TimeSpan.FromSeconds(1);
    private readonly Timer timer;
    private ICommand toggleTimer;

    #endregion

    private bool CanToggleTimer()
    {
      return true;
    }

    private void ExecuteToggleTimer()
    {
      if (dispatcherTimer.IsEnabled)
        dispatcherTimer.Stop();
      else
        dispatcherTimer.Start();
      OnPropertyChanged(nameof(IsTimerRunning));
    }

    #region public properties

    public string Timer => timer.Elapsed.ToString();

    public ICommand ToggleTimer =>
      toggleTimer ??
      (toggleTimer = new RelayCommand(ExecuteToggleTimer, CanToggleTimer));

    public bool IsTimerRunning => dispatcherTimer.IsEnabled;

    #endregion

    #region public methods

    public BottomPanelVM()
    {
      timer = new Timer();
      dispatcherTimer = new DispatcherTimer
        {Interval = timerStep};
      dispatcherTimer.Tick += DispatcherTimerOnTick;
    }

    private void DispatcherTimerOnTick(object sender, EventArgs eventArgs)
    {
      timer.Increment(timerStep);
      OnPropertyChanged(nameof(Timer));
    }

    #endregion
  }
}