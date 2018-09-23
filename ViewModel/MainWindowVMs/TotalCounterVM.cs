using System;
using CookbookMVVM;

namespace ViewModel.MainWindowVMs
{
  public class TotalCounterVM : ViewModelBase, ITotalCounterVM
  {
    private TimeSpan counter;

    public void Add(TimeSpan timeSpan)
    {
      counter += timeSpan;
      OnPropertyChanged(nameof(TotalTime));
    }

    public string TotalTime => counter.ToString();

    public TotalCounterVM()
    {
      counter = TimeSpan.Zero;
    }
  }
}