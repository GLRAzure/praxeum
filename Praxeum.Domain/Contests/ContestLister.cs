using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestLister : IHandler<ContestList, IEnumerable<ContestListed>>
    {
        private readonly IMapper _mapper;
        private readonly IContestRepository _contestRepository;

        public ContestLister(
            IMapper mapper,
            IContestRepository contestFetchListRepository)
        {
            _mapper =
                mapper;
            _contestRepository =
                contestFetchListRepository;
        }

        public async Task<IEnumerable<ContestListed>> ExecuteAsync(
            ContestList contestList)
        {
            var contests =
                await _contestRepository.FetchListAsync(
                    contestList.Status);

            var contestListed =
                contests.Select(x => _mapper.Map(x, new ContestListed()));

            contestListed = 
                contestListed.OrderBy(x => x.Name);

            return contestListed;
        }
    }
}