using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Praxeum.WebApi.Data;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerUpdater : IHandler<LearnerUpdate, LearnerUpdated>
    {
        private readonly IOptions<LearnerOptions> _learnerOptions;
        private readonly IMicrosoftProfileFetcher _microsoftProfileFetcher;
        private readonly ILearnerRepository _learnerRepository;

        public LearnerUpdater(
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

        public async Task<LearnerUpdated> ExecuteAsync(
            LearnerUpdate learnerUpdate)
        {
            var learner =
                await _learnerRepository.FetchByIdAsync(
                    learnerUpdate.Id);

            if (learner == null)
            {
                throw new NullReferenceException($"Learner {learnerUpdate.Id} does not exist.");
            }

            Mapper.Map(learnerUpdate, learner);

            var microsoftProfile =
                _microsoftProfileFetcher.FetchProfileAsync(learner.UserName);

            learner =
                Mapper.Map(microsoftProfile, learner);

            learner.LastModifiedOn =
                DateTime.UtcNow;

            learner =
                await _learnerRepository.UpdateByIdAsync(
                    learnerUpdate.Id,
                    learner);

            var learnerUpdated =
                Mapper.Map(learner, new LearnerUpdated());

            learnerUpdated.IsCached = false;

            return learnerUpdated;
        }
    }
}