using Microsoft.Practices.Unity;

using System;

namespace Blog.Core.Infrastructure.DependencyManagement
{
    public class UnityContainer : IContainer
    {
        private static readonly IUnityContainer _container = new Microsoft.Practices.Unity.UnityContainer();

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public void Register<TFrom, TTo>()
            where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>();
        }

        public void Register<TFrom, TTo>(InstanceHandle instanceHandle)
            where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>(GetLifeTimeManager(instanceHandle));
        }

        public void RegisterInstance<T>(object instance, InstanceHandle instanceHandle)
        {
            _container.RegisterInstance(typeof (T), instance, GetLifeTimeManager(instanceHandle));
        }

        private static LifetimeManager GetLifeTimeManager(InstanceHandle instanceHandle)
        {
            switch (instanceHandle)
            {
                case InstanceHandle.Instance:
                    return new ContainerControlledLifetimeManager();
                
                case InstanceHandle.Singleton:
                    return new TransientLifetimeManager();
                
                default:
                    throw new ArgumentOutOfRangeException("instanceHandle");
            }
        }
    }
}