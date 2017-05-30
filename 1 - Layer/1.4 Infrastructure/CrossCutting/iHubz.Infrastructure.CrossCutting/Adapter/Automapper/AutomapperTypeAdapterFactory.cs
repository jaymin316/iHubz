using AutoMapper;
using System;
using System.Linq;

namespace iHubz.Infrastructure.CrossCutting.Adapter.Automapper
{
    /// <summary>
    /// Factory to create auto mapper factory
    /// </summary>
    public class AutomapperTypeAdapterFactory
        : ITypeAdapterFactory
    {
        static ITypeAdapter _currentAdapter;

        #region Constructor

        /// <summary>
        /// Create a new Automapper type adapter factory
        /// </summary>
        public AutomapperTypeAdapterFactory()
        {
            //scan all assemblies finding Automapper Profile
            var profiles = AppDomain.CurrentDomain
                                    .GetAssemblies()
                                    .Where(t => t.FullName.StartsWith("iHubz."))
                                    .SelectMany(a => a.GetTypes())
                                    .Where(t => t.BaseType == typeof(Profile));

            Mapper.Initialize(cfg =>
            {
                foreach (var item in profiles)
                {
                    if (item.FullName != "AutoMapper.SelfProfiler`2")
                        cfg.AddProfile(Activator.CreateInstance(item) as Profile);
                }
            });
        }

        #endregion

        #region ITypeAdapterFactory Members

        public ITypeAdapter Create()
        {
            if (_currentAdapter == null)
            {
                _currentAdapter = new AutomapperTypeAdapter();
            }
            return _currentAdapter;
        }

        #endregion
    }
}
