using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CookbookMVVM;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using ViewModel.BottomPanelVMs;
using ViewModel.ModelsVMs;

namespace ViewModel.CentralPanelVMs
{
  public class CentralPanelVM : ViewModelBase, ICentralPanelVM
  {
    public event EventHandler<EventArgs> ItemsPositionChanged;
    public event EventHandler<EventArgs> NoteCommandReceived;

    private readonly ITimeProvider timeProvider;
    private readonly INoteFactory noteFactory;
    private readonly IReadOnlyTimer timer;

    private NoteVM runningNoteVM;

    public CentralPanelVM(
      ITimeProvider timeProvider,
      INoteFactory noteFactory,
      IReadOnlyTimer timer)
    {
      this.timeProvider = timeProvider;
      this.noteFactory = noteFactory;
      this.timer = timer;
      timer.TimeChanged += OnTimerTimeChanged;
    }

    public ObservableCollection<NoteVM> Notes { get; private set; }

    public void InitializeNotes(IList<Note> source)
    {
      Notes = new ObservableCollection<NoteVM>(source.Select(n => CreateNoteVM(n)));
      OnPropertyChanged(nameof(Notes));
    }

    public void CreateNote(string text)
    {
      var note = noteFactory.CreateNote(text);
      PlaceToLast(new NoteVM {Model = note});
    }

    public void CreateNewTask(string text)
    {
      var note = noteFactory.CreateTask(text, timeProvider.Now);
      var noteVM = CreateNoteVM(note);
      runningNoteVM = noteVM;
      PlaceToLast(CreateNoteVM(note));
    }

    public void ContinueTask(NoteVM noteVM)
    {
      var updatedNote = noteFactory.ResumedTask(noteVM.Model);
      var updatedVM = CreateNoteVM(updatedNote);
      runningNoteVM = updatedVM;
      PlaceToLast(updatedVM, noteVM);
    }

    public void StopTask(TimeSpan elapsedTimer)
    {
      var timeStopped = timeProvider.Now;
      var noteVM = Notes.Single(n => n.Model.State == NoteState.TimerRunning);
      var updatedNote = noteFactory.StoppedTask(noteVM.Model, timeStopped, elapsedTimer);
      var updatedVM = CreateNoteVM(updatedNote);
      runningNoteVM = null;
      PlaceToLast(updatedVM, noteVM);
    }

    public void StopAnyRunningTask(TimeSpan elapsedTimer)
    {
      var timeStopped = timeProvider.Now;
      var noteVM = Notes.SingleOrDefault(n => n.Model.State == NoteState.TimerRunning);
      if (noteVM != null)
      {
        var updatedNote = noteFactory.StoppedTask(noteVM.Model, timeStopped, elapsedTimer);
        var updatedVM = new NoteVM { Model = updatedNote };
        runningNoteVM = null;
        PlaceToLast(updatedVM, noteVM);
      }
    }

    private void PlaceToLast(NoteVM newNote, NoteVM oldNote = null)
    {
      if (oldNote != null)
      {
        int index = Notes.IndexOf(oldNote);
        Notes.RemoveAt(index);
        oldNote.ToggleRunningStateRequested -= OnNoteToggleRunningStateRequested;
      }
      Notes.Add(newNote);
      OnPropertyChanged(nameof(Notes));
      ItemsPositionChanged?.Invoke(this,EventArgs.Empty);
    }

    private NoteVM CreateNoteVM(Note model)
    {
      var result = new NoteVM{Model=model};
      result.ToggleRunningStateRequested += OnNoteToggleRunningStateRequested;
      return result;
    }

    private void OnNoteToggleRunningStateRequested(object sender, EventArgs e)
    {
      NoteCommandReceived?.Invoke(sender, e);
    }

    private void OnTimerTimeChanged(object sender, EventArgs e)
    {
      runningNoteVM?.UpdateDuration(timer.Elapsed);
    }
  }
}