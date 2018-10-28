using System;

namespace ViewModel.BottomPanelVMs
{
  public interface IReadOnlyTimer
  {
    event EventHandler<System.EventArgs> TimeChanged;

    TimeSpan Elapsed { get; }

    bool IsRunning { get; }
  }
}