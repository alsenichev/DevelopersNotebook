using System;

namespace Domain.Models
{
  public struct Note
  {
    public Note(
      string text, string header, NoteState state, DateTime timerStarted,
      DateTime timerStopped)
    {
      Text = text;
      Header = header;
      State = state;
      TimerStarted = timerStarted;
      TimerStopped = timerStopped;
    }

    public string Text { get; }

    public string Header { get; }

    public NoteState State { get; }

    public DateTime TimerStarted { get; }

    public DateTime TimerStopped { get; }
  }
}