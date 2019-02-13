using Praxeum.Data;

namespace Praxeum.Domain
{
    public interface IContestLearnerTargetValueUpdater
    {        
        ContestLearner Update(
            Contest contest, 
            ContestLearner contestLearner);
    }
}
