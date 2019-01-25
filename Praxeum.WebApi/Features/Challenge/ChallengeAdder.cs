using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.WebApi.Features.Challenges
{
    public class ChallengeAdder : IHandler<ChallengeAdd, ChallengeAdded>
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeAdder(
            IChallengeRepository challengeRepository)
        {
            _challengeRepository =
                challengeRepository;
        }

        public async Task<ChallengeAdded> ExecuteAsync(
            ChallengeAdd challengeAdd)
        {
            var challenge =
                Mapper.Map(challengeAdd, new Challenge());

            challenge = 
                await _challengeRepository.AddAsync(
                    challenge);

            var challengeAdded =
                Mapper.Map(challenge, new ChallengeAdded());

            return challengeAdded;
        }
    }
}