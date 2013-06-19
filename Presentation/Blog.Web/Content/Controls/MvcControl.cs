using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace BlogMVC.Content.Controls
{
    public abstract class MvcControl
    {
        #region Constructors

        protected MvcControl(string tagName)
        {
            Attributes = new SortedDictionary<string, string>(StringComparer.Ordinal);
            TagName = tagName;
        }

        #endregion

        protected string TagName { get; private set; }

        public IDictionary<string, string> Attributes { get; private set; }

        public object HtmlAttributes { get; set; }

        public string Class
        {
            set { AddClass(value); }
        }

        protected string InnerHtml { get; set; }

        public string Style
        {
            set
            {
                var currentValue = string.Empty;

                if (Attributes.TryGetValue("style", out currentValue))
                    Attributes["style"] += value;
                else
                    Attributes.Add("style", value);
            }
        }

        public void AddClass(string className)
        {
            if (string.IsNullOrEmpty(className))
                className = className.Trim();

            string currentClassName;

            if (Attributes.TryGetValue("class", out currentClassName))
            {
                currentClassName = currentClassName.Trim();

                Attributes["class"] = currentClassName + " " + className;
            }
            else
            {
                Attributes["class"] = className;
            }
        }

        public MvcHtmlString Html(ViewContext viewContext)
        {
            if (viewContext == null)
                throw new ArgumentNullException("viewContext");

            var html = new StringBuilder();

            Initialize(viewContext);

            using (var writer = new StringWriter(html))
            {
                RenderHtml(writer, viewContext);
            }

            return MvcHtmlString.Create(html.ToString());
        }

        protected virtual TagBuilder GetTagBuilder()
        {
            var tagBuilder = new TagBuilder(TagName);

            var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(HtmlAttributes);
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttributes(Attributes);
            tagBuilder.InnerHtml = InnerHtml;

            return tagBuilder;
        }

        protected virtual void RenderHtml(StringWriter writer, ViewContext viewContext)
        {
            var tagBuilder = GetTagBuilder();

            writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            RenderCustomHtml(writer, viewContext);
            writer.Write(tagBuilder.ToString(TagRenderMode.EndTag));
        }

        protected virtual void RenderCustomHtml(StringWriter writer, ViewContext viewContext)
        {
        }

        protected virtual void Initialize(ViewContext viewContext)
        {
        }

        protected string ToJson(object o)
        {
            var serializer = JsonSerializer.Create(new JsonSerializerSettings());
            var result = new StringBuilder();

            using (var writer = new StringWriter(result))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, o);
            }

            return result.ToString();
        }
    }
}