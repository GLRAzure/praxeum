using Newtonsoft.Json;
using System;
using System.Linq;

namespace Praxeum.FunctionApp.Data
{
    public class MicrosoftProfileAchievement
    {
        [JsonProperty(PropertyName = "id")]
        public string Id => this.Url.Split('/').Last();

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "achievedDate")]
        public DateTime AchievedDate { get; set; }
    }
}
