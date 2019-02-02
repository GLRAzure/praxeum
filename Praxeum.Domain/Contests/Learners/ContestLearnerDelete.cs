using System;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerDelete
    {
        [SwaggerExclude]
        public Guid LearnerId { get; set; }

        [SwaggerExclude]
        public Guid ContestId { get; set; }
    }
}