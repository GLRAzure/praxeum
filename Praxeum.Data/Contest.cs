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

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [JsonProperty(PropertyName = "endDate")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [JsonProperty(PropertyName = "targetValue")]
        [Display(Name = "Target Value")]
        public int? TargetValue { get; set; }

        [JsonProperty(PropertyName = "numberOfLearners")]
        public int NumberOfLearners { get; set; }

        [JsonProperty(PropertyName = "customCss")]
        [Display(Name = "Custom Css")]
        public string CustomCss { get; set; }

        [JsonProperty(PropertyName = "createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "lastProgressUpdateOn")]
        [Display(Name = "Last Progress Update On")]
        public DateTime? LastProgressUpdateOn { get; set; }

        [JsonProperty(PropertyName = "nextProgressUpdateOn")]
        [Display(Name = "Next Progress Update On")]
        public DateTime? NextProgressUpdateOn { get; set; }

        [JsonProperty(PropertyName = "progressUpdateInterval")]
        [Display(Name = "Progress Update Interval")]
        public int ProgressUpdateInterval { get; set; }

        public Contest()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.ProgressUpdateInterval = 1440;
        }

        public bool IsStatus(string status)
        {
            return this.Status == status;
        }

        public bool IsPointsContest()
        {
            switch (this.Type)
            {
                case ContestType.Points:
                case ContestType.AccumulatedPoints:
                case ContestType.Leaderboard:
                    return true;
                default:
                    return false;
            }
        }
    }
}
