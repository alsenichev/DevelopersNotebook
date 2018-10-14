using System;
using Domain.Enums;

namespace Domain.Models
{
  public struct Note : IEquatable<Note>
  {
    public Note(
      string text,
      string header,
      NoteState state,
      DateTime startedAt,
      TimeSpan duration)
    {
      Text = text;
      Header = header;
      State = state;
      StartedAt = startedAt;
      Duration = duration;
    }

    #region public properties

    public string Text { get; }

    public string Header { get; }

    public NoteState State { get; }

    public DateTime StartedAt { get; }

    public TimeSpan Duration { get; }

    #endregion

    #region public methods

    /// <summary>
    /// Returns the Note which has a Completed state in the case the state is TimerRunning.
    /// This is necessary when by some reason (application crash, for example) the Note with a
    /// TimerRunning state remained on disk, but we don't want any Notes with TimerRunning state on startup.
    /// </summary>
    /// <returns></returns>
    public Note FromDisk()
    {
      return new Note(
        Text,
        Header,
        State == NoteState.TimerRunning ? NoteState.TimerStopped : State,
        StartedAt,Duration);
    }
    #endregion

    #region Equality members

    public bool Equals(Note other)
    {
      return string.Equals(Text, other.Text) &&
             string.Equals(Header, other.Header) &&
             State == other.State &&
             StartedAt.Equals(other.StartedAt) &&
             Duration.Equals(other.Duration);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj))
      {
        return false;
      }

      return obj is Note other && Equals(other);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = (Text != null ? Text.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^
                   (Header != null ? Header.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (int) State;
        hashCode = (hashCode * 397) ^ StartedAt.GetHashCode();
        hashCode = (hashCode * 397) ^ Duration.GetHashCode();
        return hashCode;
      }
    }

    #endregion
  }
}