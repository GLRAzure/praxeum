using Newtonsoft.Json;
using System;

namespace Praxeum.Data
{
    [Obsolete]
    public class LeaderBoardLearner
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "learnerId")]
        public Guid LearnerId { get; set; }
    }
}
