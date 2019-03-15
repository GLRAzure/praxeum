using System;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestStarter : IHandler<ContestStart, ContestStarted>
    {
        private readonly IMapper _mapper;
        private readonly IEventPublisher _eventPublisher;
        private readonly IContestRepository _contestRepository;

        public ContestStarter(
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

        public async Task<ContestStarted> ExecuteAsync(
            ContestStart contestStart)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestStart.Id);

            contest.Status = ContestStatus.InProgress;

            if (!contest.StartDate.HasValue)
            {
                contest.StartDate = DateTime.UtcNow;
            }

            contest =
                await _contestRepository.UpdateByIdAsync(
                    contest.Id, 
                    contest);

            var contestStarted =
                _mapper.Map(contest, new ContestStarted());

            await _eventPublisher.PublishAsync("contest.started", contestStarted);

            return contestStarted;
        }
    }
}