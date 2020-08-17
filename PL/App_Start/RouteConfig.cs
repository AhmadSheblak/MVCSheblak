using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BLL;
using DAL;
namespace PL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
             MyContext c = new MyContext();
             User check = c.User.SingleOrDefault(x => x.RoleId == 1);
             if (check == null)
             {
                routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "FirstReg", action = "Create", id = UrlParameter.Optional }
             );
             }
             else
             { routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }

              );}
            c.Dispose();
        }
    }
}
