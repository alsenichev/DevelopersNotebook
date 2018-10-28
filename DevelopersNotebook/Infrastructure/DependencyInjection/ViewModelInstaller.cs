using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ViewModel.BottomPanelVMs;

namespace DevelopersNotebook.Infrastructure.DependencyInjection
{
  public class ViewModelInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Register(
        // in CastleWindsor the first one wins
        Component.For<IReadOnlyTimer, ITimer>().ImplementedBy<Timer>().LifestyleSingleton(),
        Classes.FromAssemblyNamed("ViewModel")
          .Pick()
          .WithServiceDefaultInterfaces());
    }
  }
}