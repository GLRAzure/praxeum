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

        public ContestLearnerFetcher(
            IContestRepository contestRepository)
        {
            _contestRepository =
                contestRepository;
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
                    x => x.Id == contestLearnerFetch.Id);

            if (contestLearner == null)
            {
                throw new NullReferenceException($"Learner {contestLearnerFetch.Id} does not exist.");
            }

            var contestLearnerFetched =
                Mapper.Map(contestLearner, new ContestLearnerFetched());

            return contestLearnerFetched;
        }
    }
}