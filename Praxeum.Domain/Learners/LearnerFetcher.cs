using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Learners
{
    public class LearnerFetcher : IHandler<LearnerFetch, LearnerFetched>
    {
        private readonly ILearnerRepository _learnerRepository;

        public LearnerFetcher(
            ILearnerRepository learnerRepository)
        {
            _learnerRepository =
                learnerRepository;
        }

        public async Task<LearnerFetched> ExecuteAsync(
            LearnerFetch learnerFetch)
        {
            var learner =
                await _learnerRepository.FetchByIdAsync(
                    learnerFetch.Id);

            var learnerFetched =
                Mapper.Map(learner, new LearnerFetched());

            return learnerFetched;
        }
    }
}