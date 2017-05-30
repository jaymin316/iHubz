using System;
using System.Collections.Generic;
using iHubz.Domain.Core;
using iHubz.Domain.MainModule.ReferenceData;
using iHubz.Domain.MainModule.Repositories;

namespace iHubz.Application.MainModule.State
{
    public class StateAppService : BaseAppService, IStateAppService
    {
        public StateAppService(IUnitOfWork unitOfWork, IStateRepository stateRepository) : base(unitOfWork)
        {
            if (stateRepository == null)
                throw new ArgumentNullException("stateRepository");

            _stateRepository = stateRepository;
        }

        #region Members

        private readonly IStateRepository _stateRepository;

        #endregion

        public IEnumerable<States> GetAllStates()
        {
            return _stateRepository.GetAll();
        }
    }
}
