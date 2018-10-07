using System;
using NUnit.Framework;
using ViewModel.Enums;
using ViewModel.EventArgs;
using ViewModel.TotalCounterVMs;

namespace ViewModel.UnitTests
{
  [TestFixture]
  public class TotalCounterVMTests
  {
    private ITotalCounterVM CreateTotalCounterVM()
    {
      return new TotalCounterVM();
    }

    [Test]
    public void TotalTime_AfterNoteIsCreated_IsNotChanged()
    {
      var counter = CreateTotalCounterVM();
      var args = new NoteCommandEventArgs(NoteCommands.CreateNote, "",
        TimeSpan.FromMinutes(1));
      counter.HandleNoteCommand(null, args);
      Assert.AreEqual("00:00:00", counter.TotalTime);
    }

    [Test]
    public void TotalTime_AfterTaskIsCreated_IsNotChanged()
    {
      var counter = CreateTotalCounterVM();
      var args = new NoteCommandEventArgs(NoteCommands.StartTask, "",
        TimeSpan.FromMinutes(1));
      counter.HandleNoteCommand(null, args);
      Assert.AreEqual("00:00:00", counter.TotalTime);
    }

    [Test]
    public void TotalTime_AfterTaskIsPinned_IsNotChanged()
    {
      var counter = CreateTotalCounterVM();
      var args = new NoteCommandEventArgs(NoteCommands.PinNote, "",
        TimeSpan.FromMinutes(1));
      counter.HandleNoteCommand(null, args);
      Assert.AreEqual("00:00:00", counter.TotalTime);
    }

    [Test]
    public void TotalTime_AfterTaskIsResumed_IsNotChanged()
    {
      var counter = CreateTotalCounterVM();
      var args = new NoteCommandEventArgs(NoteCommands.ResumeTask, "",
        TimeSpan.FromMinutes(1));
      counter.HandleNoteCommand(null, args);
      Assert.AreEqual("00:00:00", counter.TotalTime);
    }

    [Test]
    public void TotalTime_AfterTaskIsStopped_IsChanged()
    {
      var counter = CreateTotalCounterVM();
      var args = new NoteCommandEventArgs(NoteCommands.StopTask, "",
        TimeSpan.FromMinutes(1));
      counter.HandleNoteCommand(null, args);
      Assert.AreEqual("00:01:00", counter.TotalTime);
    }

    [Test]
    public void TotalTime_AfterTaskIsPaused_IsChanged()
    {
      var counter = CreateTotalCounterVM();
      var args = new NoteCommandEventArgs(NoteCommands.PauseTask, "",
        TimeSpan.FromMinutes(1));
      counter.HandleNoteCommand(null, args);
      Assert.AreEqual("00:01:00", counter.TotalTime);
    }

    [Test]
    public void TotalTime_AfterDifferentActions_IsChangedCorrectly()
    {
      var counter = CreateTotalCounterVM();
      var args = new NoteCommandEventArgs(NoteCommands.StopTask, "", TimeSpan.FromMinutes(1));
      counter.HandleNoteCommand(null, args);
      var args2 = new NoteCommandEventArgs(NoteCommands.PauseTask, "", TimeSpan.FromMinutes(1));
      counter.HandleNoteCommand(null, args2);
      var args3 = new NoteCommandEventArgs(NoteCommands.StopTask, "", TimeSpan.FromMinutes(1));
      counter.HandleNoteCommand(null, args3);
      Assert.AreEqual("00:03:00", counter.TotalTime);
    }
  }
}
