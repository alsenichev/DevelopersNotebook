using System;

namespace ViewModel.TotalCounterVMs
{
  public interface ITotalCounterVM
  {
    string TotalTime { get; }

    void InitCounter(TimeSpan initialValue);

    void UpdateCounter(TimeSpan timeSpan);
  }
}