using System;
using System.Collections.ObjectModel;
using CookbookMVVM;
using Domain.Interfaces;
using Domain.Models;
using ViewModel.ModelsVMs;

namespace ViewModel.MainWindowVMs
{
  public class CentralPanelVM : ViewModelBase, ICentralPanelVM
  {
    private readonly ObservableCollection<NoteVM> notes;
    private readonly ITimeProvider timeProvider;

    public ObservableCollection<NoteVM> Notes => notes;

    public void AddTimerNote(object sender, string e)
    {
      notes.Add(new NoteVM()
      {
        Model=new Note($"Current time is {timeProvider.Now}", e)
      });
      OnPropertyChanged(nameof(Notes));
    }

    public void EndTimerNote(object sender, TimeSpan e)
    {
      notes.Add(new NoteVM()
      {
        Model = new Note($"Task stopped after {e}", "The task")
      });
      OnPropertyChanged(nameof(Notes));
    }

    public CentralPanelVM(ITimeProvider timeProvider)
    {
      this.timeProvider = timeProvider;
      notes = new ObservableCollection<NoteVM>();
    }
  }
}