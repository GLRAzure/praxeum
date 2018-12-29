using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerFetchListHandler : ILearnerHandler<LearnerFetchList, IEnumerable<LearnerFetchedList>>
    {
        private readonly ILearnerRepository _learnerRepository;

        public LearnerFetchListHandler(
            ILearnerRepository learnerFetchListRepository)
        {
            _learnerRepository =
                learnerFetchListRepository;
        }

        public async Task<IEnumerable<LearnerFetchedList>> ExecuteAsync(
            LearnerFetchList learnerFetchList)
        {
            var learnerList =
                await _learnerRepository.FetchListAsync();

            var learnerFetchedList =
                learnerList.Select(x => Mapper.Map(x, new LearnerFetchedList()));

            return learnerFetchedList;
        }
    }
}