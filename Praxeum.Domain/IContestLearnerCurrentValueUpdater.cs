using Praxeum.Data;

namespace Praxeum.Domain
{
    public interface IContestLearnerCurrentValueUpdater
    {
        ContestLearner Update(
            Contest contest,
            ContestLearner contestLearner,
            MicrosoftProfile microsoftProfile);
    }
}
