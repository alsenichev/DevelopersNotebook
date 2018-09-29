using System;
using Domain.Models;

namespace Domain.Interfaces
{
  public interface INoteFactory
  {
    Note CreateTask(string text, DateTime timeStarted);

    Note CreateNote(string text);

    Note PauseTask(Note model, DateTime timeStopped, TimeSpan elapsedTimer);

    Note StopTask(Note note, DateTime timeStopped, TimeSpan elapsedTimer);

    Note ResumeTask(Note note);
  }
}