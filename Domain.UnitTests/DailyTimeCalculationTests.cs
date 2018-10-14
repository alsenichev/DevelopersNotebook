using System;
using System.Collections.Generic;
using Domain.BusinessRules.Services;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using NUnit.Framework;

namespace Domain.UnitTests
{
  [TestFixture]
  public class DailyTimeCalculationTests
  {
    private DailyTimeCalculation CreateCalculation(ITimeProvider timeProvider)
    {
      return new DailyTimeCalculation(timeProvider ??
                                      new Mock<ITimeProvider>().Object);
    }

    [Test]
    public void
      CalculateTimeForTheDay_WithTasksLaterThanStartOfDay_SumsAllTasks()
    {
      var timeProviderMoq = new Mock<ITimeProvider>();
      timeProviderMoq.Setup(p => p.Now)
        .Returns(new DateTime(2018, 10, 15, 18, 15, 0));
      var calculation = CreateCalculation(timeProviderMoq.Object);
      var note1 = new Note("", "", NoteState.TimerStopped,
        new DateTime(2018, 10, 15, 5, 01, 0), TimeSpan.FromMinutes(1));
      var note2 = new Note("", "", NoteState.TimerStopped,
        new DateTime(2018, 10, 15, 10, 01, 0), TimeSpan.FromMinutes(1));
      var note3 = new Note("", "", NoteState.TimerStopped,
        new DateTime(2018, 10, 15, 15, 01, 0), TimeSpan.FromMinutes(1));
      var result =
        calculation.CalculateTimeForToday(new List<Note> {note1, note2, note3});
      Assert.AreEqual(TimeSpan.FromMinutes(3), result);
    }

    [Test]
    public void
      CalculateTimeForTheDay_WithSomeTasksEarlierThanStartOfDay_SumsTasksThatAreLater()
    {
      var timeProviderMoq = new Mock<ITimeProvider>();
      timeProviderMoq.Setup(p => p.Now)
        .Returns(new DateTime(2018, 10, 15, 18, 15, 0));
      var calculation = CreateCalculation(timeProviderMoq.Object);
      var note1 = new Note("", "", NoteState.TimerStopped,
        new DateTime(2018, 10, 15, 4, 01, 0), TimeSpan.FromMinutes(1));
      var note2 = new Note("", "", NoteState.TimerStopped,
        new DateTime(2018, 10, 15, 10, 01, 0), TimeSpan.FromMinutes(1));
      var note3 = new Note("", "", NoteState.TimerStopped,
        new DateTime(2018, 10, 15, 15, 01, 0), TimeSpan.FromMinutes(1));
      var note4 = new Note("", "", NoteState.TimerStopped,
        new DateTime(2018, 10, 12, 15, 01, 0), TimeSpan.FromMinutes(1));
      var result =
        calculation.CalculateTimeForToday(new List<Note> {note1, note2, note3, note4});
      Assert.AreEqual(TimeSpan.FromMinutes(2), result);
    }
  }
}