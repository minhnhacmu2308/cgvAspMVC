using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CGV
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "home user",
               url: "user/home",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "Detail film",
              url: "film/detail/{id}",
              defaults: new { controller = "Film", action = "DetailFilm", id = UrlParameter.Optional }
          );
            routes.MapRoute(
            name: "Detail promotion",
            url: "post/detailpromotion/{id}",
            defaults: new { controller = "Post", action = "DetailPromotion", id = UrlParameter.Optional }
        );
            routes.MapRoute(
             name: "Search film",
             url: "film/search/{keySearch}",
             defaults: new { controller = "Film", action = "SearchFilm", id = UrlParameter.Optional }
         );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "IndexUser", id = UrlParameter.Optional }
            );
        }
    }
}
