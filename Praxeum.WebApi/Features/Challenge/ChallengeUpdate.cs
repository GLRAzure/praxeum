using System;
using System.ComponentModel.DataAnnotations;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Challenges
{
    public class ChallengeUpdate 
    {
        [SwaggerExclude]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool HasPrizes { get; set; }

        public string Prizes { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? TargetValue { get; set; }

        public bool IsActive { get; set; }
    }
}