using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Praxeum.WebApi.Data
{
    public class Learner
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "userPrincipalName")]
        public string UserPrincipalName { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "hasSeenMicrosoftPrivacyNotice")]
        public bool HasSeenMicrosoftPrivacyNotice { get; set; }

        [JsonProperty(PropertyName = "progressStatus")]
        public LearnerProgressStatus ProgressStatus { get; set; }

        [JsonProperty(PropertyName = "achievements")]
        public ICollection<LearnerAchievement> Achievements { get; set; }

        [JsonProperty(PropertyName = "createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "expiresOn")]
        public DateTime ExpiresOn { get; set; }

        [JsonIgnore]
        public bool IsExpired => this.ExpiresOn == null || this.ExpiresOn <= DateTime.UtcNow;

        public Learner()
        {
            this.Id = Guid.NewGuid();
            this.HasSeenMicrosoftPrivacyNotice = true;
            this.ProgressStatus = new LearnerProgressStatus();
            this.Achievements = new List<LearnerAchievement>();
        }
    }
}
