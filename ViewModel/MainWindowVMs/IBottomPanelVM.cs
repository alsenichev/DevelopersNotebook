using System;
using System.Windows.Input;

namespace ViewModel.MainWindowVMs
{
  public interface IBottomPanelVM
  {
    string TimerTime { get; }

    string TextInput { get; set; }

    ICommand ToggleTimer { get; }

    bool IsTimerRunning { get; }

    event EventHandler<string> TimerStarted;

    event EventHandler<TimeSpan> TimerStopped;
  }
}