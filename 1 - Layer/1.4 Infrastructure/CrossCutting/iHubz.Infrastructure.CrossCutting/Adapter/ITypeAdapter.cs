using System;
using System.Collections.Generic;

namespace iHubz.Infrastructure.CrossCutting.Adapter
{
    /// <summary>
    /// Base contract for map dto to aggregate or aggregate to dto.
    /// <remarks>
    /// This is a  contract for work with "auto" mappers ( automapper,emitmapper,valueinjecter...)
    /// or adhoc mappers
    /// </remarks>
    /// </summary>
    public interface ITypeAdapter
    {
        /// <summary>
        /// Adapt a source object to an instance of type <paramref name="TTarget"/>
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <returns><paramref name="source"/> mapped to <typeparamref name="TTarget"/></returns>
        TTarget Adapt<TSource, TTarget>(TSource source)
            where TTarget : class,new()
            where TSource : class;

        /// <summary>
        /// Adapt a source object to a dest object with custom options
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        TTarget Adapt<TSource, TTarget>(TSource source, Dictionary<string, object> options)
            where TSource : class
            where TTarget : class;

        TTarget Adapt<TSource, TTarget>(TSource source, Dictionary<string, object> options,
            Action<TSource, TTarget> afterMapAction) where TSource : class
            where TTarget : class;
        /// <summary>
        /// Adapt a source object to an instnace of type <paramref name="TTarget"/>
        /// </summary>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <returns><paramref name="source"/> mapped to <typeparamref name="TTarget"/></returns>
        TTarget Adapt<TTarget>(object source)
            where TTarget : class,new();

        /// <summary>
        /// Adapt source objects to an instance of type <paramref name="TTarget"/>
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        TTarget Adapt<TTarget>(params object[] sources)
            where TTarget : class, new();

        /// <summary>
        /// Adapt source objects to an instance of type <paramref name="TTarget"/>
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="sources"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        TTarget Adapt<TTarget>(object sources, Dictionary<string, object> options)
            where TTarget : class, new();

        /// <summary>
        /// Adapt a source object to a dest object <paramref name="TTarget"/>
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Source to adapt</param>
        /// <param name="target">Instance to adapt</param>
        /// <returns><paramref name="source"/> mapped to <typeparamref name="TTarget"/></returns>
        TTarget Adapt<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
            where TTarget : class;                
    }
}
