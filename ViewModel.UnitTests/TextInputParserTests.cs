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
      Assert.AreEqual(TextInputCommand.StartTask, command);
      Assert.AreEqual("New Task", text);
    }

    [Test]
    public void SlashTAndNoText_AreParsedCorrectly()
    {
      var (command, text) =
        TextInputParser.ParseText("/t  ");
      Assert.AreEqual(TextInputCommand.StopTask, command);
      Assert.AreEqual("", text);
    }

    [Test]
    public void SlashPinAndText_AreParsedCorrectly()
    {
      var (command, text) =
        TextInputParser.ParseText("/pinSome text");
      Assert.AreEqual(TextInputCommand.PinNote, command);
      Assert.AreEqual("Some text", text);
    }

    [Test]
    public void NoSpecialSymbols_IsParsedCorrectly()
    {
      var (command, text) =
        TextInputParser.ParseText("  The text  ");
      Assert.AreEqual(TextInputCommand.CreateNote, command);
      Assert.AreEqual("The text", text);
    }
  }
}