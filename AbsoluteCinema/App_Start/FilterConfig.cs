using System.Web;
using System.Web.Mvc;

namespace ABSOLUTE_CINEMA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}