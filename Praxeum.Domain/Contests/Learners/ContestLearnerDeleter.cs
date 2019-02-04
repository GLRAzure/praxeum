using System;
using System.Linq;
using System.Threading.Tasks;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerDeleter : IHandler<ContestLearnerDelete, ContestLearnerDeleted>
    {
        private readonly IContestRepository _contestRepository;
        private readonly ILearnerRepository _learnerRepository;

        public ContestLearnerDeleter(
            IContestRepository contestRepository,
            ILearnerRepository learnerRepository)
        {
            _contestRepository =
                contestRepository;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<ContestLearnerDeleted> ExecuteAsync(
            ContestLearnerDelete contestLearnerDelete)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestLearnerDelete.ContestId);

            if (contest == null)
            {
                throw new NullReferenceException($"Contest {contestLearnerDelete.ContestId} does not exist.");
            }

            var contestLearner =
                contest.Learners.SingleOrDefault(x => x.LearnerId == contestLearnerDelete.LearnerId);

            if (contestLearner == null)
            {
                throw new ArgumentOutOfRangeException($"Learner {contestLearnerDelete.LearnerId} does not exist.");
            }

            contest.Learners.Remove(
                contestLearner);

            contest =
                await _contestRepository.UpdateByIdAsync(
                    contestLearnerDelete.ContestId,
                    contest);

            var learner =
                await _learnerRepository.FetchByIdAsync(
                    contestLearnerDelete.LearnerId);

            var contestLearnerDeleted =
                 new ContestLearnerDeleted();

            return contestLearnerDeleted;
        }
    }
}