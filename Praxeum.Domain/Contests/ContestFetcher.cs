using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestFetcher : IHandler<ContestFetch, ContestFetched>
    {
        private readonly IContestRepository _contestRepository;
        private readonly ILearnerRepository _learnerRepository;

        public ContestFetcher(
            IContestRepository contestRepository,
            ILearnerRepository learnerRepository)
        {
            _contestRepository =
                contestRepository;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<ContestFetched> ExecuteAsync(
            ContestFetch contestFetch)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestFetch.Id);

            var contestFetched =
                Mapper.Map(contest, new ContestFetched());

            var learners =
                await _learnerRepository.FetchListAsync(
                    contest.Learners.Select(x => x.LearnerId).ToArray());

            foreach (var learner in learners)
            {
                var contestLearner =
                    contestFetched.Learners.Single(x => x.LearnerId == learner.Id);

                contestLearner.UserName = learner.UserName;
                contestLearner.DisplayName = learner.DisplayName;
            }

            return contestFetched;
        }
    }
}