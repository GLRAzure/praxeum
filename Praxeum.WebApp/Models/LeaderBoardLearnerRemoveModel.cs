using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.WebApp.Models
{
    public class LeaderBoardLearnerRemoveModel
    {
        public Guid LeaderBoardId { get; set; }

        [Required]
        [Display(Name = "Learner")]
        public Guid LearnerId { get; set; }

        public string LearnerName { get; set; }
    }
}