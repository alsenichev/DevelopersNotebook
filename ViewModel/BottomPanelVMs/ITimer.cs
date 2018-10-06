using System;

namespace ViewModel.BottomPanelVMs
{
  public interface ITimer
  {
    TimeSpan Elapsed { get; }
    bool IsRunning { get; }

    event EventHandler<System.EventArgs> TimeChanged;

    void Reset();
    void Start();
    void Stop();
  }
}