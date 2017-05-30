using Microsoft.Practices.Unity;

namespace iHubz.Infrastructure.CrossCutting.Dependency.Unity
{
    sealed class UnityDependencyChildContainer : IChildContainer
    {
        private IUnityContainer _container;

        public UnityDependencyChildContainer(IUnityContainer container)
        {
            _container = container;
        }

        public TService Resolve<TService>()
        {
            return _container.Resolve<TService>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
