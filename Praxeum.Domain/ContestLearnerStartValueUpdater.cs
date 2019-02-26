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
                case ContestType.AccumulatedLevels:
                    if (contestLearner.StartValue == null || forceUpdate)
                    {
                        contestLearner.StartValue =
                            microsoftProfile.ProgressStatus.CurrentLevel;
                    }
                    break;
                case ContestType.AccumulatedPoints:
                    if (contestLearner.StartValue == null || forceUpdate)
                    {
                        contestLearner.StartValue =
                            _experiencePointsCalculator.Calculate(
                                microsoftProfile.ProgressStatus.CurrentLevel, 
                                microsoftProfile.ProgressStatus.CurrentLevelPointsEarned);
                    }
                    break;
                case ContestType.Levels:
                    contestLearner.StartValue = 1;
                    break;
                default:
                    contestLearner.StartValue = 0;
                    break;
            }

            return contestLearner;
        }
    }
}
