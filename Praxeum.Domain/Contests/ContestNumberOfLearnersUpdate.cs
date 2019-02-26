using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Contests
{
    public class ContestNumberOfLearnersUpdate
    {
        [Required]
        public Guid ContestId { get; set; }
    }
}