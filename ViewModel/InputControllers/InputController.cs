﻿using System;
using System.Linq;
using Domain.BusinessRules.Services;
using Domain.Interfaces;
using ViewModel.BottomPanelVMs;
using ViewModel.CentralPanelVMs;
using ViewModel.Enums;
using ViewModel.EventArguments;
using ViewModel.ModelsVMs;
using ViewModel.TotalCounterVMs;

namespace ViewModel.InputControllers
{
  public class InputController : IInputController
  {
    private readonly ITimer timer;
    private readonly ITotalCounterVM totalCounterVM;
    private readonly ICentralPanelVM centralPanelVM;
    private readonly IMainRepository mainRepository;
    private readonly DailyTimeCalculation dailyTimeCalculation;
    private readonly IBottomPanelVM bottomPanelVM;

    public InputController(
      ICentralPanelVM centralPanelVM,
      ITimer timer,
      IMainRepository mainRepository,
      DailyTimeCalculation dailyTimeCalculation,
      ITotalCounterVM totalCounterVM, IBottomPanelVM bottomPanelVM)
    {
      this.centralPanelVM = centralPanelVM;
      this.timer = timer;
      this.mainRepository = mainRepository;
      this.dailyTimeCalculation = dailyTimeCalculation;
      this.totalCounterVM = totalCounterVM;
      this.bottomPanelVM = bottomPanelVM;
      this.bottomPanelVM.UserInputReceived += HandleUserInput;
      this.centralPanelVM.NoteCommandReceived += HandleNoteCommand;
    }

    public void PrepareToShutdownApplication()
    {
      if (timer.IsRunning)
      {
        centralPanelVM.StopAnyRunningTask(timer.Elapsed);
        timer.Stop();
      }

      mainRepository.SaveNotes(centralPanelVM.Notes.Select(n => n.Model));
    }

    private void HandleUserInput(object sender, TextInputCommandEventArgs e)
    {
      switch (e.TextInputCommand)
      {
        case TextInputCommand.CreateNote:
          centralPanelVM.CreateNote(e.InputText);
          break;
        case TextInputCommand.StartTask:
          if (timer.IsRunning)
          {
            centralPanelVM.StopTask(timer.Elapsed);
            totalCounterVM.UpdateCounter(timer.Elapsed);
            timer.Stop();
            timer.Reset();
          }

          centralPanelVM.CreateNewTask(e.InputText);
          timer.Start();
          break;
        case TextInputCommand.StopTask:
          if (timer.IsRunning)
          {
            centralPanelVM.StopTask(timer.Elapsed);
            totalCounterVM.UpdateCounter(timer.Elapsed);
            timer.Stop();
            timer.Reset();
          }

          break;
        case TextInputCommand.PinNote:

          // TODO
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      mainRepository.SaveNotes(centralPanelVM.Notes.Select(n => n.Model));
    }

    private void HandleNoteCommand(object sender, EventArgs e)
    {
      var sourceTask = (NoteVM) sender;
      if (timer.IsRunning)
      {
        bool sourceIsRunning = sourceTask.IsRunning;
        centralPanelVM.StopTask(timer.Elapsed);
        totalCounterVM.UpdateCounter(timer.Elapsed);
        timer.Stop();
        timer.Reset();
        if (!sourceIsRunning)
        {
          // source was not running means some other task requested start
          centralPanelVM.ContinueTask(sourceTask);
          timer.Start();
        }
      }
      else if (dailyTimeCalculation.IsTodaysTask(sourceTask.Model))
      {
        centralPanelVM.ContinueTask((NoteVM) sender);
        timer.Start();
      }
      else
      {
        centralPanelVM.CreateNewTask(sourceTask.Header);
        timer.Start();
      }
    }
  }
}