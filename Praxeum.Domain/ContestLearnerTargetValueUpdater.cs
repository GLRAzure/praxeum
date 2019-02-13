using System;
using Praxeum.Data;

namespace Praxeum.Domain
{
    public class ContestLearnerTargetValueUpdater : IContestLearnerTargetValueUpdater
    {
        public ContestLearner Update(
            Contest contest,
            ContestLearner contestLearner)
        {
            switch (contest.Type)
            {
                case ContestType.AccumulatedLevels:
                case ContestType.AccumulatedPoints:
                    contestLearner.TargetValue =
                        contestLearner.StartValue + contest.TargetValue;
                    break;
                case ContestType.Levels:
                case ContestType.Points:
                    contestLearner.TargetValue = contest.TargetValue;
                    break;
                case ContestType.Leaderboard:
                    contestLearner.TargetValue = null;
                    break;
                default:
                    throw new NotSupportedException();
            }

            return contestLearner;
        }
    }
}
