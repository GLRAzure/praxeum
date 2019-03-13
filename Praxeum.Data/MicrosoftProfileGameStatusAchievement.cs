using Newtonsoft.Json;

namespace Praxeum.Data
{
    public class MicrosoftProfileGameStatusAchievement
    {
        [JsonProperty(PropertyName = "awardUid")]
        public string AwardId { get; set; }

        [JsonProperty(PropertyName = "awardType")]
        public string AwardType { get; set; }

        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; set; }

        [JsonProperty(PropertyName = "awardedDateUTC")]
        public string AwardedDate { get; set; }

        [JsonProperty(PropertyName = "sourceUid")]
        public string SourecId { get; set; }

        [JsonProperty(PropertyName = "sourceType")]
        public string SourceType { get; set; }
    }
}
