using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

namespace Praxeum.WebApi.Data
{
    public class LeaderBoard
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public ICollection<LeaderBoardLearner> Learners { get; set; }

        public DateTime CreatedOn { get; set; }

        public LeaderBoard()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsActive = true;
            this.Learners = new List<LeaderBoardLearner>();
        }
    }
}
