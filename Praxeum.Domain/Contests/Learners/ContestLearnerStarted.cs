using System;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerStarted
    {
        public Guid ContestId { get; set; }
        public Guid Id { get; set; }
    }
}