using System;
using System.Windows.Input;
using System.Windows.Threading;
using CookbookMVVM;
using Domain.Interfaces;

namespace ViewModel.MainWindowVMs
{
  public class UpperPanelVM : ViewModelBase, IUpperPanelVM
  {
    #region private fields

    private readonly DispatcherTimer timer;
    private readonly ITimeProvider timeProvider;
    private ICommand toggleTimer;

    #endregion

    private bool CanToggleTimer()
    {
      return true;
    }

    private void ExecuteToggleTimer()
    {
      if (timer.IsEnabled)
        timer.Stop();
      else
        timer.Start();
      OnPropertyChanged(nameof(IsTimerRunning));
    }

    #region public properties

    public string Timer => "-01:00";

    public ICommand ToggleTimer =>
      toggleTimer ??
      (toggleTimer = new RelayCommand(ExecuteToggleTimer, CanToggleTimer));

    public bool IsTimerRunning => timer.IsEnabled;

    #endregion

    #region public methods

    public UpperPanelVM(ITimeProvider timeProvider)
    {
      this.timeProvider = timeProvider;
      timer = new DispatcherTimer
        {Interval = TimeSpan.FromSeconds(1)};
      timer.Tick += delegate { OnPropertyChanged(nameof(Timer)); };
    }

    #endregion
  }
}