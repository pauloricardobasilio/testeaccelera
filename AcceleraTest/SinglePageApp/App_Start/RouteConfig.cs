using System.Web.Mvc;
using System.Web.Routing;
using LowercaseRoutesMVC;

namespace SinglePageApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRouteLowercase(
                name: "ApplicationStart",
                url: "{controller}/{action}",
                defaults: new { controller = "Application", action = "Start" }
            );
        }
    }
}