using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.BusinessRules.Services
{
  public class DailyTimeCalculation
  {
    //TODO move to Settings, make customizable
    private const int offsetFromMidnight = 5;

    private readonly DateTime startOfTheDay;

    public DailyTimeCalculation(ITimeProvider timeProvider)
    {
      startOfTheDay = timeProvider.Now.Date.AddHours(offsetFromMidnight);
    }

    public TimeSpan CalculateTimeForToday(IList<Note> notes)
    {
      // If we work overnight and do not close the app, it will show us the
      // time from the moment the app started.
      // If we open the app, we will see the time for the tasks that have started
      // after the start of the working day.
      return notes
        .Where(IsTodaysTask)
        .Select(n => n.Duration)
        .Aggregate(TimeSpan.Zero, (d1, d2) => d1.Add(d2));
    }

    public bool IsTodaysTask(Note task)
    {
      return task.StartedAt > startOfTheDay;
    }

    public DateTime GetTaskDate(Note task)
    {
      return task.StartedAt.Hour > offsetFromMidnight
        ? task.StartedAt.Date
        : task.StartedAt.Date.AddDays(-1);
    }
  }
}