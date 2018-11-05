using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.BottomPanelVMs;
using ViewModel.CentralPanelVMs;
using ViewModel.TotalCounterVMs;

namespace ViewModel.MainWindowVMs
{
  public interface IMainWindowVM
  {
    event EventHandler<EventArgs> ScrollDownRequested;

    IBottomPanelVM BottomPanelVM { get; }

    ICentralPanelVM CentralPanelVM { get; }

    ITotalCounterVM TotalCounterVM { get; }

    void PrepareToShutdownApplication();

    void RequestScrollDown(object sender, EventArgs e);
  }
}
