using Newtonsoft.Json.Serialization;
using Ninject;
using System.Web.Http;
using System.Web.Http.Cors;
using WebServiceMobileFichaje.Authorization.ActionFilter;
using WebServiceMobileFichaje.Authorization.Service;
using WebServiceMobileFichaje.Domain.Services.Business;
using WebServiceMobileFichaje.Filter;

namespace WebServiceMobileFichaje
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Filters.Add(new ValidateViewModelAttribute());
            config.Filters.Add(new ApiExceptionFilterAttribute());

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}
