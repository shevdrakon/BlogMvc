using BlogMVC.App_Start;

using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Blog.Core.Infrastructure;

namespace BlogMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            EngineContext.Initialize(false);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AuthConfig.RegisterAuth();

            MemoryDataConfig.AddPosts();
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    var exception = Server.GetLastError();
        //    Server.ClearError();

        //    var routeData = new RouteData();
        //    routeData.Values.Add("controller", "Error");
        //    routeData.Values.Add("error", exception);

        //    var statusCode = exception.GetType() == typeof (HttpException)
        //                         ? ((HttpException) exception).GetHttpCode()
        //                         : 500;

        //    routeData.Values.Add("statusCode", statusCode);

        //    IController controller = new ErrorController();

        //    var actionName = string.Format("HttpError{0}", statusCode);
        //    if (controller.GetType().GetMethod(actionName) == null)
        //        actionName = "Index";

        //    routeData.Values.Add("action", actionName);

        //    controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        //}
    }
}