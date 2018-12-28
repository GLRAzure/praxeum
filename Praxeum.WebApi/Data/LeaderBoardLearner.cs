using Newtonsoft.Json;

namespace Praxeum.WebApi.Data
{
    public class LeaderBoardLearner
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
