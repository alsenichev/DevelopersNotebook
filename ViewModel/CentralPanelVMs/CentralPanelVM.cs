using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CookbookMVVM;
using Domain.BusinessRules.Services;
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
    private readonly DailyTimeCalculation dailyTimeCalculation;

    private NoteVM runningNoteVM;

    public CentralPanelVM(
      ITimeProvider timeProvider,
      INoteFactory noteFactory,
      IReadOnlyTimer timer,
      DailyTimeCalculation dailyTimeCalculation)
    {
      this.timeProvider = timeProvider;
      this.noteFactory = noteFactory;
      this.timer = timer;
      this.dailyTimeCalculation = dailyTimeCalculation;
      timer.TimeChanged += OnTimerTimeChanged;
    }

    public ObservableCollection<NoteVM> Notes { get; private set; }

    public void InitializeNotes(IList<Note> source)
    {
      var noteVMs = source.Select(n => CreateNoteVM(n));
      Notes = new ObservableCollection<NoteVM>(noteVMs);
      GroupNotesByDate();
      OnPropertyChanged(nameof(Notes));
    }

    private void GroupNotesByDate()
    {
      DateTime date = DateTime.MinValue;
      foreach (var vm in Notes)
      {
        vm.SetIsFirstInADay(false);
        var taskStarted = dailyTimeCalculation.GetTaskDate(vm.Model);
        if (date == DateTime.MinValue)
        {
          date = taskStarted;
        }
        if (taskStarted > date)
        {
          vm.SetIsFirstInADay(true);
          date = taskStarted;
        }
      }
    }

    public void CreateNote(string text)
    {
      var note = noteFactory.CreateNote(text, timeProvider.Now);
      var noteVM = new NoteVM{Model = note};
      PlaceToLast(noteVM);
    }

    public void CreateTask(string text)
    {
      var note = noteFactory.CreateTask(text, timeProvider.Now);
      var noteVM = CreateNoteVM(note);
      runningNoteVM = noteVM;
      runningNoteVM.UpdateDuration(TimeSpan.Zero);// will immediately display a counter
      PlaceToLast(noteVM);
    }

    public void ContinueTask(NoteVM noteVM)
    {
      var updatedNote = noteFactory.ResumedTask(noteVM.Model);
      var updatedVM = CreateNoteVM(updatedNote);
      runningNoteVM = updatedVM;
      runningNoteVM.UpdateDuration(TimeSpan.Zero);// will immediately display a counter
      PlaceToLast(updatedVM, noteVM);
    }

    public void StopTask(TimeSpan elapsedTimer)
    {
      var timeStopped = timeProvider.Now;
      var noteVM = Notes.SingleOrDefault(n => n.Model.State == NoteState.TimerRunning);
      if (noteVM != null)
      {
        var updatedNote = noteFactory.StoppedTask(noteVM.Model, timeStopped, elapsedTimer);
        var updatedVM = CreateNoteVM(updatedNote);
        runningNoteVM = null;
        PlaceToLast(updatedVM, noteVM); //TODO update in place (there may be notes below)
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
      GroupNotesByDate();
      OnPropertyChanged(nameof(Notes));
      ItemsPositionChanged?.Invoke(this,EventArgs.Empty);
    }

    private NoteVM CreateNoteVM(Note model)
    {
      var result = new NoteVM{ Model = model };
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