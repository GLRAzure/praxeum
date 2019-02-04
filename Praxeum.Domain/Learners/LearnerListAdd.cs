using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Learners
{
    public class LearnerListAdd
    {
        [Display(Name = "Leader Board")]
        public Guid? LeaderBoardId { get; set; }

        [Required]
        public string Names { get; set; }
    }
}