using System;
using System.Windows.Threading;

namespace ViewModel.BottomPanelVMs
{
  public class Timer
  {
    private TimeSpan elapsed;
    private readonly DispatcherTimer dispatcherTimer;
    private readonly TimeSpan timerStep = TimeSpan.FromSeconds(1);

    public event EventHandler<System.EventArgs> TimeChanged;

    private void DispatcherTimerOnTick(
      object sender, System.EventArgs eventArgs)
    {
      Increment(timerStep);
      TimeChanged?.Invoke(this, System.EventArgs.Empty);
    }

    void Increment(TimeSpan step)
    {
      elapsed += step;
    }

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
      elapsed = TimeSpan.Zero;
    }

    public TimeSpan Elapsed => elapsed;

    public bool IsRunning => dispatcherTimer.IsEnabled;

    public Timer()
    {
      elapsed = TimeSpan.Zero;
      dispatcherTimer = new DispatcherTimer
        {Interval = timerStep};
      dispatcherTimer.Tick += DispatcherTimerOnTick;
    }
  }
}