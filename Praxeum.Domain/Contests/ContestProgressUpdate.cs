using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Contests
{
    public class ContestProgressUpdate
    {
        [Required]
        public Guid Id { get; set; }
    }
}