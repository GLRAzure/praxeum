using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Data
{
    public class Contest
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "hasPrizes")]
        [Display(Name = "Has Prizes")]
        public bool HasPrizes { get; set; }

        [JsonProperty(PropertyName = "prizes")]
        public string Prizes { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "endDate")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [JsonProperty(PropertyName = "targetValue")]
        [Display(Name = "Target Value")]
        public int? TargetValue { get; set; } // Note: Could be points, levels, badge and/or achievements

        [JsonProperty(PropertyName = "isActive")]
        public bool IsActive { get; set; }

        [JsonProperty(PropertyName = "numberOfLearner")]
        public int NumberOfLearners { get; set; }

        [JsonProperty(PropertyName = "createdOn")]
        public DateTime CreatedOn { get; set; }

        public Contest()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsActive = true;
        }
    }
}
