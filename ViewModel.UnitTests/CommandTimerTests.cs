using System;
using Moq;
using NUnit.Framework;
using ViewModel.BottomPanelVMs;
using ViewModel.Enums;

namespace ViewModel.UnitTests
{
  [TestFixture]
  class CommandTimerTests
  {
    private readonly TimeSpan testTimeSpan= TimeSpan.FromMinutes(42);

    private CommandTimer CreateCommandTimer(ITimer timer = null)
    {
      return new CommandTimer(timer ?? new Mock<ITimer>().Object);
    }

    [Test]
    public void TestStartStopCommand_WhenTimerIsRunning_StopsTimer()
    {
      var innerTimerMock = new Mock<ITimer>();
      innerTimerMock.Setup(t => t.Elapsed).Returns(testTimeSpan);
      innerTimerMock.Setup(t => t.IsRunning).Returns(true);
      var commandTimer = CreateCommandTimer(innerTimerMock.Object);

      (NoteCommands command, TimeSpan elapsed) =
        commandTimer.HandleCommand(CommandsFromTextInput.StartStopTask);

      Assert.AreEqual(NoteCommands.StopTask, command);
      Assert.AreEqual(testTimeSpan, elapsed);
      innerTimerMock.Verify(t => t.Stop(), Times.Once);
      innerTimerMock.Verify(t => t.Start(), Times.Never);

    }

    [Test]
    public void TestStartStopCommand_WhenTimerIsStopped_StartsTimer()
    {
      var innerTimerMock = new Mock<ITimer>();
      innerTimerMock.Setup(t => t.IsRunning).Returns(false);
      var commandTimer = CreateCommandTimer(innerTimerMock.Object);

      (NoteCommands command, TimeSpan elapsed) =
        commandTimer.HandleCommand(CommandsFromTextInput.StartStopTask);

      Assert.AreEqual(NoteCommands.StartTask, command);
      Assert.AreEqual(TimeSpan.Zero, elapsed);
      innerTimerMock.Verify(t => t.Start(), Times.Once);
      innerTimerMock.Verify(t => t.Stop(), Times.Never);
    }

    [Test]
    public void TestPauseCommand_WhenTimerIsStarted_StopsTimer()
    {
      var innerTimerMock = new Mock<ITimer>();
      innerTimerMock.Setup(t => t.IsRunning).Returns(true);
      innerTimerMock.Setup(t => t.Elapsed).Returns(testTimeSpan);
      var commandTimer = CreateCommandTimer(innerTimerMock.Object);

      (NoteCommands command, TimeSpan elapsed) =
        commandTimer.HandleCommand(CommandsFromTextInput.PauseTask);

      Assert.AreEqual(NoteCommands.PauseTask, command);
      Assert.AreEqual(testTimeSpan, elapsed);
      innerTimerMock.Verify(t => t.Start(), Times.Never);
      innerTimerMock.Verify(t => t.Stop(), Times.Once);
    }

    [Test]
    public void TestPauseCommand_WhenTimerIsStopped_StartsTimer()
    {
      var innerTimerMock = new Mock<ITimer>();
      innerTimerMock.Setup(t => t.IsRunning).Returns(false);
      var commandTimer = CreateCommandTimer(innerTimerMock.Object);

      (NoteCommands command, TimeSpan elapsed) =
        commandTimer.HandleCommand(CommandsFromTextInput.PauseTask);

      Assert.AreEqual(NoteCommands.ResumeTask, command);
      Assert.AreEqual(TimeSpan.Zero, elapsed);
      // This is kind of a weak spot - at this point we don't know whether there is a paused
      // task, but we start the timer anyway.
      innerTimerMock.Verify(t => t.Start(), Times.Once);
      innerTimerMock.Verify(t => t.Stop(), Times.Never);
    }
  }
}