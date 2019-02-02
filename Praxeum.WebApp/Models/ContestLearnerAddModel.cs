using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.WebApp.Models
{
    public class ContestLearnerAddModel
    {
        [Required]
        [Display(Name = "Contest")]
        public Guid ContestId { get; set; }

        [Required]
        public string Names { get; set; }
    }
}