using System;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerProgressUpdate
    {
        public Guid ContestId { get; set; }

        public Guid Id { get; set; }
    }
}