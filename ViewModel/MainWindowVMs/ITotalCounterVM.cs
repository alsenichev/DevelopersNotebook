using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.MainWindowVMs
{
  public interface ITotalCounterVM
  {
    void Add(TimeSpan timeSpan);
    string TotalTime { get; }
  }
}
