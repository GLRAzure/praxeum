using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Praxeum.Data;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerFetcher : IHandler<LearnerFetch, LearnerFetched>
    {
        private readonly IOptions<LearnerOptions> _learnerOptions;
        private readonly ILearnerRepository _learnerRepository;

        public LearnerFetcher(
            IOptions<LearnerOptions> learnerOptions,
            ILearnerRepository learnerRepository)
        {
            _learnerOptions =
                learnerOptions;
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