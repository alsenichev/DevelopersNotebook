using System;
using CookbookMVVM;
using ViewModel.BottomPanelVMs;
using ViewModel.CentralPanelVMs;
using ViewModel.TotalCounterVMs;

namespace ViewModel.MainWindowVMs
{
  public class MainWindowVM : ViewModelBase
  {

    public event EventHandler<System.EventArgs> ScrollDownRequested
    {
      add => centralPanelVM.ItemsPositionChanged += value;
      remove => centralPanelVM.ItemsPositionChanged -= value;
    }

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
      this.bottomPanelVM.ActionRequested += this.centralPanelVM.HandleNoteCommand;
      this.bottomPanelVM.ActionRequested += this.totalCounterVM.HandleNoteCommand;
      this.centralPanelVM.StartTimerRequested += (s, a) => this.bottomPanelVM.StartTimer();
      this.centralPanelVM.StopTimerRequested += (s, a) => this.bottomPanelVM.StopTimer();
      this.centralPanelVM.StopTimerRequested += (s, a) => this.totalCounterVM.UpdateCounter(a);
    }

    public IBottomPanelVM BottomPanelVM => bottomPanelVM;

    public ICentralPanelVM CentralPanelVM => centralPanelVM;

    public ITotalCounterVM TotalCounterVM => totalCounterVM;
  }
}