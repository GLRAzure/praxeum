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

        public ContestLearnerProgressUpdater(
            IMapper mapper,
            IContestRepository contestRepository,
            IContestLearnerRepository contestLearnerRepository,
            IMicrosoftProfileRepository microsoftProfileRepository)
        {
            _mapper =
                mapper;
            _contestRepository =
                contestRepository;
            _contestLearnerRepository =
                contestLearnerRepository;
            _microsoftProfileRepository =
                microsoftProfileRepository;
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
            try
            {
                var microsoftProfile =
                    await _microsoftProfileRepository.FetchProfileAsync(
                        contestLearner.UserName);

                if (contest.Type == ContestType.Points)
                {
                    var experiencePointsCalculator =
                        new ExperiencePointsCalculator();

                    contestLearner.CurrentValue =
                        experiencePointsCalculator.Calculate(
                            microsoftProfile.ProgressStatus.CurrentLevel,
                            microsoftProfile.ProgressStatus.CurrentLevelPointsEarned);
                }
                else if (contest.Type == ContestType.Level)
                {
                    contestLearner.CurrentValue =
                        microsoftProfile.ProgressStatus.CurrentLevel;
                }
                else
                {
                    throw new NotImplementedException();
                }

                contestLearner.Status = ContestLearnerStatus.Updated;
                contestLearner.StatusMessage = string.Empty;

                if ((contestLearner.CurrentValue - contestLearner.TargetValue) <= contest.TargetValue)
                {
                    contestLearner.IsDone = true;
                }
                else
                {
                    contestLearner.IsDone = false;
                }
            }
            catch (Exception ex)
            {
                contestLearner.Status = ContestLearnerStatus.Failed;
                contestLearner.StatusMessage = ex.Message;
            }

            contestLearner.TargetValue =
                contest.TargetValue;

            contestLearner =
                await _contestLearnerRepository.UpdateByIdAsync(
                    contestLearnerProgressUpdate.ContestId,
                    contestLearnerProgressUpdate.Id,
                    contestLearner);

            var contestLearnerProgressUpdated =
                _mapper.Map(contestLearner, new ContestLearnerProgressUpdated());

            return contestLearnerProgressUpdated;
        }
    }
}