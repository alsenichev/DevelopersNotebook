using System.Collections.ObjectModel;
using ViewModel.ModelsVMs;

namespace ViewModel.MainWindowVMs
{
  public interface ICentralPanelVM
  {
    ObservableCollection<NoteVM> Notes { get; }
  }
}