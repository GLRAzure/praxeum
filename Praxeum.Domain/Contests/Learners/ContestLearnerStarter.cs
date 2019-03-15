using System;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerStarter : IHandler<ContestLearnerStart, ContestLearnerStarted>
    {
        private readonly IMapper _mapper;
        private readonly IContestRepository _contestRepository;
        private readonly IContestLearnerRepository _contestLearnerRepository;
        private readonly IMicrosoftProfileRepository _microsoftProfileRepository;
        private readonly IContestLearnerStartValueUpdater _contestLearnerStartValueUpdater;
        private readonly IContestLearnerCurrentValueUpdater _contestLearnerCurrentValueUpdater;
        private readonly IExperiencePointsCalculator _experiencePointsCalculator;

        public ContestLearnerStarter(
            IMapper mapper,
            IContestRepository contestRepository,
            IContestLearnerRepository contestLearnerRepository,
            IMicrosoftProfileRepository microsoftProfileRepository,
            IContestLearnerStartValueUpdater contestLearnerStartValueUpdater,
            IContestLearnerCurrentValueUpdater contestLearnerCurrentValueUpdater,
            IExperiencePointsCalculator experiencePointsCalculator
            )
        {
            _mapper =
                mapper;
            _contestRepository =
                contestRepository;
            _contestLearnerRepository =
                contestLearnerRepository;
            _microsoftProfileRepository =
                microsoftProfileRepository;
            _contestLearnerStartValueUpdater =
                contestLearnerStartValueUpdater;
            _contestLearnerCurrentValueUpdater =
                contestLearnerCurrentValueUpdater;
            _experiencePointsCalculator =
                experiencePointsCalculator;
        }

        public async Task<ContestLearnerStarted> ExecuteAsync(
            ContestLearnerStart contestLearnerStart)
        {
            var contestLearner =
                await _contestLearnerRepository.FetchByIdAsync(
                    contestLearnerStart.ContestId,
                    contestLearnerStart.Id);

            if (contestLearner == null)
            {
                throw new ArgumentOutOfRangeException($"Contest learner {contestLearnerStart.Id} not found.");
            }

            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestLearner.ContestId);

            var contestLearnerStarted =
                 new ContestLearnerStarted();

            try
            {
                var microsoftProfile =
                    await _microsoftProfileRepository.FetchProfileAsync(
                        contestLearner.UserName);

                contestLearner.Level =
                    microsoftProfile.GameStatus.Level.LevelNumber;
                contestLearner.Points =
                    _experiencePointsCalculator.Calculate(
                        microsoftProfile.GameStatus.Level.LevelNumber,
                        microsoftProfile.GameStatus.CurrentLevelPointsEarned);

                contestLearner =
                    _contestLearnerStartValueUpdater.Update(
                        contest,
                        contestLearner,
                        microsoftProfile,
                        true);

                contestLearner =
                    _contestLearnerCurrentValueUpdater.Update(
                        contest,
                        contestLearner,
                        microsoftProfile);

                contestLearner.LastProgressUpdateOn =
                    DateTime.UtcNow;

                contestLearner =
                    await _contestLearnerRepository.UpdateByIdAsync(
                        contestLearnerStart.ContestId,
                        contestLearnerStart.Id,
                        contestLearner);

                contestLearnerStarted =
                    _mapper.Map(contestLearner, contestLearnerStarted);

                return contestLearnerStarted;
            }
            catch (Exception ex)
            {
                throw new MicrosoftProfileException(contestLearner, ex);
            }
        }
    }
}