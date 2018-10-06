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
      (NoteCommands noteCommand, TimeSpan timerElapsed) =
        commandTimer.HandleCommand(CommandsFromTextInput.StartStopTask);
      Assert.AreEqual(NoteCommands.StopTask, noteCommand);
      Assert.AreEqual(testTimeSpan, timerElapsed);
      innerTimerMock.Verify(t => t.Stop(), Times.Once);
    }
  }
}