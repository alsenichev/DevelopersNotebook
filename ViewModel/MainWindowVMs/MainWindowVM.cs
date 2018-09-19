using CookbookMVVM;

namespace ViewModel.MainWindowVMs
{
  public class MainWindowVM : ViewModelBase
  {
    private readonly IUpperPanelVM upperPanelVM;

    public MainWindowVM(IUpperPanelVM upperPanelVM)
    {
      this.upperPanelVM = upperPanelVM;
    }

    public IUpperPanelVM UpperPanelVM => upperPanelVM;
  }
}