using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        public bool HasPrizes { get; set; }

        [JsonProperty(PropertyName = "prizes")]
        public string Prizes { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "endDate")]
        public DateTime? EndDate { get; set; }

        [JsonProperty(PropertyName = "targetValue")]
        public int? TargetValue { get; set; } // Note: Could be points, levels, badge and/or achievements

        [JsonProperty(PropertyName = "isActive")]
        public bool IsActive { get; set; }

        [JsonProperty(PropertyName = "learners")]
        public ICollection<ContestLearner> Learners { get; set; }

        [JsonProperty(PropertyName = "createdOn")]
        public DateTime CreatedOn { get; set; }

        public Contest()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsActive = true;
            this.Learners = new List<ContestLearner>();
        }
    }
}
