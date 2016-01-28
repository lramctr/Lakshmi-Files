using System.Web.Mvc;
using System.Web.Routing;

namespace Nps.Gis.Web
{
    /// <summary>
    /// Route Configuration
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Register Routes
        /// </summary>
        /// <param name="routes">Route Collection</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // Ignore Pivot Server extensions
            routes.IgnoreRoute("{resource}.cxml");
            routes.IgnoreRoute("{resource}.dzc");
            routes.IgnoreRoute("{resource}.dzi");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
