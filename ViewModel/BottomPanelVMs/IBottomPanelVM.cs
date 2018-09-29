using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.EventArgs;

namespace ViewModel.BottomPanelVMs
{
  public interface IBottomPanelVM
  {
    string TimerTime { get; }

    string TextInput { get; set; }

    event EventHandler<NoteCommandEventArgs> ActionRequested;

    void OnTextInputPreviewKeyDown(Key key);

    ObservableCollection<string> Captions { get; }
  }
}