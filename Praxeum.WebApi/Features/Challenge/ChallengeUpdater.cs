using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.Challenges
{
    public class ChallengeUpdater : IHandler<ChallengeUpdate, ChallengeUpdated>
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeUpdater(
            IChallengeRepository challengeRepository)
        {
            _challengeRepository =
                challengeRepository;
        }

        public async Task<ChallengeUpdated> ExecuteAsync(
            ChallengeUpdate challengeUpdate)
        {
            var challenge =
                await _challengeRepository.FetchByIdAsync(
                    challengeUpdate.Id);

            Mapper.Map(challengeUpdate, challenge);

            challenge =
                await _challengeRepository.UpdateByIdAsync(
                    challengeUpdate.Id,
                    challenge);

            var challengeUpdated =
                Mapper.Map(challenge, new ChallengeUpdated());

            return challengeUpdated;
        }
    }
}