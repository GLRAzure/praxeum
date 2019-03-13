using System;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;
using Praxeum.Domain.Contests.Learners;

namespace Praxeum.Domain.Contests
{
    public class ContestStatusUpdater : IHandler<ContestStatusUpdate, ContestStatusUpdated>
    {
        private readonly IMapper _mapper;
        private readonly IEventPublisher _eventPublisher;
        private readonly IContestRepository _contestRepository;

        public ContestStatusUpdater(
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

        public async Task<ContestStatusUpdated> ExecuteAsync(
            ContestStatusUpdate contestUpdate)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestUpdate.Id);

            if (contest.IsStatus(ContestStatus.Ready) && contest.StartDate <= DateTime.UtcNow)
            {
                contest.Status = ContestStatus.InProgress;

                contest =
                    await _contestRepository.UpdateByIdAsync(
                        contestUpdate.Id,
                        contest);

            }
            else if (contest.IsStatus(ContestStatus.InProgress) && contest.EndDate <= DateTime.UtcNow)
            {
                contest.Status = ContestStatus.Ended;

                contest =
                    await _contestRepository.UpdateByIdAsync(
                        contestUpdate.Id,
                        contest);
            }

            var contestStatusUpdated =
                _mapper.Map(contest, new ContestStatusUpdated());

            await _eventPublisher.PublishAsync("contest.updated", contestStatusUpdated);

            return contestStatusUpdated;
        }
    }
}