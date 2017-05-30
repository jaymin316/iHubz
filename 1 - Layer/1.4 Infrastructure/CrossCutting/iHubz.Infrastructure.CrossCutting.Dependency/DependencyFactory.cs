using iHubz.Infrastructure.CrossCutting.Dependency.Unity;

namespace iHubz.Infrastructure.CrossCutting.Dependency
{
    public sealed class DependencyFactory
    {
        #region Singleton

        static readonly DependencyFactory instance = new DependencyFactory();

        /// <summary>
        /// Get singleton instance of DependencyFactory
        /// </summary>
        public static DependencyFactory Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        #region Members

        IContainer _currentContainer;

        public IContainer CurrentContainer
        {
            get
            {
                return _currentContainer;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Only for singleton pattern, remove before field init IL anotation
        /// </summary>
        static DependencyFactory() { }
        DependencyFactory()
        {
            _currentContainer = new UnityDependencyContainer();
        }

        #endregion
    }
}
