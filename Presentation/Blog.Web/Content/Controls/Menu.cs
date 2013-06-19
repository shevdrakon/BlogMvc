using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using BlogMVC.BLL.Menu;

namespace BlogMVC.Content.Controls
{
    public class Menu : MvcControl
    {
        #region Constructors

        public Menu()
            : base("nav")
        {
            AddClass("primary-nav");
        }

        #endregion

        #region Properties

        public string SourceXml
        {
            get;
            set;
        }

        #endregion

        #region Methods

        protected override void RenderCustomHtml(StringWriter writer, ViewContext viewContext)
        {
            var dataSource = GetDataSource(viewContext);

            if (dataSource != null && dataSource.Items.Count > 0)
            {
                var urlHelper = new UrlHelper(viewContext.RequestContext);

                RenderMenuItems(dataSource.Items, writer, urlHelper, "sf-menu sf-shadow");
            }
        }

        private void RenderMenuItems(
           List<MenuItem> items,
           StringWriter writer,
           UrlHelper urlHelper,
           string cssClass)
        {
            if (items.Count == 0)
                return;

            var ul = new TagBuilder("ul");
            ul.AddCssClass(cssClass);

            writer.Write(ul.ToString(TagRenderMode.StartTag));

            foreach (var item in items)
            {
                RenderMenuItem(item, writer, urlHelper);
            }

            writer.Write(ul.ToString(TagRenderMode.EndTag));
        }

        private void RenderMenuItem(MenuItem item, StringWriter writer, UrlHelper urlHelper)
        {
            var li = new TagBuilder("li");

            writer.Write(li.ToString(TagRenderMode.StartTag));

            RenderActionLink(item, writer, urlHelper);

            if (item.Items.Count != 0)
                RenderMenuItems(item.Items, writer, urlHelper, string.Empty);

            writer.Write(li.ToString(TagRenderMode.EndTag));
        }

        private void RenderActionLink(MenuItem item, StringWriter writer, UrlHelper urlHelper)
        {
            var href = string.IsNullOrEmpty(item.Href) ? "#" : item.Href;

            if (!string.IsNullOrEmpty(item.Controller))
                href = urlHelper.Action(item.Action, item.Controller);

            var a = new TagBuilder("a");
            a.Attributes.Add("href", href);
            a.SetInnerText(item.Text);

            if (item.Items.Count > 0)
            {
                a.AddCssClass("sf-with-ul");

                var span = new TagBuilder("span");
                span.AddCssClass("sf-sub-indicator");

                a.InnerHtml += span;
            }

            writer.Write(a.ToString());
        }

        private MenuDataSource GetDataSource(ViewContext viewContext)
        {
            MenuDataSource result = null;

            if (!string.IsNullOrEmpty(SourceXml))
            {
                var physicalPath = viewContext.RequestContext.HttpContext.Request.MapPath(SourceXml);

                if (File.Exists(physicalPath))
                {
                    var serializer = new XmlSerializer(typeof(MenuDataSource));

                    using (var fileStream = File.OpenRead(physicalPath))
                    using (var reader = XmlReader.Create(fileStream))
                    {
                        if (serializer.CanDeserialize(reader))
                             result = (MenuDataSource)serializer.Deserialize(reader);
                    }
                }
            }

            return result;
        }

        #endregion
    }
}