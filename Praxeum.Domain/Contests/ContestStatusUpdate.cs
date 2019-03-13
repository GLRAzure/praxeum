using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Contests
{
    public class ContestStatusUpdate
    {
        [Required]
        public Guid Id { get; set; }
    }
}