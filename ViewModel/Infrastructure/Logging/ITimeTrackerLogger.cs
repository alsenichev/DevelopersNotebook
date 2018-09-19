namespace ViewModel.Infrastructure.Logging
{
  /// <summary>
  /// Logs everything.
  /// </summary>
  public interface ITimeTrackerLogger
  {
    /// <summary>
    /// Logs information message.
    /// </summary>
    void Info(string message);

    /// <summary>
    /// Logs error message.
    /// </summary>
    void Error(string message);
  }
}