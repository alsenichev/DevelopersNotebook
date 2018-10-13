using System;
using System.Collections.ObjectModel;
using ViewModel.EventArgs;
using ViewModel.ModelsVMs;

namespace ViewModel.CentralPanelVMs
{
  public interface ICentralPanelVM
  {

    event EventHandler<System.EventArgs> StartTimerRequested;
    event EventHandler<System.EventArgs> StopTimerRequested;

    ObservableCollection<NoteVM> Notes { get; }

    void LoadNotes();
    void HandleNoteCommand(object sender, NoteCommandEventArgs e);
  }
}