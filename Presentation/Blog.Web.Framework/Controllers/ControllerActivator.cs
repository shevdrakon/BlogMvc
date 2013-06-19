using System;
using System.Web.Mvc;
using System.Web.Routing;

using Blog.Core.Infrastructure.DependencyManagement;

namespace Blog.Web.Framework.Controllers
{
    public class ControllerActivator : IControllerActivator 
    {
        #region Constructors

        public ControllerActivator(IContainer container)
        {
            Container = container;
        } 

        #endregion

        #region Properties
        
        private IContainer Container
        {
            get;
            set;
        } 

        #endregion

        #region Methods
        
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return (IController) Container.Resolve(controllerType);
        } 

        #endregion
    }
}