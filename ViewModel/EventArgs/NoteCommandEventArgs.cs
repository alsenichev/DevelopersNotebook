using System;
using ViewModel.Enums;

namespace ViewModel.EventArgs
{
  public class NoteCommandEventArgs : System.EventArgs
  {
    public NoteCommandEventArgs(
      NoteCommands noteCommand, string inputText, TimeSpan timerElapsed)
    {
      NoteCommand = noteCommand;
      InputText = inputText;
      TimerElapsed = timerElapsed;
    }

    public NoteCommands NoteCommand { get; }

    public string InputText { get; }

    public TimeSpan TimerElapsed { get; }
  }
}