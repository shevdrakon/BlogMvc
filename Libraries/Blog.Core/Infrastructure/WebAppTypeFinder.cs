using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace Blog.Core.Infrastructure
{
    public class WebAppTypeFinder : AppDomainTypeFinder
    {
        #region Fields

        private bool _binFolderAssembliesLoaded = false;

        #endregion

        #region Ctor

        public WebAppTypeFinder()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a physical disk path of \Bin directory
        /// </summary>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        public virtual string GetBinDirectory()
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HttpRuntime.BinDirectory;
            }
            else
            {
                //not hosted. For example, run either in unit tests
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }


        public override IList<Assembly> GetAssemblies()
        {
            if (!_binFolderAssembliesLoaded)
            {
                _binFolderAssembliesLoaded = true;
                string binPath = GetBinDirectory();
                //binPath = _webHelper.MapPath("~/bin");
                LoadMatchingAssemblies(binPath);
            }

            return base.GetAssemblies();
        }

        #endregion
    }
}