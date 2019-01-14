using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Praxeum.WebApi.Data;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerFetcher : IHandler<LearnerFetch, LearnerFetched>
    {
        private readonly IOptions<LearnerOptions> _learnerOptions;
        private readonly IMicrosoftProfileFetcher _microsoftProfileFetcher;
        private readonly ILearnerRepository _learnerRepository;

        public LearnerFetcher(
            IOptions<LearnerOptions> learnerOptions,
            IMicrosoftProfileFetcher microsoftProfileFetcher,
            ILearnerRepository learnerRepository)
        {
            _learnerOptions =
                learnerOptions;
            _microsoftProfileFetcher =
                microsoftProfileFetcher;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<LearnerFetched> ExecuteAsync(
            LearnerFetch learnerFetch)
        {
            var learner =
                await _learnerRepository.FetchByIdAsync(
                    learnerFetch.Id);

            bool isCached = true;

            if (learner.IsExpired)
            {
                var microsoftProfile =
                    _microsoftProfileFetcher.FetchProfileAsync(learner.UserName);

                learner =
                    Mapper.Map(microsoftProfile, new Learner());

                learner.ExpiresOn =
                    DateTime.UtcNow.AddMinutes(
                        _learnerOptions.Value.CacheExpiresInMinutes);

                learner =
                    await _learnerRepository.UpdateByIdAsync(
                        learner.Id,
                        learner);

                isCached = false;
           }

            var learnerFetched =
                Mapper.Map(learner, new LearnerFetched());

            learnerFetched.IsCached = isCached;

            return learnerFetched;
        }
    }
}