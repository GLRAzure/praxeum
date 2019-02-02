using System.ComponentModel.DataAnnotations;

namespace Praxeum.WebApp.Models
{
    public class ContestCreateModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}