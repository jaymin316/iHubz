using System.Collections.Generic;
using iHubz.Domain.MainModule.ReferenceData;

namespace iHubz.Application.MainModule.State
{
    public interface IStateAppService
    {
        IEnumerable<States> GetAllStates();
    }
}
