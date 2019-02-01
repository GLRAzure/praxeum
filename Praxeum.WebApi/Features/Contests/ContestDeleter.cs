using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Contests
{
    public class ContestDeleter : IHandler<ContestDelete, ContestDeleted>
    {
        private readonly IContestRepository _contestRepository;

        public ContestDeleter(
            IContestRepository contestRepository)
        {
            _contestRepository =
                contestRepository;
        }

        public async Task<ContestDeleted> ExecuteAsync(
            ContestDelete contestDelete)
        {
            var contest =
                await _contestRepository.DeleteByIdAsync(
                    contestDelete.Id);

            var contestDeleted =
                Mapper.Map(contest, new ContestDeleted());

            return contestDeleted;
        }
    }
}