using System.Windows.Input;
using CsUtil;

namespace ViewModel.MainWindowVMs
{
  public interface IBottomPanelVM
  {
    string Timer { get; }

    ICommand ToggleTimer { get; }

    bool IsTimerRunning { get; }
  }
}