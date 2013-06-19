using System;
using System.Reflection;
using System.Web.Mvc;

namespace BlogMVC.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AsyncRequestOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var headers = controllerContext.HttpContext.Request.Headers;
            var value = headers["X-Requested-With"];

            return !string.IsNullOrEmpty(value);
        }
    }
}