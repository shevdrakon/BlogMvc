using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace BlogMVC.Content.Controls
{
    public class WaterMark : MvcControl
    {
        #region Constructors
        
        public WaterMark()
            : base("script")
        {
        } 

        #endregion

        #region Properties
        
        public string Text
        {
            get;
            set;
        }

        public string ElementsSelector
        {
            get;
            set;
        } 

        #endregion

        #region Methods

        protected override void RenderHtml(StringWriter writer, ViewContext viewContext)
        {
            if (string.IsNullOrEmpty(Text) || string.IsNullOrEmpty(ElementsSelector))
                return;

            var script = new TagBuilder("script");
            var settings = ToJson(new
            {
                text = Text,
                elementsSelector = ElementsSelector
            });

            script.InnerHtml = string.Format("new waterMark({0})", settings);

            writer.Write(script);
        }

        #endregion
    }
}