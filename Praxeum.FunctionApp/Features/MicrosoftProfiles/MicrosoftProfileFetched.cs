using Praxeum.FunctionApp.Data;

namespace Praxeum.FunctionApp.Features.MicrosoftProfiles
{
    public class MicrosoftProfileFetched : MicrosoftProfile
    {
        public MicrosoftProfileFetched()
        {

        }

        public MicrosoftProfileFetched(
            MicrosoftProfile microsoftProfile)
        {
            this.Id = microsoftProfile.Id;
            this.DisplayName = microsoftProfile.DisplayName;
            this.UserPrincipalName = microsoftProfile.UserPrincipalName;
            this.UserName = microsoftProfile.UserName;
            this.HasSeenMicrosoftPrivacyNotice = microsoftProfile.HasSeenMicrosoftPrivacyNotice;
            this.ProgressStatus = microsoftProfile.ProgressStatus;
            this.Achievements = microsoftProfile.Achievements;
            this.CreatedOn = microsoftProfile.CreatedOn;
        }
    }
}
