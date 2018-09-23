using System;
using System.Windows.Input;

namespace ViewModel.MainWindowVMs
{
  public interface IBottomPanelVM
  {
    string TimerTime { get; }

    ICommand ToggleTimer { get; }

    bool IsTimerRunning { get; }

    event EventHandler<EventArgs> TimerStarted;

    event EventHandler<TimeSpan> TimerStopped;
  }
}