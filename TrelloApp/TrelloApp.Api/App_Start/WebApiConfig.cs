using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace TrelloApp.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config) 
        {
            //Configuracion para el serializador de JSON
            //Este se encarga de convertir nuestros objetos c# a notacion JSON
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //Configuracion la serializacion de JSON
            //No guarda referencias a otros objetos de JSON
            settings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            //No utilices ningun formato especial
            settings.Formatting = Formatting.None;
            //Ignora valores nulos
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Habilita CORS en la API
            config.EnableCors();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
