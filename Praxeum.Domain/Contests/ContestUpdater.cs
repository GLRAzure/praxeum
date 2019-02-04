using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestUpdater : IHandler<ContestUpdate, ContestUpdated>
    {
        private readonly IContestRepository _contestRepository;

        public ContestUpdater(
            IContestRepository contestRepository)
        {
            _contestRepository =
                contestRepository;
        }

        public async Task<ContestUpdated> ExecuteAsync(
            ContestUpdate contestUpdate)
        {
            var contest =
                await _contestRepository.FetchByIdAsync(
                    contestUpdate.Id);

            Mapper.Map(contestUpdate, contest);

            contest =
                await _contestRepository.UpdateByIdAsync(
                    contestUpdate.Id,
                    contest);

            if(!string.IsNullOrWhiteSpace(contestUpdate.Prizes))
            {
                contest.HasPrizes = true;
            }

            var contestUpdated =
                Mapper.Map(contest, new ContestUpdated());

            return contestUpdated;
        }
    }
}