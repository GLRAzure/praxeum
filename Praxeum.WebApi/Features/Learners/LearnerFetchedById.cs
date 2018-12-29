using Newtonsoft.Json;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerFetchedById : Learner
    {
        [JsonProperty(Order = 1)]

        public bool IsCached { get; set; }

        public LearnerFetchedById()
        {
            this.IsCached = false;
        }
    }
}