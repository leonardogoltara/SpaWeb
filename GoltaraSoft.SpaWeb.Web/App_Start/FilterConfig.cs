using GoltaraSolutions.SpaWeb.Web.Filters;
using System.Web.Mvc;

namespace GoltaraSolutions.SpaWeb.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new BasicAuthenticationFilter());
        }
    }
}
