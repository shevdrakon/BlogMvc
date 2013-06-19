using Blog.Web.Framework;

using System.Web.Mvc;
using System.Web.Routing;

namespace BlogMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "TagPaging",
                url: string.Format("{{{0}}}/{{controller}}/Tag/{{tagName}}/{{{1}}}", Constants.ROUTE_PARAMNAME_LANG, Constants.PAGE_INDEX),
                defaults: new { controller = "Blog", action = "Tag", pageIndex = 1 }
                );

            routes.MapRoute(
                name: "IndexPaging",
                url: string.Format("{{{0}}}/{{controller}}/Index/{{{1}}}", Constants.ROUTE_PARAMNAME_LANG, Constants.PAGE_INDEX),
                defaults: new { controller = "Blog", action = "Index", pageIndex = 1 }
                );

            routes.MapRoute(
                name: "SearchPaging",
                url: string.Format("{{{0}}}/{{controller}}/Search/{{{1}}}", Constants.ROUTE_PARAMNAME_LANG, Constants.PAGE_INDEX),
                defaults: new { controller = "Blog", action = "Search", pageIndex = 1 }
                );

            routes.MapRoute(
                name: Constants.ROUTE_NAME,
                url: string.Format("{{{0}}}/{{controller}}/{{action}}/{{id}}", Constants.ROUTE_PARAMNAME_LANG),
                defaults: new {controller = "Blog", action = "Index", id = UrlParameter.Optional}
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{pageIndex}",
                defaults: new { controller = "Blog", action = "Index", pageIndex = 1 }
            );
        }
    }
}