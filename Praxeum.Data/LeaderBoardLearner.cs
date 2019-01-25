using Newtonsoft.Json;
using System;

namespace Praxeum.Data
{
    public class LeaderBoardLearner
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "learnerId")]
        public Guid LearnerId { get; set; }
    }
}
