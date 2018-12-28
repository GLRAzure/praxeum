using Praxeum.FunctionApp.Data;

namespace Praxeum.FunctionApp.Features.MicrosoftProfiles
{
    public class MicrosoftProfileFetched : MicrosoftProfile
    {
        public MicrosoftProfileFetched()
        {

        }

        public MicrosoftProfileFetched(
            MicrosoftProfile profile)
        {
            this.Id = profile.Id;
            this.DisplayName = profile.DisplayName;
            this.UserPrincipalName = profile.UserPrincipalName;
            this.UserName = profile.UserName;
            this.HasSeenMicrosoftPrivacyNotice = profile.HasSeenMicrosoftPrivacyNotice;
            this.ProgressStatus = profile.ProgressStatus;
            this.Achievements = profile.Achievements;
            this.CreatedOn = profile.CreatedOn;
        }
    }
}
