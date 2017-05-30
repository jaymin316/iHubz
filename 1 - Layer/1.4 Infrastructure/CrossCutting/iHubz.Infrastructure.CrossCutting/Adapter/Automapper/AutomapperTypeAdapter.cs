using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace iHubz.Infrastructure.CrossCutting.Adapter.Automapper
{
    /// <summary>
    ///     Automapper type adapter implementation
    /// </summary>
    public class AutomapperTypeAdapter
        : ITypeAdapter
    {
        private void Map(object destination, params object[] sources)
        {
            if (!sources.Any())
            {
                return;
            }

            var destinationType = destination.GetType();

            foreach (var source in sources)
            {
                var sourceType = source.GetType();
                Mapper.Map(source, destination, sourceType, destinationType);
            }
        }

        private TTarget Map<TTarget>(object source) where TTarget : class
        {
            var destinationType = typeof (TTarget);
            var sourceType = source.GetType();

            var mappingResult = Mapper.Map(source, sourceType, destinationType);

            return mappingResult as TTarget;
        }

        /// <summary>
        ///     Build mapping options
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private IMappingOperationOptions BuildMappingOptions(IMappingOperationOptions opt,
            Dictionary<string, object> options)
        {
            if (options != null)
            {
                foreach (var optionName in options.Keys)
                {
                    opt.Items.Add(optionName, options[optionName]);
                }
            }
            return opt;
        }

        #region ITypeAdapter Members

        /// <summary>
        ///     <see cref="ITypeAdapter" />
        /// </summary>
        /// <typeparam name="TSource">
        ///     <see cref="ITypeAdapter" />
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     <see cref="ITypeAdapter" />
        /// </typeparam>
        /// <param name="source">
        ///     <see cref="ITypeAdapter" />
        /// </param>
        /// <returns>
        ///     <see cref="ITypeAdapter" />
        /// </returns>
        public TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class, new()
        {
            return Mapper.Map<TSource, TTarget>(source);
        }

        /// <summary>
        ///     Adapt a source object to a dest object with custom options
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public TTarget Adapt<TSource, TTarget>(TSource source, Dictionary<string, object> options)
            where TSource : class
            where TTarget : class
        {
            return Mapper.Map<TSource, TTarget>(source, opt => { BuildMappingOptions(opt, options); });
        }

        public TTarget Adapt<TSource, TTarget>(TSource source, Dictionary<string, object> options,
            Action<TSource, TTarget> afterMapAction)
            where TSource : class
            where TTarget : class
        {
            return Mapper.Map<TSource, TTarget>(source, opt =>
            {
                BuildMappingOptions(opt, options);

                opt.AfterMap(afterMapAction);
            });
        }

        /// <summary>
        ///     <see cref="ITypeAdapter" />
        /// </summary>
        /// <typeparam name="TTarget">
        ///     <see cref="ITypeAdapter" />
        /// </typeparam>
        /// <param name="source">
        ///     <see cref="ITypeAdapter" />
        /// </param>
        /// <returns>
        ///     <see cref="ITypeAdapter" />
        /// </returns>
        public TTarget Adapt<TTarget>(object source) where TTarget : class, new()
        {
            return Mapper.Map<TTarget>(source);
        }

        /// <summary>
        ///     Adapt source objects to an instance of type <paramref name="TTarget" />
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        public TTarget Adapt<TTarget>(params object[] sources) where TTarget : class, new()
        {
            if (!sources.Any())
            {
                return default(TTarget);
            }

            var initialSource = sources[0];

            var mappingResult = Map<TTarget>(initialSource);

            // Now map the remaining source objects
            if (sources.Count() > 1)
            {
                Map(mappingResult, sources.Skip(1).ToArray());
            }

            return mappingResult;
        }

        /// <summary>
        ///     Adapt source objects to an instance of type <paramref name="TTarget" />
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public TTarget Adapt<TTarget>(object source, Dictionary<string, object> options)
            where TTarget : class, new()
        {
            return Mapper.Map<TTarget>(source, opt => { BuildMappingOptions(opt, options); });
        }

        /// <summary>
        ///     Adapt a source object to a dest object <paramref name="TTarget" />
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Source to adapt</param>
        /// <param name="target">Instance to adapt</param>
        /// <returns><paramref name="source" /> mapped to <typeparamref name="TTarget" /></returns>
        public TTarget Adapt<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
            where TTarget : class
        {
            return Mapper.Map(source, target);
        }

        #endregion
    }
}