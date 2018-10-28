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
using ViewModel.Enums;
using ViewModel.EventArgs;
using ViewModel.ModelsVMs;

namespace ViewModel.CentralPanelVMs
{
  public class CentralPanelVM : ViewModelBase, ICentralPanelVM
  {
    private readonly ITimeProvider timeProvider;
    private readonly INoteFactory noteFactory;
    private readonly IMainRepository mainRepository;
    private readonly ITimer timer;
    private readonly DailyTimeCalculation dailyTimeCalculation;

    private ObservableCollection<NoteVM> notes;
    private NoteVM runningNoteVM;

    public CentralPanelVM(ITimeProvider timeProvider, INoteFactory noteFactory, IMainRepository mainRepository, ITimer timer, DailyTimeCalculation dailyTimeCalculation)
    {
      this.timeProvider = timeProvider;
      this.noteFactory = noteFactory;
      this.mainRepository = mainRepository;
      this.timer = timer;
      this.dailyTimeCalculation = dailyTimeCalculation;
      timer.TimeChanged += OnTimerTimeChanged;
    }

    public event EventHandler<System.EventArgs> StartTimerRequested;

    public event EventHandler<TimeSpan> StopTimerRequested;

    public ObservableCollection<NoteVM> Notes => notes;

    public void InitializeNotes(IList<Note> source)
    {
      notes = new ObservableCollection<NoteVM>(source.Select(n => CreateNoteVM(n)));
      OnPropertyChanged(nameof(Notes));
    }

    public void HandleNoteCommand(object sender, NoteCommandEventArgs e)
    {
      switch (e.NoteCommand)
      {
        case NoteCommands.CreateNote:
          CreateNote(e.InputText);
          break;
        case NoteCommands.StartTask:
          CreateNewTask(e.InputText);
          break;
        case NoteCommands.PauseTask:
          PauseTask(e.TimerElapsed);
          break;
        case NoteCommands.ResumeTask:
          ResumeTask();
          break;
        case NoteCommands.StopTask:
          StopTask(e.TimerElapsed);
          break;
        case NoteCommands.ShutDown:
          Shutdown(e.TimerElapsed);
          break;
        case NoteCommands.PinNote:
          // TODO
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(NoteCommands));
      }
      mainRepository.SaveNotes(notes.Select(n => n.Model));
    }

    private void CreateNote(string text)
    {
      var note = noteFactory.CreateNote(text);
      notes.Add(new NoteVM {Model = note});
      OnPropertyChanged(nameof(Notes));
    }

    private void CreateNewTask(string text)
    {
      var note = noteFactory.CreateTask(text, timeProvider.Now);
      var noteVM = CreateNoteVM(note);
      runningNoteVM = noteVM;
      notes.Add(CreateNoteVM(note));
      OnPropertyChanged(nameof(Notes));
      StartTimerRequested?.Invoke(this, System.EventArgs.Empty);
    }

    private void PauseTask(TimeSpan elapsedTimer)
    {
      var timePaused = timeProvider.Now;
      var noteVM = notes.Single(n => n.Model.State == NoteState.TimerRunning);
      var updatedNote = noteFactory.PausedTask(noteVM.Model, timePaused, elapsedTimer);
      var updatedVM = CreateNoteVM(updatedNote);
      runningNoteVM = null;
      UpdateNoteVMInPlace(noteVM, updatedVM);
      StopTimerRequested?.Invoke(this, timer.Elapsed);
    }

    private void ResumeTask()
    {
      var noteVM = notes.LastOrDefault(n => n.Model.State == NoteState.TimerPaused);
      if (noteVM == null)
      {
        return;
      }
      var updatedNote = noteFactory.ResumedTask(noteVM.Model);
      var updatedVM = CreateNoteVM(updatedNote);
      runningNoteVM = updatedVM;
      UpdateNoteVMInPlace(noteVM, updatedVM);
      StartTimerRequested?.Invoke(this, System.EventArgs.Empty);
    }

    private void ResumeTask(NoteVM noteVM)
    {
      var updatedNote = noteFactory.ResumedTask(noteVM.Model);
      var updatedVM = CreateNoteVM(updatedNote);
      runningNoteVM = updatedVM;
      UpdateNoteVMInPlace(noteVM, updatedVM);
      StartTimerRequested?.Invoke(this, System.EventArgs.Empty);
    }

    private void StopTask(TimeSpan elapsedTimer)
    {
      var timeStopped = timeProvider.Now;
      var noteVM = notes.Single(n => n.Model.State == NoteState.TimerRunning);
      var updatedNote = noteFactory.StoppedTask(noteVM.Model, timeStopped, elapsedTimer);
      var updatedVM = CreateNoteVM(updatedNote);
      runningNoteVM = null;
      UpdateNoteVMInPlace(noteVM, updatedVM);
      StopTimerRequested?.Invoke(this, timer.Elapsed);
    }

    private void Shutdown(TimeSpan elapsedTimer)
    {
      var timeStopped = timeProvider.Now;
      var noteVM = notes.SingleOrDefault(n => n.Model.State == NoteState.TimerRunning);
      if (noteVM != null)
      {
        var updatedNote = noteFactory.StoppedTask(noteVM.Model, timeStopped, elapsedTimer);
        var updatedVM = new NoteVM { Model = updatedNote };
        runningNoteVM = null;
        UpdateNoteVMInPlace(noteVM, updatedVM);
      }
      mainRepository.SaveNotes(notes.Select(n => n.Model));
    }

    private void UpdateNoteVMInPlace(NoteVM oldNote, NoteVM newNote)
    {
      int index = notes.IndexOf(oldNote);
      notes.RemoveAt(index);
      oldNote.ToggleRunningStateRequested -= OnNoteToggleRunningStateRequested;
      notes.Insert(index, newNote);
      OnPropertyChanged(nameof(Notes));
    }

    private NoteVM CreateNoteVM(Note model)
    {
      var result = new NoteVM{Model=model};
      result.ToggleRunningStateRequested += OnNoteToggleRunningStateRequested;
      return result;
    }

    private void OnTimerTimeChanged(object sender, System.EventArgs e)
    {
      if (runningNoteVM != null)
      {
        runningNoteVM.UpdateDuration(timer.Elapsed);
      }
    }

    private void OnNoteToggleRunningStateRequested(object sender, System.EventArgs e)
    {
      var taskVM = (NoteVM) sender;
      if (timer.IsRunning)
      {
        StopTask(timer.Elapsed);
      }
      else if(dailyTimeCalculation.IsTodaysTask(taskVM.Model))
      {
        ResumeTask((NoteVM) sender);
      }
      else
      {
        CreateNewTask(taskVM.Header);
      }
    }
  }
}