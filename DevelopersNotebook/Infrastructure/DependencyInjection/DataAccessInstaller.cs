using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace DevelopersNotebook.Infrastructure.DependencyInjection
{
  public class DataAccessInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Register(
        Classes.FromAssemblyNamed("DataAccess")
          .Where(n => n.Namespace.Contains("Services"))
          .WithServiceDefaultInterfaces());
    }
  }
}