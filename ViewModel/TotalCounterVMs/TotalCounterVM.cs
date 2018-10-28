using System;
using CookbookMVVM;
using ViewModel.Enums;

namespace ViewModel.TotalCounterVMs
{
  public class TotalCounterVM : ViewModelBase, ITotalCounterVM
  {
    private TimeSpan counter = TimeSpan.MinValue;

    public string TotalTime => counter.ToString();

    public void InitCounter(TimeSpan value)
    {
      counter = value;
      OnPropertyChanged(nameof(TotalTime));
    }

    public void UpdateCounter(TimeSpan timeSpan)
    {
      counter += timeSpan;
      OnPropertyChanged(nameof(TotalTime));
    }
  }
}