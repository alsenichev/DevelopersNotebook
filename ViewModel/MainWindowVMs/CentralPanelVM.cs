using System;
using System.Collections.ObjectModel;
using System.Linq;
using CookbookMVVM;
using Domain.Interfaces;
using Domain.Models;
using ViewModel.ModelsVMs;

namespace ViewModel.MainWindowVMs
{
  public class CentralPanelVM : ViewModelBase, ICentralPanelVM
  {
    private readonly ObservableCollection<NoteVM> notes;
    private readonly ITimeProvider timeProvider;

    public ObservableCollection<NoteVM> Notes => notes;

    public void AddTimerNote(object sender, string e)
    {
      //TODO make a NotesFactory
      var timeStarted = timeProvider.Now;
      var note = new Note($"Started at: {timeStarted}", e,
        NoteState.RunningTimer, timeStarted, DateTime.MinValue);
      notes.Add(new NoteVM
        {Model = note});
      OnPropertyChanged(nameof(Notes));
    }

    public void EndTimerNote(object sender, TimeSpan e)
    {
      var timeStopped = timeProvider.Now;
      var note = notes.Single(n => n.Model.State == NoteState.RunningTimer);
      notes.Remove(note);
      var updatedNote =
        new Note($"Task run from {note.Model.TimerStarted} to {timeStopped}. Duration: {e}",
          note.Header, NoteState.TimerStopped, note.Model.TimerStarted,
          timeStopped);
      notes.Add(new NoteVM
        {Model = updatedNote});
      OnPropertyChanged(nameof(Notes));
    }

    public CentralPanelVM(ITimeProvider timeProvider)
    {
      this.timeProvider = timeProvider;
      notes = new ObservableCollection<NoteVM>();
    }
  }
}