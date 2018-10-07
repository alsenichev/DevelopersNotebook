using System;
using System.Windows.Input;
using Moq;
using NUnit.Framework;
using ViewModel.BottomPanelVMs;
using ViewModel.Enums;

namespace ViewModel.UnitTests
{
  [TestFixture]
  class BottomPanelVMTests
  {
    private readonly TimeSpan testTimeSpan= TimeSpan.FromMinutes(42);

    private BottomPanelVM CreateBottomPanelVM(ITimer timer = null)
    {
      return new BottomPanelVM(timer ?? new Mock<ITimer>().Object);
    }

    private void VerifyBottomPanelVM(
      IBottomPanelVM bottomPanelVM,
      string expectedText,
      NoteCommands expectedCommand,
      TimeSpan expectedElapsed)
    {
      bool eventRaised = false;
      string text = null;
      TimeSpan? elapsed = null;
      NoteCommands? command = null;
      bottomPanelVM.ActionRequested += (s, a) =>
      {
        eventRaised = true;
        command = a.NoteCommand;
        text = a.InputText;
        elapsed = a.TimerElapsed;
      };
      bottomPanelVM.OnTextInputPreviewKeyDown(Key.Enter);

      Assert.IsTrue(eventRaised);
      Assert.AreEqual(expectedCommand, command);
      Assert.AreEqual(expectedText, text);
      Assert.AreEqual(expectedElapsed, elapsed);
    }

    [Test]
    public void TestSlashT_WhenTimerIsRunning_StopsTheTask()
    {
      var innerTimerMock = new Mock<ITimer>();
      innerTimerMock.Setup(t => t.Elapsed).Returns(testTimeSpan);
      innerTimerMock.Setup(t => t.IsRunning).Returns(true);
      var bottomPanelVM = CreateBottomPanelVM(innerTimerMock.Object);
      bottomPanelVM.TextInput = @"/tFoo";

      // the text will be left unused, it may be utilized in a comment
      VerifyBottomPanelVM(bottomPanelVM, "Foo", NoteCommands.StopTask, testTimeSpan);
    }

    [Test]
    public void TestSlashT_WhenTimerIsStopped_CreatesNewTask()
    {
      var innerTimerMock = new Mock<ITimer>();
      innerTimerMock.Setup(t => t.IsRunning).Returns(false);
      var bottomPanelVM = CreateBottomPanelVM(innerTimerMock.Object);
      bottomPanelVM.TextInput = @"/tFoo";

      VerifyBottomPanelVM(bottomPanelVM, "Foo", NoteCommands.StartTask, TimeSpan.Zero);
    }

    [Test]
    public void TestSlashP_WhenTimerIsStarted_PausesTask()
    {
      var innerTimerMock = new Mock<ITimer>();
      innerTimerMock.Setup(t => t.IsRunning).Returns(true);
      innerTimerMock.Setup(t => t.Elapsed).Returns(testTimeSpan);
      var bottomPanelVM = CreateBottomPanelVM(innerTimerMock.Object);
      bottomPanelVM.TextInput = @"/pFoo";

      // text will be left unused
      VerifyBottomPanelVM(bottomPanelVM, "Foo", NoteCommands.PauseTask,testTimeSpan);
    }

    [Test]
    public void TestPauseCommand_WhenTimerIsStopped_StartsTimer()
    {
      var innerTimerMock = new Mock<ITimer>();
      innerTimerMock.Setup(t => t.IsRunning).Returns(false);
      var bottomPanelVM = CreateBottomPanelVM(innerTimerMock.Object);
      bottomPanelVM.TextInput = "/pFoo";

      VerifyBottomPanelVM(bottomPanelVM, "Foo", NoteCommands.ResumeTask, TimeSpan.Zero);
    }
  }
}