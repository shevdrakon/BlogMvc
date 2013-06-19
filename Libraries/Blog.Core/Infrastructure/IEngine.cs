using Blog.Core.Infrastructure.DependencyManagement;

namespace Blog.Core.Infrastructure
{
    public interface IEngine
    {
        IContainer Container
        {
            get;
        }

        void Initialize();
    }
}