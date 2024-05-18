using System.Web;
using System.Web.Mvc;

namespace Courier_Management_system_Next
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
