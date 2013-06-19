using System.IO;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BlogMVC.Content.Controls
{
    public class jQueryTE : MvcControl
    {
        #region Constuctors

        public jQueryTE(string id)
            : base("textarea")
        {
            MinHeight = new Unit("200");
            MaxHeight = new Unit("500");

            Id = id;
            Attributes.Add("id", id);
            Attributes.Add("name", id);
        }

        #endregion

        private string Id
        {
            get;
            set;
        }

        public Unit MinHeight
        {
            get; 
            set;
        }

        public Unit MaxHeight
        {
            get;
            set;
        }

        protected override void RenderHtml(StringWriter writer, ViewContext viewContext)
        {
            base.RenderHtml(writer, viewContext);

            var settings = ToJson(new
            {
                minHeight = MinHeight.ToString(),
                maxHeight = MaxHeight.ToString()
            });

            writer.Write(string.Format("<script>$('#{0}').jqte({1});</script>", Id, settings));
        }
    }
}