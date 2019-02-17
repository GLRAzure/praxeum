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

        public ContestLearnerProgressUpdater(
            IMapper mapper,
            IContestRepository contestRepository,
            IContestLearnerRepository contestLearnerRepository,
            IMicrosoftProfileRepository microsoftProfileRepository,
            IContestLearnerTargetValueUpdater contestLearnerTargetValueUpdater,
            IContestLearnerCurrentValueUpdater contestLearnerCurrentValueUpdater)
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

            if (contest.Status != ContestStatus.InProgress)
            {
                contestLearnerProgressUpdated =
                    _mapper.Map(contestLearner, contestLearnerProgressUpdated);

                return contestLearnerProgressUpdated;
            }

            var microsoftProfile =
                await _microsoftProfileRepository.FetchProfileAsync(
                    contestLearner.UserName);

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
    }
}