using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CookbookMVVM;
using ViewModel.EventArguments;

namespace ViewModel.BottomPanelVMs
{
  public class BottomPanelVM : ViewModelBase, IBottomPanelVM
  {
    public event EventHandler<TextInputCommandEventArgs> UserInputReceived;

    public string TextInput { get; set; }

    public ObservableCollection<string> Captions { get; } =
      new ObservableCollection<string>(new List<string> {"/t ", "/pin"});

    public void OnTextInputPreviewKeyDown(Key key)
    {
      if (key != Key.Enter)
      {
        return;
      }

      var (textInputCommand, userEnteredText) = TextInputParser.ParseText(TextInput);
      TextInput = "";
      OnPropertyChanged(nameof(TextInput));
      UserInputReceived?.Invoke(
        this,
        new TextInputCommandEventArgs(textInputCommand, userEnteredText));
    }
  }
}