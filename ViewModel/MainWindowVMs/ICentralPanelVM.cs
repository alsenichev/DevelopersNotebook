using System;
using System.Collections.ObjectModel;
using ViewModel.ModelsVMs;

namespace ViewModel.MainWindowVMs
{
  public interface ICentralPanelVM
  {
    ObservableCollection<NoteVM> Notes { get; }

    void AddTimerNote(object sender, EventArgs e);

    void EndTimerNote(object sender, TimeSpan e);
  }
}