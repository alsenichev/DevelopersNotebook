namespace Domain.Models
{
  public class Note
  {
    public Note(string text, string header)
    {
      Text = text;
      Header = header;
    }

    public string Text { get; }

    public string Header { get; }
  }
}