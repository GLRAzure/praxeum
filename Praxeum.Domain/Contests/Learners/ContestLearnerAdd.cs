using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerAdd
    {
        [Required]
        public Guid ContestId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
}
