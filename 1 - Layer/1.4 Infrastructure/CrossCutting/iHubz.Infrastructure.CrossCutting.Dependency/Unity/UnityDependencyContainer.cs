using System;
using System.Collections.Generic;
using iHubz.Application.MainModule;
using iHubz.Application.MainModule.Company;
using iHubz.Application.MainModule.State;
using iHubz.Domain.Core;
using iHubz.Domain.MainModule.Repositories;
using iHubz.Infrastructure.CrossCutting.Adapter;
using iHubz.Infrastructure.CrossCutting.Adapter.Automapper;
using iHubz.Infrastructure.Data.Core;
using iHubz.Infrastructure.Data.MainModule.Repositories;
using iHubz.Infrastructure.Data.MainModule.UnitOfWork;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace iHubz.Infrastructure.CrossCutting.Dependency.Unity
{
    public sealed class UnityDependencyContainer
        : IContainer
    {
        #region Constants

        private const string ROOTCONTEXT = "RootContext";

        #endregion

        #region Members

        private readonly IDictionary<string, IUnityContainer> _containersDictionary;

        #endregion

        #region Constructor

        public UnityDependencyContainer()
            : this(new UnityContainer())
        {
        }
        public UnityDependencyContainer(IUnityContainer rootContainer)
        {
            _containersDictionary = new Dictionary<string, IUnityContainer>();

            //Create root container
            rootContainer.AddNewExtension<Interception>();
            _containersDictionary.Add(ROOTCONTEXT, rootContainer);

            ConfigureRootContainer(rootContainer);
            //ConfigureRealContainer(realAppContainer);
            //ConfigureFakeContainer(fakeAppContainer);
        }

        #endregion

        #region Private Methods - Register Interfaces here

        /// <summary>
        ///     Configure root container.Register types and life time managers for unity builder process
        /// </summary>
        /// <param name="container">Container to configure</param>
        private void ConfigureRootContainer(IUnityContainer container)
        {
            #region Unit of Work

            container.RegisterType(typeof(MainModuleUnitOfWork), new HierarchicalLifetimeManager());

            container.RegisterType<IQueryableUnitOfWork, MainModuleUnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, MainModuleUnitOfWork>(new HierarchicalLifetimeManager());

            #endregion Unit of Work

            #region Method Interception Policies

            //container.Configure<Interception>()
            //    .AddPolicy("ApplicationServiceDataAuthorizationPolicy")
            //    .AddMatchingRule(new CustomAttributeMatchingRule(typeof(DataAccessAttribute), true));
            //container.Configure<Interception>()
            //    .AddPolicy("ApplicationServiceScheduleAuthorizationPolicy")
            //    .AddMatchingRule(new CustomAttributeMatchingRule(typeof(ScheduleAccessAttribute), true));

            #endregion Method Interception Policies

            #region Repositories

            container.RegisterType<ICompaniesRepository, CompaniesRepository>(new TransientLifetimeManager());
            container.RegisterType<ICompanyPropertiesRepository, CompanyPropertiesRepository>(new TransientLifetimeManager());
            container.RegisterType<IStateRepository, StateRepository>(new TransientLifetimeManager());

            #endregion Repositories

            #region Adapters

            container.RegisterType<ITypeAdapterFactory, AutomapperTypeAdapterFactory>(
                new ContainerControlledLifetimeManager());

            #endregion Adapters

            #region Application Services

            container.RegisterType<ICompanyAppService, CompanyAppService>(new TransientLifetimeManager());
            container.RegisterType<IStateAppService, StateAppService>(new TransientLifetimeManager());
            container.RegisterType<ICompanyImportAppService, CompanyImportAppService>(new TransientLifetimeManager());

            #endregion
        }

        private IUnityContainer GetRootContainer()
        {
            return _containersDictionary[ROOTCONTEXT];
        }

        #endregion

        #region IServiceFactory Members

        /// <summary>
        ///     <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve{TService}" />
        /// </summary>
        /// <typeparam name="TService">
        ///     <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve{TService}" />
        /// </typeparam>
        /// <returns>
        ///     <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve{TService}" />
        /// </returns>
        public TService Resolve<TService>()
        {
            var container = GetRootContainer();
            return container.Resolve<TService>();
        }

        /// <summary>
        ///     <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve" />
        /// </summary>
        /// <param name="type">
        ///     <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve" />
        /// </param>
        /// <returns>
        ///     <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.Resolve" />
        /// </returns>
        public object Resolve(Type type)
        {
            var container = GetRootContainer();
            return container.Resolve(type, null);
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            var container = GetRootContainer();
            return container.ResolveAll(type, null);
        }

        /// <summary>
        ///     <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.RegisterType" />
        /// </summary>
        /// <param name="type">
        ///     <see cref="M:Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.IContainer.RegisterType" />
        /// </param>
        public void RegisterType(Type type)
        {
            var container = GetRootContainer();

            if (container != null)
                container.RegisterType(type, new TransientLifetimeManager());
        }

        public void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            var container = GetRootContainer();

            if (container != null)
                container.RegisterType(typeof(TFrom), typeof(TTo), new TransientLifetimeManager());
        }

        public void BuildUp(Type t, object attribute)
        {
            var container = GetRootContainer();
            container.BuildUp(t, attribute);
        }

        public TContainer GetRootContainer<TContainer>()
        {
            var container = GetRootContainer();
            return (TContainer)container;
        }

        public IChildContainer CreateChildContainer()
        {
            var container = GetRootContainer();
            var childContainer = container.CreateChildContainer();
            return new UnityDependencyChildContainer(childContainer);
        }

        #endregion
    }
}
