using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CookbookMVVM;
using ViewModel.EventArgs;

namespace ViewModel.BottomPanelVMs
{
  public class BottomPanelVM : ViewModelBase, IBottomPanelVM
  {
    private readonly CommandTimer commandTimer;

    public event EventHandler<NoteCommandEventArgs> ActionRequested;

    public BottomPanelVM(CommandTimer commandTimer)
    {
      this.commandTimer = commandTimer;
      commandTimer.TimeChanged +=
        (s, a) => OnPropertyChanged(nameof(TimerTime));
    }

    public string TimerTime => commandTimer.Elapsed.ToString();

    public string TextInput { get; set; }

    public ObservableCollection<string> Captions { get; } =
      new ObservableCollection<string>(new List<string> {"/t ", "/p"});

    public void OnTextInputPreviewKeyDown(Key key)
    {
      if (key != Key.Enter)
        return;
      var (textInputCommand, userEnteredText) =
        TextInputParser.ParseText(TextInput);
      var (noteCommand, timerElapsed) =
        commandTimer.HandleCommand(textInputCommand);
      TextInput = "";
      OnPropertyChanged(nameof(TextInput));
      OnPropertyChanged(nameof(TimerTime));
      ActionRequested?.Invoke(
        this,
        new NoteCommandEventArgs(noteCommand, userEnteredText, timerElapsed));
    }
  }
}