using System.Collections.ObjectModel;
using CookbookMVVM;
using ViewModel.ModelsVMs;

namespace ViewModel.MainWindowVMs
{
  public class CentralPanelVM : ViewModelBase, ICentralPanelVM
  {
    public ObservableCollection<NoteVM> Notes { get; }

  }
}