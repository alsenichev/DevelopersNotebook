using System;
using CookbookMVVM;
using ViewModel.Enums;
using ViewModel.EventArgs;

namespace ViewModel.TotalCounterVMs
{
  public class TotalCounterVM : ViewModelBase, ITotalCounterVM
  {
    private TimeSpan counter;

    public TotalCounterVM()
    {
      counter = TimeSpan.Zero;
    }

    public string TotalTime => counter.ToString();

    public void HandleNoteCommand(object sender, NoteCommandEventArgs e)
    {
      switch (e.NoteCommand)
      {
        case NoteCommands.PauseTask:
        case NoteCommands.StopTask:
          counter += e.TimerElapsed;
          break;
        case NoteCommands.CreateNote:
        case NoteCommands.StartTask:
        case NoteCommands.PinNote:
        case NoteCommands.ResumeTask:
        case NoteCommands.ShutDown:
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(NoteCommands));
      }

      OnPropertyChanged(nameof(TotalTime));
    }
  }
}