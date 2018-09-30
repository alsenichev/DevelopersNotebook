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
      var (textInputCommand, userEnteredText) =
        TextInputParser.ParseText("/tNew Task");
      Assert.AreEqual(CommandsFromTextInput.StartStopTask, textInputCommand);
      Assert.AreEqual("New Task", userEnteredText);
    }
  }
}