using System;
using System.Web.Mvc;

namespace Blog.Web.Framework.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            LocalizationControllerHelper.OnBeginExecuteCore(this);

            return base.BeginExecuteCore(callback, state);
        }
    }
}