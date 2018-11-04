using System;
using System.Windows.Input;
using CookbookMVVM;
using Domain.Enums;
using Domain.Models;

namespace ViewModel.ModelsVMs
{
  public class NoteVM : ViewModelBase<Note>
  {
    public event EventHandler<System.EventArgs> ToggleRunningStateRequested;

    private ICommand toggleRunningState;

    public string Header => Model.Header;

    public string Text => Model.Text;

    public string Duration { get; private set; }

    public bool IsTask => Model.State != NoteState.Unknown;//TODO maybe we need an explicit isTask property in Note;

    public bool IsRunning => Model.State == NoteState.TimerRunning;

    public ICommand ToggleRunningState =>
      toggleRunningState ?? (toggleRunningState = new RelayCommand(ExecuteToggleRunningState));

    private void ExecuteToggleRunningState()
    {
      ToggleRunningStateRequested?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateDuration(TimeSpan duration)
    {
      Duration = duration.ToString();
      OnPropertyChanged(nameof(Duration));
    }
  }
}