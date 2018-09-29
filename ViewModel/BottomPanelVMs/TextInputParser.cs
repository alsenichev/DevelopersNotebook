using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using ViewModel.Enums;

namespace ViewModel.BottomPanelVMs
{
  public static class TextInputParser
  {
    public static (CommandsFromTextInput noteCommand, string userEnteredText) ParseText(string input)
    {
      var trimmed = input.TrimStart();
      if (trimmed.StartsWith("/p"))
      {
        return (CommandsFromTextInput.PauseTask, input.TrimStart().Remove(0, 2).Trim());
      }
      else if (trimmed.StartsWith("/t"))
      {
        return (CommandsFromTextInput.StartStopTask, input.TrimStart().Remove(0, 2).Trim());
      }
      else if (trimmed.StartsWith("/pin"))
      {
        return (CommandsFromTextInput.PinNote, input.TrimStart().Remove(0, 3).Trim());
      }
      else
      {
        return (CommandsFromTextInput.None, input.Trim());
      }
    }
  }
}
