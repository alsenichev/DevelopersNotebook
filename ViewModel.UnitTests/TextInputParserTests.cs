using NUnit.Framework;
using ViewModel.BottomPanelVMs;
using ViewModel.Enums;

namespace ViewModel.UnitTests
{
  [TestFixture]
  public class TextInputParserTests
  {
    [Test]
    public void SlashTAndTaskName_AreParsedCorrectly()
    {
      var (command, text) =
        TextInputParser.ParseText("/tNew Task");
      Assert.AreEqual(CommandsFromTextInput.StartStopTask, command);
      Assert.AreEqual("New Task", text);
    }

    [Test]
    public void SlashPAndText_AreParsedCorrectly()
    {
      var (command, text) =
        TextInputParser.ParseText("/pSome text");
      Assert.AreEqual(CommandsFromTextInput.PauseTask, command);
      // Currently I don't know how to use the text during pausing a task
      // may be it could be used as a comment
      Assert.AreEqual("Some text", text);
    }
  }
}