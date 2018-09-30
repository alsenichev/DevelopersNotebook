using System.Windows;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DevelopersNotebook.StartUp;

namespace DevelopersNotebook.Infrastructure.DependencyInjection
{
  public class ViewInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Register(
        Component.For<Window>().ImplementedBy<MainWindow>(),
        Component.For<ApplicationInitialization>()
      );
    }
  }
}