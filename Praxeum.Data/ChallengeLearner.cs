using Newtonsoft.Json;
using System;

namespace Praxeum.Data
{
    public class ChallengeLearner: Learner
    {
        [JsonProperty(PropertyName = "currentProgressStatus")]
        public LearnerProgressStatus CurrentProgressStatus { get; set; }

        public ChallengeLearner()
        {
            this.CurrentProgressStatus = 
                new LearnerProgressStatus();
        }
    }
}
