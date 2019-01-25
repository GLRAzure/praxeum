using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.WebApi.Features.Challenges.Learners
{
    public class ChallengeLearnerFetcher : IHandler<ChallengeLearnerFetch, ChallengeLearnerFetched>
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly ILearnerRepository _learnerRepository;

        public ChallengeLearnerFetcher(
            IChallengeRepository challengeRepository,
            ILearnerRepository learnerRepository)
        {
            _challengeRepository =
                challengeRepository;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<ChallengeLearnerFetched> ExecuteAsync(
            ChallengeLearnerFetch challengeLearnerFetch)
        {
            var challenge =
                await _challengeRepository.FetchByIdAsync(
                    challengeLearnerFetch.ChallengeId);

            if (challenge == null)
            {
                throw new NullReferenceException($"Challenge {challengeLearnerFetch.ChallengeId} does not exist.");
            }

            var challengeLearner =
                challenge.Learners.SingleOrDefault(
                    x => x.Id == challengeLearnerFetch.LearnerId);

            if (challengeLearner == null)
            {
                throw new NullReferenceException($"Learner {challengeLearnerFetch.LearnerId} does not exist.");
            }

            var learner =
                await _learnerRepository.FetchByIdAsync(
                    challengeLearnerFetch.LearnerId);

            var challengeLearnerFetched =
                Mapper.Map(learner, new ChallengeLearnerFetched());

            return challengeLearnerFetched;
        }
    }
}