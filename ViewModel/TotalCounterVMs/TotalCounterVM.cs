using System;
using CookbookMVVM;
using ViewModel.Enums;
using ViewModel.EventArgs;

namespace ViewModel.TotalCounterVMs
{
  public class TotalCounterVM : ViewModelBase, ITotalCounterVM
  {
    private TimeSpan counter;

    public void Add(TimeSpan timeSpan)
    {
      counter += timeSpan;
      OnPropertyChanged(nameof(TotalTime));
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
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(NoteCommands));
      }
      OnPropertyChanged(nameof(TotalTime));
    }

    public TotalCounterVM()
    {
      counter = TimeSpan.Zero;
    }
  }
}