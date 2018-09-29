using ViewModel.EventArgs;

namespace ViewModel.TotalCounterVMs
{
  public interface ITotalCounterVM
  {
    string TotalTime { get; }

    void HandleNoteCommand(object sender, NoteCommandEventArgs e);
  }
}