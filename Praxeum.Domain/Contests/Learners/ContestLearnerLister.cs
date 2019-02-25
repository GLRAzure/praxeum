using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerLister : IHandler<ContestLearnerList, IEnumerable<ContestLearnerListed>>
    {
        private readonly IMapper _mapper;
        private readonly IContestLearnerRepository _contestLearnerRepository;

        public ContestLearnerLister(
            IMapper mapper,
            IContestLearnerRepository contestLearnerRepository)
        {
            _mapper =
                mapper;
            _contestLearnerRepository =
                contestLearnerRepository;
        }

        public async Task<IEnumerable<ContestLearnerListed>> ExecuteAsync(
            ContestLearnerList contestLearnerList)
        {
            var contestLearners =
                await _contestLearnerRepository.FetchListAsync(
                    contestLearnerList.ContestId);

            contestLearners = 
                contestLearners.Where(
                    x => !string.IsNullOrWhiteSpace(x.DisplayName));

            var contestLearnerListed =
                contestLearners.Select(
                    x => _mapper.Map(x, new ContestLearnerListed()));

            return contestLearnerListed;
        }
    }
}