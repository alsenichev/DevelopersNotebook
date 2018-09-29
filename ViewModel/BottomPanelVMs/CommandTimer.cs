using System;
using ViewModel.Enums;

namespace ViewModel.BottomPanelVMs
{
  /// <summary>
  /// Interprets the
  /// </summary>
  public class CommandTimer
  {
    private readonly Timer timer;

    public event EventHandler<System.EventArgs> TimeChanged
    {
      add => timer.TimeChanged += value;
      remove => timer.TimeChanged -= value;
    }

    public CommandTimer(Timer timer)
    {
      this.timer = timer;
    }

    public TimeSpan Elapsed => timer.Elapsed;

    public (NoteCommands noteCommand, TimeSpan timerElapsed)
      HandleCommand(CommandsFromTextInput inputCommand)
    {
      switch (inputCommand)
      {
        case CommandsFromTextInput.None:
          return (NoteCommands.CreateNote, timer.Elapsed);
        case CommandsFromTextInput.StartStopTask:
          if (timer.IsRunning)
          {
            timer.Stop();
            var elapsed = timer.Elapsed;
            timer.Reset();
            return (NoteCommands.StopTask, elapsed);
          }
          else
          {
            // Todo - if there is no paused task, there will be nothing to resume,
            // but the timer is already started. So now we start a new task, but it would be better
            // to ignore the command.
            // so we need to poll the CentralPanelVM for the presence of paused tasks
            timer.Start();
            return (NoteCommands.StartTask, TimeSpan.Zero);
          }
        case CommandsFromTextInput.PauseTask:
          if (timer.IsRunning)
          {
            timer.Stop();
            var elapsed = timer.Elapsed;
            timer.Reset();
            return (NoteCommands.PauseTask, elapsed);
          }
          timer.Start();
          return (NoteCommands.ResumeTask, TimeSpan.Zero);
        case CommandsFromTextInput.PinNote:
          return (NoteCommands.PinNote, timer.Elapsed);
        default:
          throw new ArgumentOutOfRangeException(nameof(inputCommand),
            inputCommand, null);
      }
    }
  }
}