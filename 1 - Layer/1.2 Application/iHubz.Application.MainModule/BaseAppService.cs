using iHubz.Domain.Core;

namespace iHubz.Application.MainModule
{
    public abstract class BaseAppService
    {
        protected IUnitOfWork UnitOfWork { get; private set; }

        protected BaseAppService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

    }
}
