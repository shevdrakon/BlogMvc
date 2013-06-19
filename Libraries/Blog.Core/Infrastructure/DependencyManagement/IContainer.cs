using System;

namespace Blog.Core.Infrastructure.DependencyManagement
{
    public enum InstanceHandle
    {
        Instance = 0,
        Singleton = 1
    }

    public interface IContainer
    {
        T Resolve<T>();

        object Resolve(Type type);

        void Register<TFrom, TTo>()
            where TTo : TFrom;

        void Register<TFrom, TTo>(InstanceHandle instanceHandle)
            where TTo : TFrom;

        void RegisterInstance<T>(object instance, InstanceHandle instanceHandle);
    }
}