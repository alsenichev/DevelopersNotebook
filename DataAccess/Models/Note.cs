using System;
using Domain.Enums;

namespace DataAccess.Models
{
  public struct Note
  {
    public string Text { get; set; }

    public string Header { get; set; }

    public NoteState State { get; set; }

    public DateTime StartedAt { get; set; }

    public TimeSpan Duration { get; set; }
  }
}