using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WEbApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // o customers\
            // o customers\{ customerid}
            // o customers\{ customerid}\orders
            // o customers\{ customerid}\orders\{ orderid}

            config.MapHttpAttributeRoutes();           

            config.Routes.MapHttpRoute("API", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute("API Area", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}
