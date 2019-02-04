using System;
using System.Collections.Generic;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerListAdded
    {
        public Guid ContestId { get; set; }

        public IList<ContestLearnerAdded> LearnersAdded { get; set; }

        public IList<string> LearnersNotAdded { get; set; }

        public ContestLearnerListAdded()
        {
            this.LearnersAdded = new List<ContestLearnerAdded>();
            this.LearnersNotAdded = new List<string>();
        }
    }
}