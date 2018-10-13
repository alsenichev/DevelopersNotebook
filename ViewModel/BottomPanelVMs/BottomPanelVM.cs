using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CookbookMVVM;
using ViewModel.Enums;
using ViewModel.EventArgs;

namespace ViewModel.BottomPanelVMs
{
  public class BottomPanelVM : ViewModelBase, IBottomPanelVM
  {
    private readonly ITimer timer;

    public event EventHandler<NoteCommandEventArgs> ActionRequested;

    public BottomPanelVM(ITimer timer)
    {
      this.timer = timer;
      timer.TimeChanged +=
        (s, a) => OnPropertyChanged(nameof(TimerTime));
    }

    public string TimerTime => timer.Elapsed.ToString();

    public string TextInput { get; set; }

    public ObservableCollection<string> Captions { get; } =
      new ObservableCollection<string>(new List<string> {"/t ", "/p"});

    public void StartTimer()
    {
      timer.Start();
    }

    public void StopTimer()
    {
      timer.Stop();
      timer.Reset();
      OnPropertyChanged(nameof(TimerTime));
    }

    public void Shutdown()
    {
      ActionRequested?.Invoke(
        this,
        new NoteCommandEventArgs(NoteCommands.ShutDown, "", timer.Elapsed));
    }

    public void OnTextInputPreviewKeyDown(Key key)
    {
      if (key != Key.Enter)
      {
        return;
      }

      var (textInputCommand, userEnteredText) = TextInputParser.ParseText(TextInput);
      var (noteCommand, timerElapsed) = InterpretCommand(textInputCommand);
      TextInput = "";
      OnPropertyChanged(nameof(TextInput));
      OnPropertyChanged(nameof(TimerTime));
      ActionRequested?.Invoke(
        this,
        new NoteCommandEventArgs(noteCommand, userEnteredText, timerElapsed));
    }

    private (NoteCommands noteCommand, TimeSpan timerElapsed) InterpretCommand(
      CommandsFromTextInput inputCommand)
    {
      switch (inputCommand)
      {
        case CommandsFromTextInput.None:
          return (NoteCommands.CreateNote, timer.Elapsed);
        case CommandsFromTextInput.StartStopTask:
          if (timer.IsRunning)
          {
            return (NoteCommands.StopTask, timer.Elapsed);
          }
          return (NoteCommands.StartTask, TimeSpan.Zero);
        case CommandsFromTextInput.PauseTask:
          if (timer.IsRunning)
          {
            return (NoteCommands.PauseTask, timer.Elapsed);
          }
          return (NoteCommands.ResumeTask, TimeSpan.Zero);
        case CommandsFromTextInput.PinNote:
          return (NoteCommands.PinNote, TimeSpan.Zero);
        default:
          throw new ArgumentOutOfRangeException(nameof(inputCommand),
            inputCommand, null);
      }
    }
  }
}