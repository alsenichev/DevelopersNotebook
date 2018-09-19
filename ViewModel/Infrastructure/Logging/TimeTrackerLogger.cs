using System.Reflection;
using log4net;

namespace ViewModel.Infrastructure.Logging
{
  public class TimeTrackerLogger : ITimeTrackerLogger
  {
    private static readonly ILog log =
      LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


    public void Info(string message)
    {
      log.Info(message);
    }

    public void Error(string message)
    {
      log.Error(message);
    }
  }
}