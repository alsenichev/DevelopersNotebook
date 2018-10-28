using System;
using CookbookMVVM;
using ViewModel.BottomPanelVMs;
using ViewModel.CentralPanelVMs;
using ViewModel.InputControllers;
using ViewModel.TotalCounterVMs;

namespace ViewModel.MainWindowVMs
{
  public class MainWindowVM : ViewModelBase
  {

    public event EventHandler<System.EventArgs> ScrollDownRequested
    {
      add => CentralPanelVM.ItemsPositionChanged += value;
      remove => CentralPanelVM.ItemsPositionChanged -= value;
    }

    private readonly IInputController inputController;

    public MainWindowVM(
      IBottomPanelVM bottomPanelVM,
      ICentralPanelVM centralPanelVM,
      ITotalCounterVM totalCounterVM,
      IInputController inputController)
    {
      this.BottomPanelVM = bottomPanelVM;
      this.CentralPanelVM = centralPanelVM;
      this.TotalCounterVM = totalCounterVM;
      this.inputController = inputController;
    }

    public IBottomPanelVM BottomPanelVM { get; }

    public ICentralPanelVM CentralPanelVM { get; }

    public ITotalCounterVM TotalCounterVM { get; }

    public void PrepareToShutdownApplication()
    {
      inputController.PrepareToShutdownApplication();
    }
  }
}