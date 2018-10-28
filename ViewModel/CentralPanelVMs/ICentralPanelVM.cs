using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Domain.Models;
using ViewModel.ModelsVMs;

namespace ViewModel.CentralPanelVMs
{
  public interface ICentralPanelVM
  {
    event EventHandler<EventArgs> ItemsPositionChanged;

    event EventHandler<EventArgs> NoteCommandReceived;

    ObservableCollection<NoteVM> Notes { get; }

    void InitializeNotes(IList<Note> source);

    void CreateNote(string text);

    void CreateNewTask(string text);

    void ContinueTask(NoteVM noteVM);

    void StopTask(TimeSpan elapsedTimer);

    void StopAnyRunningTask(TimeSpan elapsedTimer);

    event PropertyChangedEventHandler PropertyChanged;
  }
}