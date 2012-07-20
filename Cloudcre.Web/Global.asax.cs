using System.Web.Mvc;
using System.Web.Routing;
using Cloudcre.Service.Property.Mapping;
using Cloudcre.Web.Filters;
using Cloudcre.Web.Models;
using LowercaseRoutesMVC;

namespace Cloudcre.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // uncomment next two lines to force login and for https use
            //filters.Add(new AuthorizeAndTransferAttribute());
            //filters.Add(new RequireHttpsExceptLocalhostAttribute());
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRouteLowercase(
                "Default",
                "", // Only matches the empty URL (i.e. ~/)
                new { controller = "Search", action = "Index" }
            );

            routes.MapRouteLowercase(
                "Queue",
                "Queue/",
                new { controller = "Queue", action = "Index" }
            );

            //routes.MapRouteLowercase("Report", "{controller}/Report/{action}");

            routes.MapRouteLowercase(
                "SlugsAfterId",
                "{controller}/{action}/{id}/{slug}",
                new { action = "Edit", slug = "" }
            );

            routes.MapRouteLowercase(null, "{controller}/{action}");
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            Mapping.BootStrapper.ConfigureAutoMapper();
            BootStrapper.ConfigureAutoMapper();

            ModelBinders.Binders.Add(typeof(PropertySearchRequestViewModel), new JsonModelBinder());
            //ModelBinders.Binders.Add(typeof(Model.Queue.Queue), new QueueModelBinder());

            //LoggingFactory.InitializeLogFactory(ServiceLocator.Current.GetInstance<ILogger>());
            //LoggingFactory.GetLogger().Log("Application Started");
        }
    }
}