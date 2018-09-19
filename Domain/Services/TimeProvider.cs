using System;
using Domain.Interfaces;

namespace Domain.Services
{
  public class TimeProvider : ITimeProvider
  {
    public DateTime Now => GetNow();

    protected virtual DateTime GetNow()
    {
      return DateTime.Now;
    }

    public DateTime ThisFriday => Now.Add(TimeSpan.FromDays(FridayDiff(Now)));

    private int FridayDiff(DateTime today)
    {
      if (today.DayOfWeek == DayOfWeek.Saturday)
        return 6;
      else
        return DayOfWeek.Friday - today.DayOfWeek;
    }

    public DateTime Tomorrow => Now.Add(TimeSpan.FromDays(1));

    public DateTime NextMonday => Now.Add(TimeSpan.FromDays(MondayDiff(Now)));

    private int MondayDiff(DateTime today)
    {
      if (today.DayOfWeek == DayOfWeek.Sunday)
        return 1;
      else
        return 8 - (int) today.DayOfWeek;
    }
  }
}