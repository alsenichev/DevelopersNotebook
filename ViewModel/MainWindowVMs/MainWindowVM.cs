using CookbookMVVM;

namespace ViewModel.MainWindowVMs
{
  public class MainWindowVM : ViewModelBase
  {
    private readonly IBottomPanelVM bottomPanelVM;
    private readonly ICentralPanelVM centralPanelVM;
    private readonly ITotalCounterVM totalCounterVM;

    public MainWindowVM(
      IBottomPanelVM bottomPanelVM,
      ICentralPanelVM centralPanelVM,
      ITotalCounterVM totalCounterVM)
    {
      this.bottomPanelVM = bottomPanelVM;
      this.centralPanelVM = centralPanelVM;
      this.totalCounterVM = totalCounterVM;
      this.bottomPanelVM.TimerStarted += this.centralPanelVM.AddTimerNote;
      this.bottomPanelVM.TimerStopped += this.centralPanelVM.EndTimerNote;
      this.bottomPanelVM.TimerStopped += (s, a) => this.totalCounterVM.Add(a);
    }

    public IBottomPanelVM BottomPanelVM => bottomPanelVM;

    public ICentralPanelVM CentralPanelVM => centralPanelVM;

    public ITotalCounterVM TotalCounterVM => totalCounterVM;
  }
}