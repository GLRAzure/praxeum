using Praxeum.Data;
using Praxeum.Domain.Contests.Learners;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name="Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name="End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name="Target Value")]
        public int? TargetValue { get; set; }

        public bool IsActive { get; set; }

        public ICollection<ContestLearnerFetched> Learners { get; set; }

        public int NumberOfLearners { get; set; }

        public DateTime CreatedOn { get; set; }

        public ContestFetched()
        {
            this.Learners = new List<ContestLearnerFetched>();
        }
    }
}