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
        [Display(Name = "User Names")]
        public string UserNames { get; set; }
    }
}