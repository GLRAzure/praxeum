using Newtonsoft.Json;

namespace Praxeum.Data
{
    public class MicrosoftProfileGameStatusLevel
    {
        [JsonProperty(PropertyName = "levelNumber")]
        public int LevelNumber { get; set; }

        [JsonProperty(PropertyName = "pointsLow")]
        public int PointsLow { get; set; }

        [JsonProperty(PropertyName = "pointsHigh")]
        public int PointsHigh { get; set; }
    }
}
