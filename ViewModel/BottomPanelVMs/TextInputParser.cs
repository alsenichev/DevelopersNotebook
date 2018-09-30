using ViewModel.Enums;

namespace ViewModel.BottomPanelVMs
{
  /// <summary>
  /// Translates the text input into the command.
  /// </summary>
  public static class TextInputParser
  {
    public static (CommandsFromTextInput textInputCommand, string userEnteredText)
      ParseText(string input)
    {
      var trimmed = input.TrimStart();
      if (trimmed.StartsWith("/p"))
        return (CommandsFromTextInput.PauseTask,
          input.TrimStart().Remove(0, 2).Trim());
      else if (trimmed.StartsWith("/t"))
        return (CommandsFromTextInput.StartStopTask,
          input.TrimStart().Remove(0, 2).Trim());
      else if (trimmed.StartsWith("/pin"))
        return (CommandsFromTextInput.PinNote,
          input.TrimStart().Remove(0, 3).Trim());
      else
        return (CommandsFromTextInput.None, input.Trim());
    }
  }
}