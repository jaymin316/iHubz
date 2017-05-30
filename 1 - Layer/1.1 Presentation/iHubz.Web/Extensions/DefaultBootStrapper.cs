using System;
using iHubz.Infrastructure.CrossCutting.Adapter;
using iHubz.Infrastructure.CrossCutting.Dependency;

namespace iHubz.Web.Extensions
{
    public class DefaultBootStrapper
        : IBootStrapper
    {
        #region Members

        /// <summary>
        ///     Current IoC container
        /// </summary>
        private readonly IContainer _currentContainer;

        #endregion

        #region Constructors

        /// <summary>
        ///     Create a new instance of this boot strapper
        /// </summary>
        /// <param name="container">Default IoC container</param>
        public DefaultBootStrapper(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("serviceFactory");

            _currentContainer = container;
        }

        #endregion

        #region IBootStrapper Members

        public void Boot()
        {
            RegisterDependencies();
            ConfigureFactories();
            //ConfigureSettings();
        }

        #endregion

        #region Private Methods

        private void RegisterDependencies()
        {
        }

        private void ConfigureFactories()
        {
            //var configFilePath = HttpContext.Current.Server.MapPath("~/log4net.config");
            //LoggerFactory.SetCurrent(new Log4NetLogFactory(), configFilePath);

            //EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());
            //SerializerFactory.SetCurrent(new ProtoBufSerializerFactory());

            //CacheManagerFactory.SetCurrent(new MemoryCacheManagerFactory());

            //var resourcesPath = HttpContext.Current.Server.MapPath("~/resources");
            //TranslatorFactory.SetCurrent(new LanguageTranslatorFactory(_currentContainer), resourcesPath);

            var typeAdapterFactory = _currentContainer.Resolve<ITypeAdapterFactory>();
            TypeAdapterFactory.SetCurrent(typeAdapterFactory);

            //ExcelManagerFactory.SetCurrent(new AsposeExcelManagerFactory());
            //PdfManagerFactory.SetCurrent(new AsposePdfManagerFactory());
            //ArchiveManagerFactory.SetCurrent(new ZipArchiveManagerFactory());
        }

        //private void AddSetting(SystemProperty property)
        //{
        //    if (property == null)
        //        return;

        //    SettingsHelper.AddSetting(property.PropertyName, property.PropertyValue);
        //}

        #endregion
    }
}