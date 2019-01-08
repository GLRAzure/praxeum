using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerLister : IHandler<LearnerList, IEnumerable<LearnerListed>>
    {
        private readonly ILearnerRepository _learnerRepository;

        public LearnerLister(
            ILearnerRepository learnerFetchListRepository)
        {
            _learnerRepository =
                learnerFetchListRepository;
        }

        public async Task<IEnumerable<LearnerListed>> ExecuteAsync(
            LearnerList learnerFetchList)
        {
            var learners =
                await _learnerRepository.FetchListAsync();

            var learnerListed =
                learners.Select(x => Mapper.Map(x, new LearnerListed()));

            return learnerListed;
        }
    }
}