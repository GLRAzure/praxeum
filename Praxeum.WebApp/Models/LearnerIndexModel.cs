using Humanizer;
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

        public string Status { get; set; }

        public string StatusMessage { get; set; }

        public int Rank { get; set; }

        public LearnerProgressStatus ProgressStatus { get; set; }

        public ICollection<LearnerAchievement> Achievements { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }

        // https://github.com/Humanizr/Humanizer#humanize-datetime
        public string LastModifiedOnHumanized => this.LastModifiedOn.Humanize();

        public bool IsExpired => this.LastModifiedOn == null || this.LastModifiedOn <= DateTime.UtcNow;

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
