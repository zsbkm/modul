using DotNetNuke.Web;
using DotNetNuke.Web.Api;
using DotNetNuke.Web.Mvc.Routing;
using System.Diagnostics;
using IMapRoute = DotNetNuke.Web.Api.IMapRoute;

namespace SzemelyiEdzokSzemelyiEdzok.Components
{
    public class RouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute(
                "SzemelyiEdzok",
                "default",
                "{controller}/{action}",
                new string[] { "SzemelyiEdzokSzemelyiEdzok.Controllers" });
        }
    }
}