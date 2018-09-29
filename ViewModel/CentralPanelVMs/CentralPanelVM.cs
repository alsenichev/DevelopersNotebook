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
    private readonly ObservableCollection<NoteVM> notes;
    private readonly ITimeProvider timeProvider;
    private readonly INoteFactory noteFactory;

    public ObservableCollection<NoteVM> Notes => notes;

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
          ResumeTask(e.InputText);
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
    }

    private void PauseTask(TimeSpan elapsedTimer)
    {
      var timePaused = timeProvider.Now;
      var noteVM = notes.Single(n => n.Model.State == NoteState.TimerRunning);
      var updatedNote =
        noteFactory.PauseTask(noteVM.Model, timePaused, elapsedTimer);
      var updatedVM = new NoteVM {Model = updatedNote};
      UpdateNoteVM(noteVM, updatedVM);
    }

    private void ResumeTask(string text)
    {
      var noteVM = notes.LastOrDefault(n => n.Model.State == NoteState.TimerPaused);
      if (noteVM == null)
      {
        // timer is started, command to resume the task,
        // but there was no paused task - ok, make a new one
        CreateNewTask(text);
        return;
      }
      var updatedNote = noteFactory.ResumeTask(noteVM.Model);
      var updatedVM = new NoteVM {Model = updatedNote};
      UpdateNoteVM(noteVM, updatedVM);
    }

    private void UpdateNoteVM(NoteVM oldNote, NoteVM newNote)
    {
      int index = notes.IndexOf(oldNote);
      notes.RemoveAt(index);
      notes.Insert(index, newNote);
      OnPropertyChanged(nameof(Notes));
    }

    public void StopTask(TimeSpan elapsedTimer)
    {
      var timeStopped = timeProvider.Now;
      var noteVM = notes.Single(n => n.Model.State == NoteState.TimerRunning);
      var updatedNote =
        noteFactory.StopTask(noteVM.Model, timeStopped, elapsedTimer);
      var updatedVM = new NoteVM {Model = updatedNote};
      UpdateNoteVM(noteVM, updatedVM);
    }

    public CentralPanelVM(ITimeProvider timeProvider, INoteFactory noteFactory)
    {
      this.timeProvider = timeProvider;
      this.noteFactory = noteFactory;
      notes = new ObservableCollection<NoteVM>();
    }
  }
}