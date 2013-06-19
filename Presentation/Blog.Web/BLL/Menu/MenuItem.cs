using System.Collections.Generic;
using System.Xml.Serialization;

namespace BlogMVC.BLL.Menu
{
    [XmlRoot("menuItem")]
    public class MenuItem
    {
        #region Constructors
        
        public MenuItem()
        {
            Items = new List<MenuItem>();
        } 

        #endregion

        #region Properties

        [XmlAttribute("text")]
        public string Text
        {
            get;
            set;
        }

        [XmlAttribute("controller")]
        public string Controller
        {
            get; 
            set;
        }

        [XmlAttribute("action")]
        public string Action
        {
            get; 
            set;
        }

        [XmlAttribute("href")]
        public string Href
        {
            get; 
            set;
        }

        [XmlElement("menuItem")]
        public List<MenuItem> Items
        {
            get;
            set;
        } 

        #endregion

        #region Methods

        public override string ToString()
        {
            return Text;
        }

        #endregion
    }
}