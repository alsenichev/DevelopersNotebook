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
    public event EventHandler<EventArgs> ScrollDownRequested;

    private readonly IInputController inputController;

    public MainWindowVM(
      IBottomPanelVM bottomPanelVM,
      ICentralPanelVM centralPanelVM,
      ITotalCounterVM totalCounterVM,
      IInputController inputController)
    {
      BottomPanelVM = bottomPanelVM;
      CentralPanelVM = centralPanelVM;
      CentralPanelVM.NoteCommandReceived += RequestScrollDown;
      TotalCounterVM = totalCounterVM;
      this.inputController = inputController;
    }

    public IBottomPanelVM BottomPanelVM { get; }

    public ICentralPanelVM CentralPanelVM { get; }

    public ITotalCounterVM TotalCounterVM { get; }

    public void PrepareToShutdownApplication()
    {
      inputController.PrepareToShutdownApplication();
    }

    public void RequestScrollDown(object sender, EventArgs e)
    {
      ScrollDownRequested?.Invoke(this, EventArgs.Empty);
    }
  }
}