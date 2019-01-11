using System;
using System.Collections.Generic;

namespace Praxeum.WebApp.Models
{
    public class LearnerIndexModel
    {
        public Guid Id { get; set; }

        public string DisplayNameAndUserName => $"{this.DisplayName} ({this.UserName})"; 

        public string DisplayName { get; set; }

        public string UserPrincipalName { get; set; }

        public string UserName { get; set; }

        public bool HasSeenMicrosoftPrivacyNotice { get; set; }

        public LearnerProgressStatus ProgressStatus { get; set; }

        public ICollection<LearnerAchievement> Achievements { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        public bool IsExpired => this.ExpiresOn == null || this.ExpiresOn <= DateTime.UtcNow;

        public class LearnerAchievement
        {
            public string Title { get; set; }

            public string Type { get; set; }

            public string Url { get; set; }

            public string Image { get; set; }

            public DateTime AchievedDate { get; set; }
        }

        public class LearnerProgressStatus
        {
            public int TotalPoints { get; set; }

            public int  CurrentLevel { get; set; }

            public int CurrentLevelPointsEarned { get; set; }

            public int BadgesEarned { get; set; }

            public int TrophiesEarned { get; set; }
        }
    }
}
