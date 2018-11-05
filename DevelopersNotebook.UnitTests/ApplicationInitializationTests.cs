using System;
using System.Collections.Generic;
using DevelopersNotebook.StartUp;
using Domain.BusinessRules.Services;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Domain.Services;
using Moq;
using NUnit.Framework;
using ViewModel.CentralPanelVMs;
using ViewModel.MainWindowVMs;
using ViewModel.TotalCounterVMs;

namespace DevelopersNotebook.UnitTests
{
  [TestFixture]
  public class ApplicationInitializationTests
  {
    private ApplicationInitialization CreateApplicationInitialization(
      ICentralPanelVM centralPanelVM = null,
      IMainRepository mainRepository = null,
      ITotalCounterVM totalCounterVM = null,
      IMainWindowVM mainWindowVM = null)
    {
      if (totalCounterVM == null)
      {
        totalCounterVM = new Mock<ITotalCounterVM>().Object;
      }
      return new ApplicationInitialization(
        centralPanelVM ?? new Mock<ICentralPanelVM>().Object,
        mainRepository ?? new Mock<IMainRepository>().Object,
        new DailyTimeCalculation(new TimeProvider()),
        totalCounterVM,
        mainWindowVM ?? new Mock<IMainWindowVM>().Object);
    }

    [Test]
    public void InitializeBeforeShowingTheWindow_Always_InjectsSafeNotesToCentralPanelVM()
    {
      var note = new Note("", "", NoteState.TimerRunning, DateTime.MinValue,
        TimeSpan.FromMinutes(1));
      var repository = new Mock<IMainRepository>();
      repository.Setup(r => r.LoadNotes()).Returns(new[] {note});
      var centralPanel = new Mock<ICentralPanelVM>();
      var init = CreateApplicationInitialization(centralPanel.Object, repository.Object);
      init.InitializeBeforeShowingTheWindow();
      centralPanel.Verify(p =>
          p.InitializeNotes(It.Is<IList<Note>>(l => l[0].State == NoteState.TimerStopped)),
          Times.Once);
    }
  }
}