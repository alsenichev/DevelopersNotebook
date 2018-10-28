using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.EventArguments;

namespace ViewModel.BottomPanelVMs
{
  public interface IBottomPanelVM
  {
    event EventHandler<TextInputCommandEventArgs> UserInputReceived;

    string TextInput { get; set; }

    void OnTextInputPreviewKeyDown(Key key);

    ObservableCollection<string> Captions { get; }
  }
}