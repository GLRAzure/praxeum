using Newtonsoft.Json;
using Praxeum.Data;

namespace Praxeum.Domain.Learners
{
    public class LearnerFetched : Learner
    {
        [JsonProperty(Order = 1)]

        public bool IsCached { get; set; }

        public LearnerFetched()
        {
            this.IsCached = false;
        }
    }
}