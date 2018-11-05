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
    private DailyTimeCalculation CreateCalculation(ITimeProvider timeProvider = null)
    {
      if (timeProvider == null)
      {
        var timeProviderMoq = new Mock<ITimeProvider>();
        timeProviderMoq.Setup(p => p.Now)
          .Returns(new DateTime(2018, 10, 15, 18, 15, 0));
        timeProvider = timeProviderMoq.Object;
      }
      return new DailyTimeCalculation(timeProvider);
    }

    [Test]
    public void CalculateTimeForTheDay_WithTasksLaterThanStartOfDay_SumsAllTasks()
    {
      var calculation = CreateCalculation();
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
    public void CalculateTimeForTheDay_WithSomeTasksEarlierThanStartOfDay_SumsTasksThatAreLater()
    {
      var calculation = CreateCalculation();
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

    [Test]
    public void IsTodaysTask_WhenStartedEarlierThanStartOfDay_ReturnsFalse()
    {
      var started = new DateTime(2018, 10, 15, 4, 58, 0);
      var note = new Note("", "", NoteState.Unknown, started, TimeSpan.Zero);
      var calculation = CreateCalculation();
      Assert.IsFalse(calculation.IsTodaysTask(note));
    }

    [Test]
    public void IsTodaysTask_WhenStartedLaterThanStartOfDay_ReturnsTrue()
    {
      var started = new DateTime(2018, 10, 15, 5, 5, 0);
      var note = new Note("", "", NoteState.Unknown, started, TimeSpan.Zero);
      var calculation = CreateCalculation();
      Assert.IsTrue(calculation.IsTodaysTask(note));
    }

    [Test]
    public void IsTodaysTask_WhenStartedAtTheStartOfDay_ReturnsTrue()
    {
      var started = new DateTime(2018, 10, 15, 5, 0, 0);
      var note = new Note("", "", NoteState.Unknown, started, TimeSpan.Zero);
      var calculation = CreateCalculation();
      Assert.IsTrue(calculation.IsTodaysTask(note));
    }

    [Test]
    public void GetTaskDate_WhenStartedBeforeThanStartOfDay_ReturnsYesterday()
    {
      var started = new DateTime(2018, 10, 15, 4, 59, 0);
      var note = new Note("", "", NoteState.Unknown, started, TimeSpan.Zero);
      var calculation = CreateCalculation();
      Assert.AreEqual(new DateTime(2018, 10, 14),calculation.GetTaskDate(note)); 
    }

    [Test]
    public void GetTaskDate_WhenStartedAfterThanStartOfDay_ReturnsToday()
    {
      var started = new DateTime(2018, 10, 15, 5, 59, 0);
      var note = new Note("", "", NoteState.Unknown, started, TimeSpan.Zero);
      var calculation = CreateCalculation();
      Assert.AreEqual(new DateTime(2018, 10, 15),calculation.GetTaskDate(note)); 
    }

    [Test]
    public void GetTaskDate_WhenStartedAtStartOfDay_ReturnsToday()
    {
      var started = new DateTime(2018, 10, 15, 5, 0, 0);
      var note = new Note("", "", NoteState.Unknown, started, TimeSpan.Zero);
      var calculation = CreateCalculation();
      Assert.AreEqual(new DateTime(2018, 10, 15), calculation.GetTaskDate(note)); 
    }
  }
}