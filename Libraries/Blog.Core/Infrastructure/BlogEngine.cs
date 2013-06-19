using Blog.Core.Infrastructure.DependencyManagement;

namespace Blog.Core.Infrastructure
{
    public class BlogEngine : IEngine
    {
        #region Ctor
        
        public BlogEngine()
            : this(new ContainerConfigurer())
        { }

        public BlogEngine(ContainerConfigurer configurer)
        {
            InitializeContainer(configurer);
        } 

        #endregion

        private void InitializeContainer(ContainerConfigurer configurer)
        {
            Container = new UnityContainer();
            configurer.Configure(this, Container);
        }

        public IContainer Container
        {
            get; 
            private set;
        }

        public void Initialize()
        {
            //bool databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();
            //if (databaseInstalled)
            //{
            //    //startup tasks
            //    RunStartupTasks();
            //}
        }
    }
}