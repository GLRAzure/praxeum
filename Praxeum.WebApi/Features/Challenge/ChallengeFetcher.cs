using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.Challenges
{
    public class ChallengeFetcher : IHandler<ChallengeFetch, ChallengeFetched>
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeFetcher(
            IChallengeRepository challengeRepository)
        {
            _challengeRepository =
                challengeRepository;
        }

        public async Task<ChallengeFetched> ExecuteAsync(
            ChallengeFetch challengeFetch)
        {
            var challenge =
                await _challengeRepository.FetchByIdAsync(
                    challengeFetch.Id);

            var challengeFetched =
                Mapper.Map(challenge, new ChallengeFetched());

            return challengeFetched;
        }
    }
}