using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Data;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Challenges
{
    public class ChallengeDeleter : IHandler<ChallengeDelete, ChallengeDeleted>
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeDeleter(
            IChallengeRepository challengeRepository)
        {
            _challengeRepository =
                challengeRepository;
        }

        public async Task<ChallengeDeleted> ExecuteAsync(
            ChallengeDelete challengeDelete)
        {
            var challenge =
                await _challengeRepository.DeleteByIdAsync(
                    challengeDelete.Id);

            var challengeDeleted =
                Mapper.Map(challenge, new ChallengeDeleted());

            return challengeDeleted;
        }
    }
}