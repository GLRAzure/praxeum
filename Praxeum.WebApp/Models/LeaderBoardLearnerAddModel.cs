using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.WebApp.Models
{
    public class LeaderBoardLearnerAddModel
    {
        [Required]
        public Guid LeaderBoardId { get; set; }

        [Required]
        public string Names { get; set; }
    }
}