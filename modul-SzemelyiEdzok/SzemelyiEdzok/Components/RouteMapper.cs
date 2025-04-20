using DotNetNuke.Web;
using DotNetNuke.Web.Mvc.Routing;
using System.Diagnostics;

namespace SzemelyiEdzokSzemelyiEdzok.Components
{
    public class RouteMapper : IMvcRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapRoute(
                "SzemelyiEdzok",
                "SzemelyiEdzok",
                "{controller}/{action}",
                new string[] { "SzemelyiEdzokSzemelyiEdzok.Controllers" });
        }
    }
}
