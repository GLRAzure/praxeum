using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Praxeum.Data
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

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "statusMessage")]
        public string StatusMessage { get; set; }

        [JsonProperty(PropertyName = "hasSeenMicrosoftPrivacyNotice")]
        public bool HasSeenMicrosoftPrivacyNotice { get; set; }

        [JsonProperty(PropertyName = "rank")]
        public int Rank { get; set; }

        [JsonProperty(PropertyName = "progressStatus")]
        public LearnerProgressStatus ProgressStatus { get; set; }

        [JsonProperty(PropertyName = "achievements")]
        public ICollection<LearnerAchievement> Achievements { get; set; }

        [JsonProperty(PropertyName = "numberOfLeaderBoards")]
        public int NumberOfLeaderBoards { get; set; }

        [JsonProperty(PropertyName = "leaderBoards")]
        public ICollection<LearnerLeaderBoard> LeaderBoards { get; set; }

        [JsonProperty(PropertyName = "createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "lastModifiedOn")]
        public DateTime LastModifiedOn { get; set; }

        public Learner()
        {
            this.Id = Guid.NewGuid();
            this.HasSeenMicrosoftPrivacyNotice = true;
            this.ProgressStatus = new LearnerProgressStatus();
            this.Achievements = new List<LearnerAchievement>();
            this.NumberOfLeaderBoards = 0;
            this.LeaderBoards = new List<LearnerLeaderBoard>();
            this.CreatedOn = DateTime.UtcNow;
            this.LastModifiedOn = this.CreatedOn;
        }
    }
}
