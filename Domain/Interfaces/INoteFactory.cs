using System;
using Domain.Models;

namespace Domain.Interfaces
{
  public interface INoteFactory
  {
    Note CreateTask(string text, DateTime timeStarted);

    Note CreateNote(string text);

    Note PausedTask(Note model, DateTime timeStopped, TimeSpan elapsedTimer);

    Note StoppedTask(Note note, DateTime timeStopped, TimeSpan elapsedTimer);

    Note ResumedTask(Note note);
  }
}