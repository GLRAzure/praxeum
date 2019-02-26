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

        public ContestUpdater(
            IMapper mapper,
            IEventPublisher eventPublisher,
            IContestRepository contestRepository)
        {
            _mapper =
                mapper;
            _eventPublisher =
                eventPublisher;
            _contestRepository =
                contestRepository;
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
                contest.TargetValue = null;
            }

            if (contest.Status == ContestStatus.InProgress
                && contest.StartDate == null)
            {
                contest.StartDate = DateTime.UtcNow;
            }

            if (contest.Status == ContestStatus.Ended
                && contest.EndDate == null)
            {
                contest.EndDate = DateTime.UtcNow;
            }

            contest =
                await _contestRepository.UpdateByIdAsync(
                    contestUpdate.Id,
                    contest);

            var contestUpdated =
                _mapper.Map(contest, new ContestUpdated());

            await _eventPublisher.PublishAsync("contest.updated", contestUpdated);

            return contestUpdated;
        }
    }
}