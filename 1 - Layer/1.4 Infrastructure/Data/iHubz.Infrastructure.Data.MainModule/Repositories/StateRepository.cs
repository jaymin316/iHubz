using iHubz.Domain.MainModule.ReferenceData;
using iHubz.Domain.MainModule.Repositories;
using iHubz.Infrastructure.Data.Core;

namespace iHubz.Infrastructure.Data.MainModule.Repositories
{
    public class StateRepository : Repository<States, int>, IStateRepository
    {
        public StateRepository(IQueryableUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
