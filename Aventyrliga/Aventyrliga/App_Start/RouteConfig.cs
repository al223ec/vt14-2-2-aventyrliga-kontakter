using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Aventyrliga.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Start", "Äventyrligakontakter", "~/Default.aspx");
            routes.MapPageRoute("Error", "serverfel", "~/Error.html");
        }
    }
}