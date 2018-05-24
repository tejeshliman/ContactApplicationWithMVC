using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Autofac;
using ContactApplicationViewLayer.Models;
using System.Reflection;

namespace ContactApplicationViewLayer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var builder = RegisterDependencies();
            var container = builder.Build();
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
        }

        private static ContainerBuilder RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            var currentAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterType<Notifier>().AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterType<NotifierFilterAttribute>().AsActionFilterFor<Controller>().PropertiesAutowired();

            builder.RegisterFilterProvider();
            builder.RegisterControllers(currentAssembly);

            return builder;
        }
    }
}
