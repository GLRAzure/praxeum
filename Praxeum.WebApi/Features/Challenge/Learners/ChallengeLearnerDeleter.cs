using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.Challenges.Learners
{
    public class ChallengeLearnerDeleter : IHandler<ChallengeLearnerDelete, ChallengeLearnerDeleted>
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly ILearnerRepository _learnerRepository;

        public ChallengeLearnerDeleter(
            IChallengeRepository challengeRepository,
            ILearnerRepository learnerRepository)
        {
            _challengeRepository =
                challengeRepository;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<ChallengeLearnerDeleted> ExecuteAsync(
            ChallengeLearnerDelete challengeLearnerDelete)
        {
            var challenge =
                await _challengeRepository.FetchByIdAsync(
                    challengeLearnerDelete.ChallengeId);

            if (challenge == null)
            {
                throw new NullReferenceException($"Challenge {challengeLearnerDelete.ChallengeId} does not exist.");
            }

            var challengeLearner =
                challenge.Learners.SingleOrDefault(x => x.Id == challengeLearnerDelete.LearnerId);

            if (challengeLearner == null)
            {
                throw new ArgumentOutOfRangeException($"Learner {challengeLearnerDelete.LearnerId} does not exist.");
            }

            challenge.Learners.Remove(
                challengeLearner);

            challenge =
                await _challengeRepository.UpdateByIdAsync(
                    challengeLearnerDelete.ChallengeId,
                    challenge);

            var learner =
                await _learnerRepository.FetchByIdAsync(
                    challengeLearnerDelete.LearnerId);

            var challengeLearnerDeleted =
                 new ChallengeLearnerDeleted();

            return challengeLearnerDeleted;
        }
    }
}