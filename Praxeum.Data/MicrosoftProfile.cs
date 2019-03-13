using Newtonsoft.Json;
using System;

namespace Praxeum.Data
{
    public class MicrosoftProfile
    {
        [JsonProperty(PropertyName = "userId")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "hasSeenMicrosoftPrivacyNotice")]
        public bool HasSeenMicrosoftPrivacyNotice { get; set; }

        [JsonProperty(PropertyName = "isMicrosoftUser")]
        public bool IsMicrosoftUser { get; set; }

        [JsonProperty(PropertyName = "gameStatus")]
        public MicrosoftProfileGameStatus GameStatus { get; set; }

        [JsonProperty(PropertyName = "createdOn")]
        public string CreatedOn { get; set; }

        public MicrosoftProfile()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsMicrosoftUser = false;
            this.HasSeenMicrosoftPrivacyNotice = true;
            this.GameStatus = new MicrosoftProfileGameStatus();
        }
    }
}
