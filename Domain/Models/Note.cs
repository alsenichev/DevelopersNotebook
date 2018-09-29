using System;
using Domain.Enums;

namespace Domain.Models
{
  public struct Note
  {
    public Note(
      string text, string header, NoteState state, DateTime startedAt,
      TimeSpan duration)
    {
      Text = text;
      Header = header;
      State = state;
      StartedAt = startedAt;
      Duration = duration;
    }

    public string Text { get; }

    public string Header { get; }

    public NoteState State { get; }

    public DateTime StartedAt { get; }

    public TimeSpan Duration { get; }
  }
}