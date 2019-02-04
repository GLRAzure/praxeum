using Newtonsoft.Json;
using System;

namespace Praxeum.Data
{
    public class ContestLearner
    {
        [JsonProperty(PropertyName = "learnerId")]
        public Guid LearnerId { get; set; }

        [JsonProperty(PropertyName = "rank")]
        public int Rank { get; set; }

        [JsonProperty(PropertyName = "originalProgressStatus")]
        public LearnerProgressStatus OriginalProgressStatus { get; set; }

        [JsonProperty(PropertyName = "currentProgressStatus")]
        public LearnerProgressStatus CurrentProgressStatus { get; set; }

        public ContestLearner()
        {
            this.OriginalProgressStatus =
                new LearnerProgressStatus();
            this.CurrentProgressStatus =
                new LearnerProgressStatus();
        }
    }
}
