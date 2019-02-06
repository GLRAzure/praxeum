using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Contests
{
    public class ContestUpdate: ContestAdd 
    {
        [Required]
        public string Status { get; set; }
    }
}