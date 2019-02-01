using Newtonsoft.Json;

namespace Praxeum.Data
{
    public class ContestLearner: Learner
    {
        [JsonProperty(PropertyName = "currentProgressStatus")]
        public LearnerProgressStatus CurrentProgressStatus { get; set; }

        public ContestLearner()
        {
            this.CurrentProgressStatus = 
                new LearnerProgressStatus();
        }
    }
}
