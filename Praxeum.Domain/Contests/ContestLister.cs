using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestLister : IHandler<ContestList, IEnumerable<ContestListed>>
    {
        private readonly IContestRepository _contestRepository;

        public ContestLister(
            IContestRepository contestFetchListRepository)
        {
            _contestRepository =
                contestFetchListRepository;
        }

        public async Task<IEnumerable<ContestListed>> ExecuteAsync(
            ContestList contestFetchList)
        {
            var contestList =
                await _contestRepository.FetchListAsync();

            var contestFetchedList =
                contestList.Select(x => Mapper.Map(x, new ContestListed()));

            return contestFetchedList;
        }
    }
}