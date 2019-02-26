using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests
{
    public class ContestDeleter : IHandler<ContestDelete, ContestDeleted>
    {
        private readonly IMapper _mapper;
        private readonly IContestRepository _contestRepository;

        public ContestDeleter(
            IMapper mapper,
            IContestRepository contestRepository)
        {
            _mapper =
                mapper;
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
                _mapper.Map(contest, new ContestDeleted());

            return contestDeleted;
        }
    }
}