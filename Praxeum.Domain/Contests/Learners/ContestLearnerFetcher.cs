using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerFetcher : IHandler<ContestLearnerFetch, ContestLearnerFetched>
    {        
        private readonly IMapper _mapper;
        private readonly IContestLearnerRepository _contestLearnerRepository;

        public ContestLearnerFetcher(
            IMapper mapper,
            IContestLearnerRepository contestLearnerRepository)
        {
            _mapper =
                mapper;
            _contestLearnerRepository =
                contestLearnerRepository;
        }

        public async Task<ContestLearnerFetched> ExecuteAsync(
            ContestLearnerFetch contestLearnerFetch)
        {
            var contestLearner =
                await _contestLearnerRepository.FetchByIdAsync(
                    contestLearnerFetch.ContestId,
                    contestLearnerFetch.Id);

            if (contestLearner == null)
            {
                throw new NullReferenceException($"Contest {contestLearnerFetch.ContestId} not found.");
            }

            var contestLearnerFetched =
                _mapper.Map(contestLearner, new ContestLearnerFetched());
            
            return contestLearnerFetched;
        }
    }
}