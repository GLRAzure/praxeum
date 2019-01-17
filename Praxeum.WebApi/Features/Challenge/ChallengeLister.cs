using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.Challenges
{
    public class ChallengeLister : IHandler<ChallengeList, IEnumerable<ChallengeListed>>
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeLister(
            IChallengeRepository challengeFetchListRepository)
        {
            _challengeRepository =
                challengeFetchListRepository;
        }

        public async Task<IEnumerable<ChallengeListed>> ExecuteAsync(
            ChallengeList challengeFetchList)
        {
            var challengeList =
                await _challengeRepository.FetchListAsync();

            var challengeFetchedList =
                challengeList.Select(x => Mapper.Map(x, new ChallengeListed()));

            return challengeFetchedList;
        }
    }
}