using System;
using Praxeum.Data;

namespace Praxeum.Domain
{
    public class ContestLearnerCurrentValueUpdater : IContestLearnerCurrentValueUpdater
    {
        private readonly IExperiencePointsCalculator _experiencePointsCalculator;

        public ContestLearnerCurrentValueUpdater(
            IExperiencePointsCalculator experiencePointsCalculator)
        {
            _experiencePointsCalculator =
                experiencePointsCalculator;
        }

        public ContestLearner Update(
            Contest contest,
            ContestLearner contestLearner,
            MicrosoftProfile microsoftProfile)
        {
            switch (contest.Type)
            {
                case ContestType.AccumulatedLevels:
                    contestLearner.CurrentValue = 
                        microsoftProfile.GameStatus.Level.LevelNumber - contestLearner.StartValue;
                    break;                
                case ContestType.AccumulatedPoints:
                case ContestType.Leaderboard:
                    contestLearner.CurrentValue =
                        _experiencePointsCalculator.Calculate(
                            microsoftProfile.GameStatus.Level.LevelNumber,
                            microsoftProfile.GameStatus.CurrentLevelPointsEarned) - contestLearner.StartValue;
                    break;
                case ContestType.Levels:
                    contestLearner.CurrentValue =
                        microsoftProfile.GameStatus.Level.LevelNumber;
                    break;
                case ContestType.Points:
                    contestLearner.CurrentValue =
                        _experiencePointsCalculator.Calculate(
                            microsoftProfile.GameStatus.Level.LevelNumber,
                            microsoftProfile.GameStatus.CurrentLevelPointsEarned);
                    break;
            }

            return contestLearner;
        }
    }
}
