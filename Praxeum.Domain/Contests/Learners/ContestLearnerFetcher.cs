using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerFetcher : IHandler<ContestLearnerFetch, ContestLearnerFetched>
    {
        private readonly IContestRepository _contestRepository;
        private readonly ILearnerRepository _learnerRepository;

        public ContestLearnerFetcher(
            IContestRepository contestRepository,
            ILearnerRepository learnerRepository)
        {
            _contestRepository =
                contestRepository;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<ContestLearnerFetched> ExecuteAsync(
            ContestLearnerFetch contestLearnerFetch)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestLearnerFetch.ContestId);

            if (contest == null)
            {
                throw new NullReferenceException($"Contest {contestLearnerFetch.ContestId} does not exist.");
            }

            var contestLearner =
                contest.Learners.SingleOrDefault(
                    x => x.Id == contestLearnerFetch.LearnerId);

            if (contestLearner == null)
            {
                throw new NullReferenceException($"Learner {contestLearnerFetch.LearnerId} does not exist.");
            }

            var learner =
                await _learnerRepository.FetchByIdAsync(
                    contestLearnerFetch.LearnerId);

            var contestLearnerFetched =
                Mapper.Map(learner, new ContestLearnerFetched());

            return contestLearnerFetched;
        }
    }
}