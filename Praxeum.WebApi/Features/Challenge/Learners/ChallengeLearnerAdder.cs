using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.WebApi.Features.Challenges.Learners
{
    public class ChallengeLearnerAdder : IHandler<ChallengeLearnerAdd, ChallengeLearnerAdded>
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly ILearnerRepository _learnerRepository;

        public ChallengeLearnerAdder(
            IChallengeRepository challengeRepository,
            ILearnerRepository learnerRepository)
        {
            _challengeRepository =
                challengeRepository;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<ChallengeLearnerAdded> ExecuteAsync(
            ChallengeLearnerAdd challengeLearnerAdd)
        {
            var challenge =
                await _challengeRepository.FetchByIdAsync(
                    challengeLearnerAdd.ChallengeId);

            if (challenge == null)
            {
                throw new NullReferenceException($"Challenge {challengeLearnerAdd.ChallengeId} does not exist.");
            }

            var challengeLearner =
                challenge.Learners.SingleOrDefault(x => x.Id == challengeLearnerAdd.LearnerId);

            if (challengeLearner != null)
            {
                throw new ArgumentOutOfRangeException($"Learner {challengeLearnerAdd.LearnerId} already exists.");
            }

            challenge.Learners.Add(
                new ChallengeLearner
                {
                    Id = challengeLearnerAdd.LearnerId
                });

            challenge =
                await _challengeRepository.UpdateByIdAsync(
                    challengeLearnerAdd.ChallengeId,
                    challenge);

            var learner =
                await _learnerRepository.FetchByIdAsync(
                    challengeLearnerAdd.LearnerId);

            var challengeLearnerAdded =
                Mapper.Map(learner, new ChallengeLearnerAdded());

            return challengeLearnerAdded;
        }
    }
}