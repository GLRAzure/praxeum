using Praxeum.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Contests
{
    public class ContestAdd
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
        
        [Display(Name="Target Value")]
        [RequiredIf("Type", ContestType.Leaderboard, ErrorMessage = "Target Value is required when Type is not Leaderboard.", IsInverted = true)]
        public int? TargetValue { get; set; }

        [Display(Name="Start Date")]
        public DateTime? StartDate { get; set; }
        
        [Display(Name="End Date")]
        public DateTime? EndDate { get; set; }
        
        [Display(Name="Custom Css")]
        public string CustomCss { get; set; }
        
        [Display(Name="Progress Update Interval")]
        public int ProgressUpdateInterval { get; set; }
        
        [Display(Name="SelfRegistrationEnabled")]
        public bool SelfRegistrationEnabled { get; set; }

        public ContestAdd()
        {
            this.Id = Guid.NewGuid();
            this.Type = ContestType.AccumulatedPoints;
            this.HasPrizes = false;
            this.SelfRegistrationEnabled = false;
        }
    }
}