using System;
using System.Windows.Threading;

namespace ViewModel.BottomPanelVMs
{
  /// <summary>
  /// The timer of an application.
  /// </summary>
  public class Timer : ITimer
  {
    public event EventHandler<System.EventArgs> TimeChanged;

    private readonly DispatcherTimer dispatcherTimer;
    private readonly TimeSpan timerStep = TimeSpan.FromSeconds(1);

    public Timer()
    {
      Elapsed = TimeSpan.Zero;
      dispatcherTimer = new DispatcherTimer
        {Interval = timerStep};
      dispatcherTimer.Tick += DispatcherTimerOnTick;
    }

    private void DispatcherTimerOnTick(
      object sender, System.EventArgs eventArgs)
    {
      Increment(timerStep);
      TimeChanged?.Invoke(this, System.EventArgs.Empty);
    }

    private void Increment(TimeSpan step)
    {
      Elapsed += step;
    }

    public TimeSpan Elapsed { get; private set; }

    public bool IsRunning => dispatcherTimer.IsEnabled;

    public void Start()
    {
      dispatcherTimer.Start();
    }

    public void Stop()
    {
      dispatcherTimer.Stop();
    }

    public void Reset()
    {
      Elapsed = TimeSpan.Zero;
    }
  }
}