using System;

namespace Domain.Services
{
  public class Timer
  {
    private TimeSpan elapsed;

    public void Reset()
    {
      elapsed = TimeSpan.Zero;
    }

    public void Increment(TimeSpan step)
    {
      elapsed += step;
    }

    public TimeSpan Elapsed => elapsed;

    public Timer()
    {
      elapsed = TimeSpan.Zero;
    }
  }
}