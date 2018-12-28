using Praxeum.WebApi.Data;
using System;
using System.Collections.Generic;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardFetchedById 
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public ICollection<LeaderBoardLearner> Learners { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}