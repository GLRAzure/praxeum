using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestAdder : IHandler<ContestAdd, ContestAdded>
    {
        private readonly IMapper _mapper;
        private readonly IContestRepository _contestRepository;

        public ContestAdder(
            IMapper mapper,
            IContestRepository contestRepository)
        {
            _mapper =
                mapper;
            _contestRepository =
                contestRepository;
        }

        public async Task<ContestAdded> ExecuteAsync(
            ContestAdd contestAdd)
        {
            var contest =
                _mapper.Map(contestAdd, new Contest());

            contest = 
                await _contestRepository.AddAsync(
                    contest);

            if(!string.IsNullOrWhiteSpace(contestAdd.Prizes))
            {
                contest.HasPrizes = true;
            }

            var contestAdded =
                _mapper.Map(contest, new ContestAdded());

            return contestAdded;
        }
    }
}