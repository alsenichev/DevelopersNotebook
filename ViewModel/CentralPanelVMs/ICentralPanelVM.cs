using System.Collections.ObjectModel;
using ViewModel.EventArgs;
using ViewModel.ModelsVMs;

namespace ViewModel.CentralPanelVMs
{
  public interface ICentralPanelVM
  {
    ObservableCollection<NoteVM> Notes { get; }

    void HandleNoteCommand(object sender, NoteCommandEventArgs e);
  }
}