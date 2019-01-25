using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Praxeum.Data
{
    public class MicrosoftProfile
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "userPrincipalName")]
        public string UserPrincipalName { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "hasSeenMicrosoftPrivacyNotice")]
        public bool HasSeenMicrosoftPrivacyNotice { get; set; }

        [JsonProperty(PropertyName = "progressStatus")]
        public MicrosoftProfileProgressStatus ProgressStatus { get; set; }

        [JsonProperty(PropertyName = "achievements")]
        public ICollection<MicrosoftProfileAchievement> Achievements { get; set; }

        [JsonProperty(PropertyName = "createdOn")]
        public DateTime CreatedOn { get; set; }

        public MicrosoftProfile()
        {
            this.Id = Guid.NewGuid().ToString();
            this.HasSeenMicrosoftPrivacyNotice = true;
            this.ProgressStatus = new MicrosoftProfileProgressStatus();
            this.Achievements = new List<MicrosoftProfileAchievement>();
        }
    }
}
