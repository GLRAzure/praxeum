using Praxeum.Data;
using System;
using System.Collections.Generic;

namespace Praxeum.Domain.Contests
{
    public class ContestFetched
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool HasPrizes { get; set; }

        public string Prizes { get; set; }

        public string Type { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? TargetValue { get; set; }

        public bool IsActive { get; set; }

        public ICollection<ContestLearner> Learners { get; set; }

        public DateTime CreatedOn { get; set; }

        public ContestFetched()
        {
            this.Learners = new List<ContestLearner>();
        }
    }
}