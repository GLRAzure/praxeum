using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerList
    {
        [Required]
        public Guid ContestId { get; set; }
    }
}