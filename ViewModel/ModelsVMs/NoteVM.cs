using CookbookMVVM;
using Domain.Models;

namespace ViewModel.ModelsVMs
{
  public class NoteVM : ViewModelBase<Note>
  {

    public string Header => Model.Header;
    public string Text => Model.Text;
  }
}