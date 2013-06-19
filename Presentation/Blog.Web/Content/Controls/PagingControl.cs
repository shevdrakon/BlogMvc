using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using Blog.Web.Framework;

namespace BlogMVC.Content.Controls
{
    public class PagingControl : MvcControl
    {
        #region Constructors
        
        public PagingControl(int pagesCount)
            : base("div")
        {
            PagesCount = pagesCount;
            AddClass("pager-area");
        }

        #endregion

        #region Properties
        
        private int PagesCount
        {
            get;
            set;
        } 

        #endregion

        #region Methods

        protected override void RenderCustomHtml(StringWriter writer, ViewContext viewContext)
        {
            if (PagesCount == 1)
                return;

            var divContainer = new TagBuilder("div");
            divContainer.AddCssClass("showpage-area");

            writer.Write(divContainer.ToString(TagRenderMode.StartTag));
            RenderPagesLinks(writer, viewContext);
            writer.Write(divContainer.ToString(TagRenderMode.EndTag));
        }

        private void RenderPagesLinks(StringWriter writer, ViewContext viewContext)
        {
            var offset = 2;

            var currentIndex = GetCurrentPageIndex(viewContext);
            var startIndex = Math.Max(currentIndex - offset, 1);
            var endIndex = Math.Min(currentIndex + offset, PagesCount);

            if (startIndex != 1)
            {
                RenderPageLink(1, writer, viewContext);

                if (startIndex - 1 != 1)
                    RenderHasMorePagesLabel(writer);
            }

            for (var index = startIndex; index <= endIndex; index++)
            {
                if (index == GetCurrentPageIndex(viewContext))
                    RenderCurrentPageLabel(index, writer);
                else
                    RenderPageLink(index, writer, viewContext);
            }

            if (endIndex != PagesCount)
            {
                if (endIndex + 1 != PagesCount)
                    RenderHasMorePagesLabel(writer);

                RenderPageLink(PagesCount, writer, viewContext);
            }
        }

        private void RenderHasMorePagesLabel(StringWriter writer)
        {
            var span = new TagBuilder("span");
            span.AddCssClass("paging-dots");
            span.SetInnerText("...");
            
            writer.Write(span.ToString());
        }

        private void RenderPageLink(int pageIndex, StringWriter writer, ViewContext viewContext)
        {
            var routeData = new RouteValueDictionary(viewContext.RouteData.Values);
            routeData[Constants.PAGE_INDEX] = pageIndex;

            CopyQueryString(routeData, viewContext.RequestContext.HttpContext.Request.QueryString);

            var link = HtmlHelper.GenerateLink(
                viewContext.RequestContext,
                RouteTable.Routes,
                pageIndex.ToString(),
                null, null, null, routeData, new Dictionary<string, object> {{"class", "number"}});

            writer.Write(link);
        }

        private void CopyQueryString(RouteValueDictionary data, NameValueCollection queryString)
        {
            foreach (string key in queryString.Keys)
            {
                data.Add(key, queryString[key]);
            }
        }

        private void RenderCurrentPageLabel(int pageIndex, StringWriter writer)
        {
            var span = new TagBuilder("span");
            span.AddCssClass("number current");
            span.SetInnerText(pageIndex.ToString());

            writer.Write(span.ToString());
        }

        private int GetCurrentPageIndex(ViewContext viewContext)
        {
            var pageIndex = viewContext.RouteData.Values[Constants.PAGE_INDEX];
            if (pageIndex == null)
                throw new InvalidDataException();

            return int.Parse(pageIndex.ToString());
        }

        #endregion
    }
}