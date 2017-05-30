using System.Collections.Generic;
using System.Web.Mvc;
using iHubz.Infrastructure.CrossCutting.Dependency;

namespace iHubz.Web.Filters
{
    public class DependencyResolverFilterAttributeFilterProvider : FilterAttributeFilterProvider
    {
        private readonly IContainer _container;

        public DependencyResolverFilterAttributeFilterProvider(IContainer container)
        {
            _container = container;
        }

        protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext,
            ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetControllerAttributes(controllerContext, actionDescriptor);
            BuildUpAttributes(attributes);

            return attributes;
        }

        protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext,
            ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetActionAttributes(controllerContext, actionDescriptor);
            BuildUpAttributes(attributes);

            return attributes;
        }

        private void BuildUpAttributes(IEnumerable<FilterAttribute> attributes)
        {
            foreach (var attribute in attributes)
            {
                _container.BuildUp(attribute.GetType(), attribute);
            }
        }
    }
}