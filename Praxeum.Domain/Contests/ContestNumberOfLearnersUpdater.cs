using System.Linq;
using System.Threading.Tasks;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestNumberOfLearnersUpdater : IHandler<ContestNumberOfLearnersUpdate, ContestNumberOfLearnersUpdated>
    {
        private readonly IContestRepository _contestRepository;
        private readonly IContestLearnerRepository _contestLearnerRepository;

        public ContestNumberOfLearnersUpdater(
            IContestRepository contestRepository,
            IContestLearnerRepository contestLearnerRepository)
        {
            _contestRepository =
                contestRepository;
            _contestLearnerRepository =
                contestLearnerRepository;
        }

        public async Task<ContestNumberOfLearnersUpdated> ExecuteAsync(
            ContestNumberOfLearnersUpdate contestNumberOfLearnersUpdate)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestNumberOfLearnersUpdate.ContestId);

            var contestLearners =
                await _contestLearnerRepository.FetchListAsync(
                    contestNumberOfLearnersUpdate.ContestId);

            contest.NumberOfLearners =
                contestLearners.Count();

            await _contestRepository.UpdateByIdAsync(
                contestNumberOfLearnersUpdate.ContestId,
                contest);

            var contestNumberOfLearnersUpdated =
                new ContestNumberOfLearnersUpdated
                {
                    NumberOfLearners = contestLearners.Count()
                };

            return contestNumberOfLearnersUpdated;
        }
    }
}