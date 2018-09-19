using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace DevelopersNotebook.Infrastructure.DependencyInjection
{
  public class ViewModelInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Register(
        Classes.FromAssemblyNamed("ViewModel")
          .Pick()
          .WithServiceDefaultInterfaces());
    }
  }
}