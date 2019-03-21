using System;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestEnder : IHandler<ContestEnd, ContestEnded>
    {
        private readonly IMapper _mapper;
        private readonly IContestRepository _contestRepository;

        public ContestEnder(
            IMapper mapper,
            IContestRepository contestRepository)
        {
            _mapper =
                mapper;
            _contestRepository =
                contestRepository;
        }

        public async Task<ContestEnded> ExecuteAsync(
            ContestEnd contestEnd)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestEnd.Id);

            contest.Status = ContestStatus.Ended;

            if (!contest.EndDate.HasValue)
            {
                contest.EndDate = DateTime.UtcNow;
            }

            contest =
                await _contestRepository.UpdateByIdAsync(
                    contest.Id, 
                    contest);

            var contestEnded =
                _mapper.Map(contest, new ContestEnded());
            
            return contestEnded;
        }
    }
}