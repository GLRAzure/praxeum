using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerListAdd
    {
        [SwaggerExclude]
        [Display(Name = "Contest")]
        public Guid ContestId { get; set; }

        [Required]
        public string UserNames { get; set; }
    }
}