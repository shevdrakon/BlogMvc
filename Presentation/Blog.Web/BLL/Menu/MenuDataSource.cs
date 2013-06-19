using System.Collections.Generic;
using System.Xml.Serialization;

namespace BlogMVC.BLL.Menu
{
    [XmlRoot("menu")]
    public class MenuDataSource
    {
        [XmlElement("menuItem")]
        public List<MenuItem> Items
        {
            get; 
            set;
        }
    }
}