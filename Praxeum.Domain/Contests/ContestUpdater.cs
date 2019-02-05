using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestUpdater : IHandler<ContestUpdate, ContestUpdated>
    {
        private readonly IMapper _mapper;
        private readonly IContestRepository _contestRepository;

        public ContestUpdater(
            IMapper mapper,
            IContestRepository contestRepository)
        {
            _mapper =
                mapper;
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

            contest =
                await _contestRepository.UpdateByIdAsync(
                    contestUpdate.Id,
                    contest);

            if(!string.IsNullOrWhiteSpace(contestUpdate.Prizes))
            {
                contest.HasPrizes = true;
            }

            var contestUpdated =
                _mapper.Map(contest, new ContestUpdated());

            return contestUpdated;
        }
    }
}