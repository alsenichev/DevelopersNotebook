using ViewModel.Enums;

namespace ViewModel.EventArguments
{
  public class TextInputCommandEventArgs : System.EventArgs
  {
    public TextInputCommandEventArgs(
      TextInputCommand textInputCommand, string inputText)
    {
      TextInputCommand = textInputCommand;
      InputText = inputText;
    }

    public TextInputCommand TextInputCommand { get; }

    public string InputText { get; }

  }
}