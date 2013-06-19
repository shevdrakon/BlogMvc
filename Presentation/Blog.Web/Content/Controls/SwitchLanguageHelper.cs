using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

using Blog.Web.Framework;

namespace BlogMVC.Content.Controls
{
    public static class SwitchLanguageHelper
    {
        private class Language
        {
            public string Url
            {
                get;
                set;
            }

            public RouteValueDictionary RouteValues
            {
                get;
                set;
            }

            public bool IsSelected
            {
                get;
                set;
            }
        }

        public static MvcHtmlString LanguageSelectorLink(this HtmlHelper helper,
           string cultureName, string selectedText, string unselectedText,
           IDictionary<string, object> htmlAttributes, string languageRouteName = "lang", bool strictSelected = false)
        {
            var language = helper.LanguageUrl(cultureName, languageRouteName, strictSelected);

            MvcHtmlString link = null;

            if (!language.IsSelected)
            {
                var text = language.IsSelected ? selectedText : unselectedText;
                link = helper.RouteLink(text, null, language.RouteValues, htmlAttributes);
            }

            return link;
        }

        private static Language LanguageUrl(this HtmlHelper helper, string cultureName,
            string languageRouteName = "lang", bool strictSelected = false)
        {
            // set the input language to lower
            cultureName = cultureName.ToLower();
            // retrieve the route values from the view context
            var routeValues = new RouteValueDictionary(helper.ViewContext.RouteData.Values);
            // copy the query strings into the route values to generate the link
            var queryString = helper.ViewContext.HttpContext.Request.QueryString;
            foreach (string key in queryString)
            {
                if (queryString[key] != null && !string.IsNullOrWhiteSpace(key))
                {
                    if (routeValues.ContainsKey(key))
                    {
                        routeValues[key] = queryString[key];
                    }
                    else
                    {
                        routeValues.Add(key, queryString[key]);
                    }
                }
            }

            // set the language into route values
            routeValues[languageRouteName] = cultureName;

            if (routeValues.ContainsKey(Constants.PAGE_INDEX))
                routeValues[Constants.PAGE_INDEX] = 1;

            // generate the language specify url
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);
            var url = urlHelper.RouteUrl(routeValues);

            // check whether the current thread ui culture is this language
            var current_lang_name = Thread.CurrentThread.CurrentUICulture.Name.ToLower();
            var isSelected = strictSelected
                                 ? current_lang_name == cultureName
                                 : current_lang_name.StartsWith(cultureName);

            return new Language
            {
                Url = url,
                RouteValues = routeValues,
                IsSelected = isSelected
            };
        }
    }
}