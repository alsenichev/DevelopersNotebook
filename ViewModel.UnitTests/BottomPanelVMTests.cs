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
      return new BottomPanelVM();
    }

    private void VerifyBottomPanelVM(
      IBottomPanelVM bottomPanelVM,
      string expectedText,
      TextInputCommand expectedCommand,
      TimeSpan expectedElapsed)
    {
      bool eventRaised = false;
      string text = null;
      TextInputCommand? textInputCommand = null;
      bottomPanelVM.UserInputReceived += (s, a) =>
      {
        eventRaised = true;
        textInputCommand = a.TextInputCommand;
        text = a.InputText;
      };
      bottomPanelVM.OnTextInputPreviewKeyDown(Key.Enter);

      Assert.IsTrue(eventRaised);
      Assert.AreEqual(expectedCommand, textInputCommand);
      Assert.AreEqual(expectedText, text);
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
      VerifyBottomPanelVM(bottomPanelVM, "Foo", TextInputCommand.StopTask, testTimeSpan);
    }

    [Test]
    public void TestSlashT_WhenTimerIsStopped_CreatesNewTask()
    {
      var innerTimerMock = new Mock<ITimer>();
      innerTimerMock.Setup(t => t.IsRunning).Returns(false);
      var bottomPanelVM = CreateBottomPanelVM(innerTimerMock.Object);
      bottomPanelVM.TextInput = @"/tFoo";

      VerifyBottomPanelVM(bottomPanelVM, "Foo", TextInputCommand.StartTask, TimeSpan.Zero);
    }

  }
}