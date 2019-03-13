using System;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerProgressUpdater : IHandler<ContestLearnerProgressUpdate, ContestLearnerProgressUpdated>
    {
        private readonly IMapper _mapper;
        private readonly IContestRepository _contestRepository;
        private readonly IContestLearnerRepository _contestLearnerRepository;
        private readonly IMicrosoftProfileRepository _microsoftProfileRepository;
        private readonly IContestLearnerTargetValueUpdater _contestLearnerTargetValueUpdater;
        private readonly IContestLearnerCurrentValueUpdater _contestLearnerCurrentValueUpdater;
        private readonly IExperiencePointsCalculator _experiencePointsCalculator;

        public ContestLearnerProgressUpdater(
            IMapper mapper,
            IContestRepository contestRepository,
            IContestLearnerRepository contestLearnerRepository,
            IMicrosoftProfileRepository microsoftProfileRepository,
            IContestLearnerTargetValueUpdater contestLearnerTargetValueUpdater,
            IContestLearnerCurrentValueUpdater contestLearnerCurrentValueUpdater,
            IExperiencePointsCalculator experiencePointsCalculator)
        {
            _mapper =
                mapper;
            _contestRepository =
                contestRepository;
            _contestLearnerRepository =
                contestLearnerRepository;
            _microsoftProfileRepository =
                microsoftProfileRepository;
            _contestLearnerTargetValueUpdater =
                contestLearnerTargetValueUpdater;
            _contestLearnerCurrentValueUpdater =
                contestLearnerCurrentValueUpdater;
            _experiencePointsCalculator =
                experiencePointsCalculator;
        }

        public async Task<ContestLearnerProgressUpdated> ExecuteAsync(
            ContestLearnerProgressUpdate contestLearnerProgressUpdate)
        {
            var contest =
               await _contestRepository.FetchByIdAsync(
                   contestLearnerProgressUpdate.ContestId);

            if (contest == null)
            {
                throw new NullReferenceException($"Contest {contestLearnerProgressUpdate.ContestId} not found");
            }

            var contestLearner =
                await _contestLearnerRepository.FetchByIdAsync(
                    contestLearnerProgressUpdate.ContestId,
                    contestLearnerProgressUpdate.Id);

            var contestLearnerProgressUpdated =
                 new ContestLearnerProgressUpdated();

            if (contest.Status != ContestStatus.InProgress
                || string.IsNullOrWhiteSpace(contestLearner.DisplayName))
            {
                contestLearnerProgressUpdated =
                    _mapper.Map(contestLearner, contestLearnerProgressUpdated);

                return contestLearnerProgressUpdated;
            }

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
                    _contestLearnerTargetValueUpdater.Update(
                        contest,
                        contestLearner);

                contestLearner =
                    _contestLearnerCurrentValueUpdater.Update(
                        contest,
                        contestLearner,
                        microsoftProfile);

                contestLearner.LastProgressUpdateOn =
                    DateTime.UtcNow;

                contestLearner =
                    await _contestLearnerRepository.UpdateByIdAsync(
                        contestLearnerProgressUpdate.ContestId,
                        contestLearnerProgressUpdate.Id,
                        contestLearner);

                contestLearnerProgressUpdated =
                    _mapper.Map(contestLearner, contestLearnerProgressUpdated);

                return contestLearnerProgressUpdated;
            }
            catch (Exception ex)
            {
                throw new MicrosoftProfileException(contestLearner, ex);
            }
        }
    }
}