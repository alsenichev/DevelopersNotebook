using System;

namespace ViewModel.BottomPanelVMs
{
  public interface ITimer: IReadOnlyTimer
  {
    void Reset();
    void Start();
    void Stop();
  }
}