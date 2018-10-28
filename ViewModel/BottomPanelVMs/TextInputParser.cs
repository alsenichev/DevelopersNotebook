using ViewModel.Enums;

namespace ViewModel.BottomPanelVMs
{
  /// <summary>
  /// Translates the text input into the noteCommand.
  /// </summary>
  public static class TextInputParser
  {
    public static (TextInputCommand textInputCommand, string userEnteredText)
      ParseText(string input)
    {
      var trimmed = input.TrimStart();
      if (trimmed.StartsWith("/t"))
      {
        string content=trimmed.Remove(0, 2);
        if (string.IsNullOrWhiteSpace(content))
        {
          return (TextInputCommand.StopTask, "");
        }
        return (TextInputCommand.StartTask, content);
      }
      else if (trimmed.StartsWith("/pin"))
        return (TextInputCommand.PinNote,
          input.TrimStart().Remove(0, 4).Trim());
      else
        return (TextInputCommand.CreateNote, input.Trim());
    }
  }
}