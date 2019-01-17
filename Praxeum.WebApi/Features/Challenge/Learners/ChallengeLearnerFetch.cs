using System;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Challenges.Learners
{
    public class ChallengeLearnerFetch
    {
        [SwaggerExclude]
        public Guid LearnerId { get; set; }

        [SwaggerExclude]
        public Guid ChallengeId { get; internal set; }
    }
}