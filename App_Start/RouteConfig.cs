using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace press_agency_asp_webapp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           
            routes.MapRoute(
                name: "Viewer",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Viewer", action = "Wall", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Editor",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Editor", action = "CreatePost", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                   name: "Admin",
                   url: "{controller}/{action}/{id}",
                   defaults: new { controller = "Admin", action = "Posts", id = UrlParameter.Optional }
               );



        }
    }
}
