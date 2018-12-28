using Newtonsoft.Json;
using Praxeum.FunctionApp.Data;

namespace Praxeum.FunctionApp.Features.Learners
{
    public class LearnerFetchedByUserName : Learner
    {
        [JsonProperty(Order = 1)]
        public bool IsCached { get; set; }

        public LearnerFetchedByUserName()
        {
        }

        public LearnerFetchedByUserName(
            Learner learner)
        {
            this.Id = learner.Id;
            this.DisplayName = learner.DisplayName;
            this.UserPrincipalName = learner.UserPrincipalName;
            this.UserName = learner.UserName;
            this.HasSeenMicrosoftPrivacyNotice = learner.HasSeenMicrosoftPrivacyNotice;
            this.ProgressStatus = learner.ProgressStatus;
            this.Achievements = learner.Achievements;
            this.CreatedOn = learner.CreatedOn;
            this.ModifiedOn = learner.ModifiedOn;
        }
    }
}