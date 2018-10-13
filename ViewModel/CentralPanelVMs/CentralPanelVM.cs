using System;
using System.Collections.ObjectModel;
using System.Linq;
using CookbookMVVM;
using Domain.Enums;
using Domain.Interfaces;
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

    private ObservableCollection<NoteVM> notes;

    public CentralPanelVM(ITimeProvider timeProvider, INoteFactory noteFactory, IMainRepository mainRepository)
    {
      this.timeProvider = timeProvider;
      this.noteFactory = noteFactory;
      this.mainRepository = mainRepository;
    }

    public event EventHandler<System.EventArgs> StartTimerRequested;

    public event EventHandler<System.EventArgs> StopTimerRequested;

    public ObservableCollection<NoteVM> Notes => notes;

    public void LoadNotes()
    {
      notes = new ObservableCollection<NoteVM>(mainRepository.LoadNotes()
        .Select(n => new NoteVM { Model = n }));
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
      notes.Add(new NoteVM {Model = note});
      OnPropertyChanged(nameof(Notes));
      StartTimerRequested?.Invoke(this, System.EventArgs.Empty);
    }

    private void PauseTask(TimeSpan elapsedTimer)
    {
      var timePaused = timeProvider.Now;
      var noteVM = notes.Single(n => n.Model.State == NoteState.TimerRunning);
      var updatedNote = noteFactory.PausedTask(noteVM.Model, timePaused, elapsedTimer);
      var updatedVM = new NoteVM {Model = updatedNote};
      UpdateNoteVMInPlace(noteVM, updatedVM);
      StopTimerRequested?.Invoke(this, System.EventArgs.Empty);
    }

    private void ResumeTask()
    {
      var noteVM = notes.LastOrDefault(n => n.Model.State == NoteState.TimerPaused);
      if (noteVM == null)
      {
        return;
      }
      var updatedNote = noteFactory.ResumedTask(noteVM.Model);
      var updatedVM = new NoteVM {Model = updatedNote};
      UpdateNoteVMInPlace(noteVM, updatedVM);
      StartTimerRequested?.Invoke(this, System.EventArgs.Empty);
    }

    private void StopTask(TimeSpan elapsedTimer)
    {
      var timeStopped = timeProvider.Now;
      var noteVM = notes.Single(n => n.Model.State == NoteState.TimerRunning);
      var updatedNote = noteFactory.StoppedTask(noteVM.Model, timeStopped, elapsedTimer);
      var updatedVM = new NoteVM {Model = updatedNote};
      UpdateNoteVMInPlace(noteVM, updatedVM);
      StopTimerRequested?.Invoke(this, System.EventArgs.Empty);
    }

    private void UpdateNoteVMInPlace(NoteVM oldNote, NoteVM newNote)
    {
      int index = notes.IndexOf(oldNote);
      notes.RemoveAt(index);
      notes.Insert(index, newNote);
      OnPropertyChanged(nameof(Notes));
    }
  }
}