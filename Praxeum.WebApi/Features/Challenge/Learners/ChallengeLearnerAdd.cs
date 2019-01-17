using System;
using System.ComponentModel.DataAnnotations;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Challenges.Learners
{
    public class ChallengeLearnerAdd
    {
        [SwaggerExclude]
        public Guid Id { get; set; }

        [SwaggerExclude]
        public Guid ChallengeId { get; set; }

        [Required]
        public Guid LearnerId { get; set; }


        public ChallengeLearnerAdd()
        {
            this.Id = Guid.NewGuid();
        }
    }
}