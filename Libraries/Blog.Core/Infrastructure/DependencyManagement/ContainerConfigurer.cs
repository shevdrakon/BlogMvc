using System;
using System.Linq;

namespace Blog.Core.Infrastructure.DependencyManagement
{
    public class ContainerConfigurer
    {
        public virtual void Configure(IEngine engine, IContainer container)
        {
            container.RegisterInstance<IEngine>(engine, InstanceHandle.Singleton);
            container.Register<ITypeFinder, WebAppTypeFinder>();

            //register dependencies provided by other assemblies
            var typeFinder = container.Resolve<ITypeFinder>();

            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = drTypes.Select(drType => (IDependencyRegistrar) Activator.CreateInstance(drType)).ToList();

            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
            {
                dependencyRegistrar.Register(container, typeFinder);
            }
        }
    }
}