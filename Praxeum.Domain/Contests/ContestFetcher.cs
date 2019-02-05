using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestFetcher : IHandler<ContestFetch, ContestFetched>
    {
        private readonly IMapper _mapper;
        private readonly IContestRepository _contestRepository;

        public ContestFetcher(
            IMapper mapper,
            IContestRepository contestRepository)
        {
            _mapper =
                mapper;
            _contestRepository =
                contestRepository;
        }

        public async Task<ContestFetched> ExecuteAsync(
            ContestFetch contestFetch)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestFetch.Id);

            var contestFetched =
                _mapper.Map(contest, new ContestFetched());

            return contestFetched;
        }
    }
}