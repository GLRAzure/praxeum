using Newtonsoft.Json;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerAdded : ContestLearner
    {
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        public ContestLearnerAdded()
        {

        }

        public ContestLearnerAdded(
            Learner learner)
        {
            this.DisplayName = learner.DisplayName;
            this.UserName = learner.UserName;
        }
    }
}
