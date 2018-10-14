using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Models;
using ViewModel.EventArgs;
using ViewModel.ModelsVMs;

namespace ViewModel.CentralPanelVMs
{
  public interface ICentralPanelVM
  {

    event EventHandler<System.EventArgs> StartTimerRequested;
    event EventHandler<System.EventArgs> StopTimerRequested;

    ObservableCollection<NoteVM> Notes { get; }

    void InitializeNotes(IList<Note> notes);
    void HandleNoteCommand(object sender, NoteCommandEventArgs e);
  }
}