using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Praxeum.FunctionApp.Data
{
    public class LeaderBoard
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "isActive")]
        public bool IsActive { get; set; }

        [JsonProperty(PropertyName = "learners")]
        public ICollection<LeaderBoardLearner> Learners { get; set; }

        [JsonProperty(PropertyName = "createdOn")]
        public DateTime CreatedOn { get; set; }

        public LeaderBoard()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.IsActive = true;
            this.Learners = new List<LeaderBoardLearner>();
        }
    }
}
