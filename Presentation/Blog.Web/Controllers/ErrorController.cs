using System;
using System.Web.Mvc;

namespace BlogMVC.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(int statusCode, Exception exception)
        {
            Response.StatusCode = statusCode;

            return View();
        }

        public ActionResult HttpError404(int statusCode, Exception exception)
        {
            Response.StatusCode = statusCode;

            return View();
        }
    }
}