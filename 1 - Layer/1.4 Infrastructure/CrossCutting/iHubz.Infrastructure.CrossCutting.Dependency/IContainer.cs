using System;
using System.Collections.Generic;

namespace iHubz.Infrastructure.CrossCutting.Dependency
{
    public interface IContainer
    {
        TService Resolve<TService>();
        object Resolve(Type type);
        IEnumerable<object> ResolveAll(Type type);
        void RegisterType(Type type);
        void RegisterType<TFrom, TTo>() where TTo : TFrom;
        void BuildUp(Type t, object existing);
        TContainer GetRootContainer<TContainer>();
        IChildContainer CreateChildContainer();
    }
}
