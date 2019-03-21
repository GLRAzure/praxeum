using System;
using Praxeum.Data;

namespace Praxeum.Domain
{
    public class ContestLearnerStartValueUpdater : IContestLearnerStartValueUpdater
    {
        private readonly IExperiencePointsCalculator _experiencePointsCalculator;

        public ContestLearnerStartValueUpdater(
            IExperiencePointsCalculator experiencePointsCalculator)
        {
            _experiencePointsCalculator =
                experiencePointsCalculator;
        }

        public ContestLearner Update(
            Contest contest,
            ContestLearner contestLearner,
            MicrosoftProfile microsoftProfile,
            bool forceUpdate = false)
        {
            switch (contest.Type)
            {
                case ContestType.Levels:
                case ContestType.AccumulatedLevels:
                    if (contestLearner.StartValue == null || forceUpdate)
                    {
                        contestLearner.StartValue =
                            microsoftProfile.GameStatus.Level.LevelNumber;
                    }
                    break;
                case ContestType.Points:
                case ContestType.AccumulatedPoints:
                case ContestType.Leaderboard:
                    if (contestLearner.StartValue == null || forceUpdate)
                    {
                        contestLearner.StartValue =
                            _experiencePointsCalculator.Calculate(
                                microsoftProfile.GameStatus.Level.LevelNumber,
                                microsoftProfile.GameStatus.CurrentLevelPointsEarned);
                    }
                    break;
                default:
                    contestLearner.StartValue = 0;
                    break;
            }

            return contestLearner;
        }
    }
}
