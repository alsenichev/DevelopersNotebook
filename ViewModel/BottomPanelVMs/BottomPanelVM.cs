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
    #region private fields
    private readonly CommandTimer commandTimer;
    #endregion

    #region events
    public event EventHandler<NoteCommandEventArgs> ActionRequested;
    #endregion

    #region private methods
    public void OnTextInputPreviewKeyDown(Key key)
    {
      if (key == Key.Enter)
      {
        var (textInputCommand, userEnteredText) =
          TextInputParser.ParseText(TextInput);
        var (noteCommand, timerElapsed) = commandTimer.HandleCommand(textInputCommand);
        TextInput = "";
        OnPropertyChanged(nameof(TextInput));
        OnPropertyChanged(nameof(TimerTime));
        ActionRequested?.Invoke(
          this,
          new NoteCommandEventArgs(noteCommand, userEnteredText, timerElapsed));
      }
    }

    #endregion

    #region public properties

    public string TimerTime => commandTimer.Elapsed.ToString();

    public string TextInput { get; set; }

    public ObservableCollection<string> Captions { get; } =
      new ObservableCollection<string>(new List<string> {"/t ", "/p"});

    #endregion

    #region public methods

    public BottomPanelVM(CommandTimer commandTimer)
    {
      this.commandTimer = commandTimer;
      commandTimer.TimeChanged += (s,a) => OnPropertyChanged(nameof(TimerTime));
    }

    #endregion
  }
}