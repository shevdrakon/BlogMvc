using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

using BlogMVC.BLL.Menu;

namespace BlogMVC.Content.Controls
{
    public class VerticalMenu : MvcControl
    {
        #region Constructors
        
        public VerticalMenu()
            : base("ul")
        {
            AddClass("cbp-vimenu");
        } 

        #endregion

        #region Properties

        public string SourceXml
        {
            get;
            set;
        }

        #endregion

        protected override void RenderCustomHtml(StringWriter writer, ViewContext viewContext)
        {
            var dataSource = GetDataSource(viewContext);

            if (dataSource != null && dataSource.Items.Count > 0)
            {
                var urlHelper = new UrlHelper(viewContext.RequestContext);

                RenderMenuItems(dataSource.Items, writer, urlHelper);
            }
        }

        private void RenderMenuItems(List<MenuItem> items, StringWriter writer, UrlHelper urlHelper)
        {
            if (items.Count == 0)
                return;

            foreach (var item in items)
            {
                RenderMenuItem(item, writer, urlHelper);
            }
        }

        private void RenderMenuItem(MenuItem item, StringWriter writer, UrlHelper urlHelper)
        {
            var li = new TagBuilder("li");

            writer.Write(li.ToString(TagRenderMode.StartTag));
            RenderActionLink(item, writer, urlHelper);
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
    }
}