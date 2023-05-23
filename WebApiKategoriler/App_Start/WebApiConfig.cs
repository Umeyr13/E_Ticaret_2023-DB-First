using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApiKategoriler
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            //GELEN SONUCU JSON DATAYA ÇEVİRMEK İÇİN

            config.Formatters.Remove(config.Formatters.XmlFormatter);


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
