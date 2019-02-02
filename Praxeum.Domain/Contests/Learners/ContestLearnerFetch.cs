using System;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerFetch
    {
        [SwaggerExclude]
        public Guid LearnerId { get; set; }

        [SwaggerExclude]
        public Guid ContestId { get; internal set; }
    }
}