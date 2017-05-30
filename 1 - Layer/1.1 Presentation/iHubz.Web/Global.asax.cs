using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using iHubz.Infrastructure.CrossCutting.Dependency;
using iHubz.Web.Extensions;
using iHubz.Web.Filters;

namespace iHubz.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Perform application configuration
            IBootStrapper bootStrapper = new DefaultBootStrapper(DependencyFactory.Instance.CurrentContainer);
            bootStrapper.Boot();

            // Set up dependency injection resolver
            DependencyResolver.SetResolver(DependencyFactory.Instance.CurrentContainer.ToDependencyResolver());

            // remove normal MVC filter attribute filter providers and enable our custom ones which allow for dependency injection in action filters
            // that can still be invoked using method decoration attributes
            IFilterProvider filterProvider = FilterProviders.Providers.Single(p => p is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(filterProvider);

            var dependencyResolverFilterAttributeFilterProvider = new DependencyResolverFilterAttributeFilterProvider(DependencyFactory.Instance.CurrentContainer);
            FilterProviders.Providers.Add(dependencyResolverFilterAttributeFilterProvider);
        }
    }
}
