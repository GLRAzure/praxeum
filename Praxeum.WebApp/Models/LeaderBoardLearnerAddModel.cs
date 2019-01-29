using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.WebApp.Models
{
    public class LeaderBoardLearnerAddModel
    {
        [Required]
        [Display(Name = "Leader Board")]
        public Guid LeaderBoardId { get; set; }

        [Required]
        public string Names { get; set; }
    }
}