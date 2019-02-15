using System;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestUpdater : IHandler<ContestUpdate, ContestUpdated>
    {
        private readonly IMapper _mapper;
        private readonly IEventPublisher _eventPublisher;
        private readonly IContestRepository _contestRepository;
        private readonly IContestLearnerRepository _contestLearnerRepository;
        private readonly IContestLearnerTargetValueUpdater _contestLearnerTargetValueUpdater;

        public ContestUpdater(
            IMapper mapper,
            IEventPublisher eventPublisher,
            IContestRepository contestRepository,
            IContestLearnerRepository contestLearnerRepository,
            IContestLearnerTargetValueUpdater contestLearnerTargetValueUpdater)
        {
            _mapper =
                mapper;
            _eventPublisher =
                eventPublisher;
            _contestRepository =
                contestRepository;
            _contestLearnerRepository =
                contestLearnerRepository;
            _contestLearnerTargetValueUpdater =
                contestLearnerTargetValueUpdater;
        }

        public async Task<ContestUpdated> ExecuteAsync(
            ContestUpdate contestUpdate)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestUpdate.Id);

            _mapper.Map(contestUpdate, contest);

            if (!string.IsNullOrWhiteSpace(contestUpdate.Prizes))
            {
                contest.HasPrizes = true;
            }

            if (contest.Type == ContestType.Leaderboard)
            {
                contest.TargetValue = 0;
            }

            if (contest.Status == ContestStatus.InProgress)
            {
                if (contest.StartDate == null)
                {
                    contest.StartDate = DateTime.UtcNow;
                }
            }

            if (contest.Status == ContestStatus.Ended)
            {
                if (contest.EndDate == null)
                {
                    contest.EndDate = DateTime.UtcNow;
                }
            }

            contest =
                await _contestRepository.UpdateByIdAsync(
                    contestUpdate.Id,
                    contest);

            var contestLearners =
                await _contestLearnerRepository.FetchListAsync(
                    contest.Id);

            foreach(var contestLearner in contestLearners)
            {
                _contestLearnerTargetValueUpdater.Update(
                    contest, contestLearner);

                await _contestLearnerRepository.UpdateByIdAsync(
                    contest.Id, contestLearner.Id, contestLearner);
            }

            var contestUpdated =
                _mapper.Map(contest, new ContestUpdated());

            await _eventPublisher.PublishAsync("contest.updated", contestUpdated);

            return contestUpdated;
        }
    }
}