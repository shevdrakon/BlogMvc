using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Framework.Controllers
{
    public class LocalizationControllerHelper
    {
        public static void OnBeginExecuteCore(Controller controller)
        {
            if (controller.RouteData.Values[Constants.ROUTE_PARAMNAME_LANG] != null &&
                !string.IsNullOrWhiteSpace(controller.RouteData.Values[Constants.ROUTE_PARAMNAME_LANG].ToString()))
            {
                // set the culture from the route data (url)
                var lang = controller.RouteData.Values[Constants.ROUTE_PARAMNAME_LANG].ToString();
                SetCulture(lang);
            }
            else
            {
                // load the culture info from the cookie
                var cookie = controller.HttpContext.Request.Cookies[Constants.COOKIE_NAME];
                var langHeader = string.Empty;

                if (cookie != null)
                {
                    // set the culture by the cookie content
                    langHeader = cookie.Value;
                    SetCulture(langHeader);
                }
                else
                {
                    // set the culture by the location if not speicified
                    langHeader = controller.HttpContext.Request.UserLanguages[0];
                    SetCulture(langHeader);
                }

                // set the lang value into route data
                controller.RouteData.Values[Constants.ROUTE_PARAMNAME_LANG] = langHeader.Split('-')[0];
            }

            // save the location into cookie
            var responseCookie = new HttpCookie(Constants.COOKIE_NAME, Thread.CurrentThread.CurrentUICulture.Name);
            responseCookie.Expires = DateTime.Now.AddDays(5);
            controller.HttpContext.Response.SetCookie(responseCookie);
        }

        private static void SetCulture(string langHeader)
        {
            Thread.CurrentThread.CurrentUICulture =
                Thread.CurrentThread.CurrentCulture =
                CultureInfo.CreateSpecificCulture(langHeader);
        }
    }
}