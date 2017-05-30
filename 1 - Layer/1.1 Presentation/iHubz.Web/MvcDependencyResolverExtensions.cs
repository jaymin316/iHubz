using System.Web.Mvc;
using iHubz.Infrastructure.CrossCutting.Dependency;
using Microsoft.Practices.Unity;
using Unity.Mvc3;

namespace iHubz.Web
{
    public static class MvcDependencyResolverExtensions
    {
        public static IDependencyResolver ToDependencyResolver(this IContainer container)
        {
            var rootContainer = container.GetRootContainer<IUnityContainer>();
            return new UnityDependencyResolver(rootContainer);
        }
    }
}