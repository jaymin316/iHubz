using System;

namespace iHubz.Infrastructure.CrossCutting.Dependency
{
    public interface IChildContainer : IDisposable
    {
        TService Resolve<TService>();
    }
}
