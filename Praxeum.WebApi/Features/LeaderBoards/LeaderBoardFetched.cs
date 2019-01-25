using Praxeum.Data;
using System;
using System.Collections.Generic;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardFetched
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Learner> Learners { get; set; }

        public DateTime CreatedOn { get; set; }

        public LeaderBoardFetched()
        {
            this.Learners = new List<Learner>();
        }
    }
}