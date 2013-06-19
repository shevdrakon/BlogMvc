namespace Blog.Core.Infrastructure.DependencyManagement
{
    public interface IDependencyRegistrar
    {
        void Register(IContainer container, ITypeFinder typeFinder);

        int Order
        {
            get;
        }
    }
}