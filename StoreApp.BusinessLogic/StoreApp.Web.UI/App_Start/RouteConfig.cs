using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StoreApp.Web.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region Shop related route configuration

            routes.MapRoute(
                name: "ShopRoute1",
                url: "Shop",
                defaults: new { controller = "Shop", action = "ProductList", selectedCategory = "All", page = 1 }
            );

            routes.MapRoute(
                    name: "ShopRoute2",
                    url: "Shop/{selectedCategory}",
                    defaults: new { controller = "Shop", action = "ProductList", page = 1 }
                );

            routes.MapRoute(
                name: "ShopRoute3",
                url: "Shop/{selectedCategory}/Page{page}",
                defaults: new { controller = "Shop", action = "ProductList" }
            );

            routes.MapRoute(
                name: "ShopRoute4",
                url: "Shop/Page{page}",
                defaults: new { controller = "Shop", action = "ProductList", selectedCategory = "All" }
            );

            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
