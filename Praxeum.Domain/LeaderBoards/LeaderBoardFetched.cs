using Praxeum.Data;
using Praxeum.Domain.Learners;
using System;
using System.Collections.Generic;

namespace Praxeum.Domain.LeaderBoards
{
    public class LeaderBoardFetched
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int NumberOfLearners { get; set; }

        public ICollection<LearnerFetched> Learners { get; set; }

        public DateTime CreatedOn { get; set; }

        public LeaderBoardFetched()
        {
            this.Learners = new List<LearnerFetched>();
        }
    }
}