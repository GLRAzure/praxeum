using Newtonsoft.Json;

namespace Praxeum.FunctionApp.Data
{
    public class LeaderBoardLearner
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
