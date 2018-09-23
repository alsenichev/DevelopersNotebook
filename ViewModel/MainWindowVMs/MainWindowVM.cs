using CookbookMVVM;

namespace ViewModel.MainWindowVMs
{
  public class MainWindowVM : ViewModelBase
  {
    private readonly IBottomPanelVM bottomPanelVM;
    private readonly ICentralPanelVM centralPanelVM;

    public MainWindowVM(IBottomPanelVM bottomPanelVM, ICentralPanelVM centralPanelVM)
    {
      this.bottomPanelVM = bottomPanelVM;
      this.centralPanelVM = centralPanelVM;
    }

    public IBottomPanelVM BottomPanelVM => bottomPanelVM;

    public ICentralPanelVM CentralPanelVM => centralPanelVM;
  }
}