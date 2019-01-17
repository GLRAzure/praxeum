using System;
using System.ComponentModel.DataAnnotations;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Challenges.Learners
{
    public class ChallengeLearnerDelete
    {
        [SwaggerExclude]
        public Guid LearnerId { get; set; }

        [SwaggerExclude]
        public Guid ChallengeId { get; set; }
    }
}