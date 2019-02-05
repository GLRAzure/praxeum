using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerDeleter : IHandler<ContestLearnerDelete, ContestLearnerDeleted>
    {
        private readonly IContestRepository _contestRepository;

        public ContestLearnerDeleter(
            IContestRepository contestRepository)
        {
            _contestRepository =
                contestRepository;
        }

        public async Task<ContestLearnerDeleted> ExecuteAsync(
            ContestLearnerDelete contestLearnerDelete)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestLearnerDelete.ContestId);

            if (contest == null)
            {
                throw new NullReferenceException($"Contest {contestLearnerDelete.ContestId} not found.");
            }

            var contestLearner =
                contest.Learners.SingleOrDefault(
                    x => x.Id == contestLearnerDelete.Id);

            if (contestLearner == null)
            {
                throw new ArgumentOutOfRangeException($"Learner {contestLearnerDelete.Id} not found.");
            }

            contest.Learners.Remove(
                contestLearner);

            contest =
                await _contestRepository.UpdateByIdAsync(
                    contestLearnerDelete.ContestId,
                    contest);

            var contestLearnerDeleted =
                Mapper.Map(contestLearner, new ContestLearnerDeleted());

            return contestLearnerDeleted;
        }
    }
}