using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TwitterFeed.API
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services				

			// Ensure data returned is what we expect
			var resolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
			resolver.IgnoreSerializableAttribute = true;
			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = resolver;

			var cors = new EnableCorsAttribute("*", "*", "*");
			config.EnableCors(cors);

			//remove xml formatter
			config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
