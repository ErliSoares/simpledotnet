using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Example.Project.Environment;
using Simple;
using Example.Project.Web.Controllers;
using MvcContrib.SimplyRestful;
using Example.Project.Web.Helpers;
using Simple.Entities;
using Simple.Reflection;
using Simple.Web.Mvc;

namespace Example.Project.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default",
                 "{controller}/{action}/{id}",
                 new { controller = "Home", action = "Index", id = 0 }
             );
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            DefaultModelBinder.ResourceClassKey = "ValidationMessages";
            ModelBinders.Binders.DefaultBinder = new EntityModelBinder();

            ModelValidatorProviders.Providers.Clear();
            new Configurator().StartServer<ServerStarter>();
        }
    }
}