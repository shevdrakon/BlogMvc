using System.Web.Mvc;
using Blog.Core.Infrastructure;
using Blog.Core.Infrastructure.DependencyManagement;
using Blog.Services.Articles;
using Blog.Web.Framework.Controllers;
using Blog.Web.Framework.Mappers;

namespace Blog.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IContainer container, ITypeFinder typeFinder)
        {
            container.Register<IMapper, Mapper>(InstanceHandle.Singleton);
            container.Register<IArticleRepository, ArticleMemoryRepository>();

            var controllerActivator = new ControllerActivator(container);
            ControllerBuilder.Current.SetControllerFactory(new DefaultControllerFactory(controllerActivator));
        }

        public int Order
        {
            get { return 0; }
        }
    }
}