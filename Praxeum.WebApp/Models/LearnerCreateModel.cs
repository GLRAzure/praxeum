using System.ComponentModel.DataAnnotations;

namespace Praxeum.WebApp.Models
{
    public class LearnerCreateModel
    {
        [Required]
        public string Name { get; set; }
    }
}