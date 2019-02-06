using Praxeum.Data;
using Praxeum.Domain.Contests.Learners;
using System.Collections.Generic;

namespace Praxeum.Domain.Contests
{
    public class ContestFetched : Contest
    {
        public ICollection<ContestLearnerFetched> Learners { get; set; }

        public ContestFetched()
        {
            this.Learners = new List<ContestLearnerFetched>();
        }
    }
}