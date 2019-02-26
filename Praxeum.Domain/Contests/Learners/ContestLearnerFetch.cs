using System;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerFetch
    {
        [SwaggerExclude]
        public Guid ContestId { get; set; }

        [SwaggerExclude]
        public Guid Id { get; set; }
    }
}