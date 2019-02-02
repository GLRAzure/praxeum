using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerAdder : IHandler<ContestLearnerAdd, ContestLearnerAdded>
    {
        private readonly IContestRepository _contestRepository;
        private readonly ILearnerRepository _learnerRepository;

        public ContestLearnerAdder(
            IContestRepository contestRepository,
            ILearnerRepository learnerRepository)
        {
            _contestRepository =
                contestRepository;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<ContestLearnerAdded> ExecuteAsync(
            ContestLearnerAdd contestLearnerAdd)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestLearnerAdd.ContestId);

            if (contest == null)
            {
                throw new NullReferenceException($"Contest {contestLearnerAdd.ContestId} does not exist.");
            }

            var contestLearner =
                contest.Learners.SingleOrDefault(x => x.Id == contestLearnerAdd.LearnerId);

            if (contestLearner != null)
            {
                throw new ArgumentOutOfRangeException($"Learner {contestLearnerAdd.LearnerId} already exists.");
            }

            contest.Learners.Add(
                new ContestLearner
                {
                    Id = contestLearnerAdd.LearnerId
                });

            contest =
                await _contestRepository.UpdateByIdAsync(
                    contestLearnerAdd.ContestId,
                    contest);

            var learner =
                await _learnerRepository.FetchByIdAsync(
                    contestLearnerAdd.LearnerId);

            var contestLearnerAdded =
                Mapper.Map(learner, new ContestLearnerAdded());

            return contestLearnerAdded;
        }
    }
}