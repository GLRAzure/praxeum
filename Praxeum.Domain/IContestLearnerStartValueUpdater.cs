using Praxeum.Data;

namespace Praxeum.Domain
{
    public interface IContestLearnerStartValueUpdater
    {
        ContestLearner Update(
            Contest contest,
            ContestLearner contestLearner,
            MicrosoftProfile microsoftProfile,
            bool forceUpdate = false);
    }
}
