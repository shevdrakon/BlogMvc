using System;
using System.Web.Mvc;

namespace BlogMVC.Content.Controls
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString MvcControl(this HtmlHelper htmlHelper, MvcControl mvcControl)
        {
            if (mvcControl == null)
                throw new ArgumentNullException("mvcControl");

            return mvcControl.Html(htmlHelper.ViewContext);
        }
    }
}