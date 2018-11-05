using System;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services
{
  public class NoteFactory : INoteFactory
  {
    public Note CreateTask(string text, DateTime timeStarted)
    {
      return new Note(
        $"Started at {timeStarted}", text, NoteState.TimerRunning, timeStarted, TimeSpan.Zero);
    }

    public Note CreateNote(string text, DateTime timeCreated)
    {
      return new Note(
        "", text, NoteState.Unknown, timeCreated, TimeSpan.Zero);
    }

    public Note PausedTask(Note note, DateTime timePaused, TimeSpan elapsedTimer)
    {
      return new Note(
        $"Started at {note.StartedAt}. Paused at {timePaused}. Duration: {note.Duration + elapsedTimer}.",
        note.Header,
        NoteState.TimerPaused,
        note.StartedAt,
        note.Duration + elapsedTimer);
    }

    public Note StoppedTask(Note note, DateTime timeStopped, TimeSpan elapsedTimer)
    {
      return new Note(
        $"Started at {note.StartedAt}. Completed at {timeStopped}. Duration: {note.Duration + elapsedTimer}.",
        note.Header,
        NoteState.TimerStopped,
        note.StartedAt,
        note.Duration + elapsedTimer);
    }

    public Note ResumedTask(Note note)
    {
      return new Note(
        $"Started at {note.StartedAt}. Duration so far: {note.Duration}",
        note.Header,
        NoteState.TimerRunning,
        note.StartedAt,
        note.Duration);
    }
  }
}