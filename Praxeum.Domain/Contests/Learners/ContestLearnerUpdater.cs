using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerUpdater : IHandler<ContestLearnerUpdate, ContestLearnerUpdated>
    {
        private readonly IMapper _mapper;
        private readonly IContestLearnerRepository _contestLearnerRepository;

        public ContestLearnerUpdater(
            IMapper mapper,
            IContestLearnerRepository contestLearnerRepository)
        {
            _mapper =
                mapper;
            _contestLearnerRepository =
                contestLearnerRepository;
        }

        public async Task<ContestLearnerUpdated> ExecuteAsync(
            ContestLearnerUpdate contestLearnerUpdate)
        {
            var contestLearner =
                await _contestLearnerRepository.FetchByIdAsync(
                    contestLearnerUpdate.ContestId,
                    contestLearnerUpdate.Id);

            contestLearner.UserName = 
                contestLearnerUpdate.UserName;
            contestLearner.StartValue = 
                contestLearnerUpdate.StartValue;

            contestLearner =
                await _contestLearnerRepository.UpdateByIdAsync(
                    contestLearnerUpdate.ContestId,
                    contestLearnerUpdate.Id,
                    contestLearner);

            var contestLearnerUpdated =
                _mapper.Map(contestLearner, new ContestLearnerUpdated());

            return contestLearnerUpdated;
        }
    }
}