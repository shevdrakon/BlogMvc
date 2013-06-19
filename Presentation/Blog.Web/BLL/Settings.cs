using System.Configuration;

namespace BlogMVC.BLL
{
    public class Settings
    {
        public static int PageSize
        {
            get
            {
                var pageSize = ConfigurationManager.AppSettings["PageSize"];

                var result = 4;
                int.TryParse(pageSize, out result);

                return result;
            }
        }
    }
}