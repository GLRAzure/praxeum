using System;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Contests.Learners
{
    public class ContestLearnerFetch
    {
        [SwaggerExclude]
        public Guid LearnerId { get; set; }

        [SwaggerExclude]
        public Guid ContestId { get; internal set; }
    }
}